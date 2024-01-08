using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ClusteringAlgorithms
{
    public class MyGaussianMixture
    {
        public int k;  // number components
        public int maxIter;
        public Random rnd;  // for initialization
        public int N;  // number data items
        public int dim;  // per data item

        public double[] coefs;
        public double[][] means;  // init'ed by Cluster()
        public double[][][] covars;  // init'ed by Cluster()
        public double[][] probs; // computed clustering probs

        public MyGaussianMixture(int k, int maxIter, int seed)
        {
            this.k = k;
            this.maxIter = maxIter;
            this.rnd = new Random(seed);
            this.N = 0;
            this.dim = 0;

            this.coefs = new double[k];
            for (int j = 0; j < k; ++j)
                this.coefs[j] = 1.0 / k;
        } // ctor

        // ------------------------------------------------------

        public int Cluster(double[][] X)
        {
            this.N = X.Length;
            this.dim = X[0].Length;

            // init means to k random data items
            this.means = LocalMatCreate(this.k, this.dim);
            int[] idxs = Select(this.N, this.k);
            for (int j = 0; j < this.k; ++j)
            {
                int idx = idxs[j];
                for (int d = 0; d < this.dim; ++d)
                    this.means[j][d] = X[idx][d];
            }

            // init covars to (dim by dim) Identity matrices
            this.covars = LocalMat3DCreate(this.k,
              this.dim, this.dim);
            for (int j = 0; j < this.k; ++j) // each component
            {
                this.covars[j] = LocalMatIdentity(this.dim);
            }

            // instantiate this.probs
            this.probs = LocalMatCreate(this.N, this.k);
            for (int i = 0; i < this.N; ++i)
                for (int j = 0; j < this.k; ++j)
                    this.probs[i][j] = 1.0 / this.k;

            // use EM meta-algorithm to compute this.probs
            int iter;
            for (iter = 0; iter < this.maxIter; ++iter)
            {
                double oldMeanLogLike = MeanLogLikelihood();
                this.ExpectStep(X);  // update the probs
                double newMeanLogLike = MeanLogLikelihood();

                // auto-stop
                //if (iter > 10 &&
                //  Math.Abs(newMeanLogLike - oldMeanLogLike) <
                //  1.0e-3)
                if (iter > this.maxIter / 2 &&
                  Math.Abs(newMeanLogLike - oldMeanLogLike) < 1.0e-3)
                {
                    break;
                }

                this.MaximStep(X);  // coefs, means, covars
            }
            return iter;
        } // Cluster

        // ------------------------------------------------------

        public double[] PredictProbs(double[] x)
        {
            // for a single input
            double[] result = new double[this.k];
            double sum = 0.0;
            for (int j = 0; j < this.k; ++j)
            {
                double pdf = LocalMatGaussianPdf(x, this.means[j],
                    this.covars[j]);
                result[j] = this.coefs[j] * pdf;
                sum += result[j];
            }
            for (int j = 0; j < this.k; ++j)
                result[j] /= sum;
            return result;
        }

        // ------------------------------------------------------

        public double[][] PredictProbs(double[][] X)
        {
            double[][] result = LocalMatCreate(this.N, this.k);
            for (int j = 0; j < this.k; ++j)
            {
                for (int i = 0; i < this.N; ++i)
                {
                    result[i][j] = this.coefs[j] *
                      LocalMatGaussianPdf(X[i], this.means[j],
                        this.covars[j]);  // this is the hard part
                }
            }

            // make each row sum to 1
            for (int i = 0; i < this.N; ++i)
            {
                double rowSum = 0.0;
                for (int j = 0; j < this.k; ++j)
                    rowSum += result[i][j];
                for (int j = 0; j < this.k; ++j)
                    result[i][j] /= rowSum;
            }

            return result;
        } // PredictProbs

        // ------------------------------------------------------

        public int[] PredictLabels(double[][] X)
        {
            // mimics the scikit GaussianMixture.predict()
            int[] result = new int[this.N];
            double[][] probs = this.PredictProbs(X);  // Nxk
            for (int i = 0; i < this.N; ++i)
            {
                for (int j = 0; j < this.k; ++j)
                {
                    double[] p = probs[i];
                    int maxIdx = ArgMax(p);
                    result[i] = maxIdx;
                }
            }

            return result;
        }

        // ------------------------------------------------------

        private static int ArgMax(double[] vec)
        {
            // helper for PredictLabels()
            int n = vec.Length;
            double maxVal = vec[0];
            int maxIdx = 0;
            for (int i = 0; i < n; ++i)
            {
                if (vec[i] > maxVal)
                {
                    maxVal = vec[i];
                    maxIdx = i;
                }
            }
            return maxIdx;
        }

        // ------------------------------------------------------

        private void ExpectStep(double[][] X)
        {
            // use new means and covariance matrices update probs
            this.probs = this.PredictProbs(X);
            //if (verbose == true)
            //{
            //  Console.WriteLine("\nnew probs: ");
            //  LocalMatShow(this.probs, 4, 9);
            //}
        }

        // ------------------------------------------------------

        private void MaximStep(double[][] X)
        {
            // use new probs update means and covariance matrices
            // 0. compute new prob col sums needed to update
            double[] probSums = new double[this.k];
            for (int i = 0; i < this.N; ++i)
                for (int j = 0; j < this.k; ++j)
                    probSums[j] += this.probs[i][j];

            // 1. update mixture coefficients directly
            for (int j = 0; j < this.k; ++j)
                this.coefs[j] = probSums[j] / this.N;

            // 2. update means indiectly then copy into this.means
            // uj = S(i,n)[pij * xi] / S(i,n)[pij]
            double[][] newMeans = LocalMatCreate(this.k, this.dim);
            for (int j = 0; j < this.k; ++j)
                for (int i = 0; i < this.N; ++i)
                    for (int d = 0; d < this.dim; ++d)
                        newMeans[j][d] += X[i][d] * this.probs[i][j];

            for (int j = 0; j < this.k; ++j)
                for (int d = 0; d < this.dim; ++d)
                    newMeans[j][d] /= probSums[j];

            for (int row = 0; row < this.k; ++row)  // copy
                for (int col = 0; col < this.dim; ++col)
                    this.means[row][col] = newMeans[row][col];

            // 3. update covariances indirectly then copy
            // Cj = S(i,n)[pij * (xi-uj) * (xi-uj)T] / S(i,n)[pij]

            double[][][] newCovars = LocalMat3DCreate(this.k,
              this.dim, this.dim);
            for (int j = 0; j < this.k; ++j)
            {
                double[] u = this.means[j];  // mean for component
                for (int i = 0; i < this.N; ++i)
                {
                    double[] x = X[i];
                    double scale = this.probs[i][j];
                    double[][] tmp = LocalVecVecScale(x, u, scale);
                    // accumulate
                    for (int row = 0; row < this.dim; ++row)
                        for (int col = 0; col < this.dim; ++col)
                            newCovars[j][row][col] += tmp[row][col];
                } // i

                // divide curr covar by prob_sums
                for (int row = 0; row < this.dim; ++row)
                    for (int col = 0; col < this.dim; ++col)
                        newCovars[j][row][col] /= probSums[j];

                // condition the diagonal
                for (int row = 0; row < this.dim; ++row)
                    newCovars[j][row][row] += 1.0e-6;

            } // j

            // copy indirect result into this.covars
            for (int j = 0; j < this.k; ++j)
                for (int row = 0; row < this.dim; ++row)
                    for (int col = 0; col < this.dim; ++col)
                        this.covars[j][row][col] =
                          newCovars[j][row][col];

            //if (verbose == true)
            //{
            //  Console.WriteLine("\nnew coefficients: ");
            //  LocalVecShow(this.coefs, 4, 9);

            //  Console.WriteLine("\nnew means: ");
            //  LocalMatShow(this.means, 4, 9);
            //}

        } // MaximStep

        // ------------------------------------------------------

        public double MeanLogLikelihood()
        {
            // used for auto-stopping
            double sum = 0.0; // for all rows
            for (int i = 0; i < this.N; ++i)
            {
                double rowSum = 0.0;
                for (int j = 0; j < this.k; ++j)
                {
                    if (this.probs[i][j] > 1.0e-6)
                    {
                        rowSum += Math.Log(this.probs[i][j]);
                    }
                }
                sum += rowSum;
            }
            return sum / this.N;
        } // MeanLogLikelihood

        // ------------------------------------------------------
        //
        // lots of local helper functions needed
        //
        // ------------------------------------------------------

        static double[][] LocalVecVecScale(double[] x,
          double[] u, double scale)
        {
            // helper for maximization step
            // (x-u) * (x-u)T * scale
            int n = u.Length;
            double[] x_minus_u = new double[n];
            for (int i = 0; i < n; ++i)
                x_minus_u[i] = x[i] - u[i];

            double[][] result = LocalMatCreate(n, n);
            for (int i = 0; i < n; ++i)
                for (int j = 0; j < n; ++j)
                    result[i][j] = x_minus_u[i] *
                      x_minus_u[j] * scale;
            return result;
        }

        // ------------------------------------------------------

        private int[] Select(int N, int n)
        {
            // select n distinct indices from [0,N-1] inclusive
            int[] indices = new int[N];
            for (int i = 0; i < N; ++i)
                indices[i] = i;
            this.Shuffle(indices);
            int[] result = new int[n];
            for (int i = 0; i < n; ++i)
                result[i] = indices[i];
            return result;
        }

        // ------------------------------------------------------

        private void Shuffle(int[] indices)
        {
            // helper for Select()
            // Fisher-Yates shuffle
            int n = indices.Length;
            for (int i = 0; i < n; ++i)
            {
                int j = this.rnd.Next(i, n);
                int tmp = indices[i];
                indices[i] = indices[j];
                indices[j] = tmp;
            }
        }

        // ------------------------------------------------------

        static double LocalMatGaussianPdf(double[] x,
          double[] u, double[][] covar)
        {
            // multivariate Gaussian distribution PDF
            // x and u must be convert to column vectors/matrices
            double[][] X = LocalVecToMat(x, x.Length, 1);
            double[][] U = LocalVecToMat(u, u.Length, 1);

            int k = x.Length; // dimension
            double[][] a = LocalMatTranspose(LocalMatDiff(X, U));
            double[][] L = LocalMatCholesky(covar);
            double[][] b = LocalMatInverseFromCholesky(L);
            double[][] c = LocalMatDiff(X, U);
            double[][] d = LocalMatProduct(a, b);
            double[][] e = LocalMatProduct(d, c);
            double numer = Math.Exp(-0.5 * e[0][0]);
            double f = Math.Pow(2 * Math.PI, k);
            double g = LocalMatDeterminantFromCholesky(L);
            double denom = Math.Sqrt(f * g);
            double pdf = numer / denom;
            return pdf;
        }

        // ------------------------------------------------------

        static double[][] LocalVecToMat(double[] vec,
          int nRows, int nCols)
        {
            // helper for LocalMatGaussianPdf()
            double[][] result = LocalMatCreate(nRows, nCols);
            int k = 0;
            for (int i = 0; i < nRows; ++i)
                for (int j = 0; j < nCols; ++j)
                    result[i][j] = vec[k++];
            return result;
        }

        // ------------------------------------------------------

        static double[][] LocalMatTranspose(double[][] m)
        {
            // helper for LocalMatGaussianPdf()
            int nr = m.Length;
            int nc = m[0].Length;
            double[][] result = LocalMatCreate(nc, nr);  // note
            for (int i = 0; i < nr; ++i)
                for (int j = 0; j < nc; ++j)
                    result[j][i] = m[i][j];
            return result;
        }

        // ------------------------------------------------------

        static double[][] LocalMatDiff(double[][] A,
          double[][] B)
        {
            // helper for LocalMatGaussianPdf()
            int rows = A.Length;
            int cols = A[0].Length;
            double[][] result = LocalMatCreate(rows, cols);
            for (int i = 0; i < rows; ++i)
                for (int j = 0; j < cols; ++j)
                    result[i][j] = A[i][j] - B[i][j];
            return result;
        }

        // ------------------------------------------------------

        static double[][] LocalMatProduct(double[][] matA,
          double[][] matB)
        {
            // helper for LocalMatGaussianPdf()
            int aRows = matA.Length;
            int aCols = matA[0].Length;
            int bRows = matB.Length;
            int bCols = matB[0].Length;
            if (aCols != bRows)
                throw new Exception("Non-conformable matrices");

            double[][] result = LocalMatCreate(aRows, bCols);

            for (int i = 0; i < aRows; ++i) // each row of A
                for (int j = 0; j < bCols; ++j) // each col of B
                    for (int k = 0; k < aCols; ++k)
                        result[i][j] += matA[i][k] * matB[k][j];

            return result;
        }

        // ------------------------------------------------------

        static double[][] LocalMatCholesky(double[][] m)
        {
            // helper for LocalMatGaussianPdf()
            // Cholesky decomposition
            // m is square, symmetric, positive definite
            // typically a covariance matrix
            int n = m.Length;
            double[][] result = LocalMatCreate(n, n);
            for (int i = 0; i < n; ++i)
            {
                for (int j = 0; j <= i; ++j)
                {
                    double sum = 0.0;
                    for (int k = 0; k < j; ++k)
                        sum += result[i][k] * result[j][k];
                    if (i == j)
                    {
                        double tmp = m[i][i] - sum;
                        if (tmp < 0.0)
                            throw new
                              Exception("MatCholesky fatal error ");
                        result[i][j] = Math.Sqrt(tmp);
                    }
                    else
                    {
                        if (result[j][j] == 0.0)
                            throw new
                              Exception("MatCholesky fatal error ");
                        result[i][j] = (1.0 / result[j][j] *
                          (m[i][j] - sum));
                    }
                } // j
            } // i
            return result;
        } // LocalMatCholesky

        // ------------------------------------------------------

        static double[][]
          LocalMatInverseFromCholesky(double[][] L)
        {
            // helper for LocalMatGaussianPdf()
            // L is a lower triangular result of Cholesky decomp
            // direct version
            int n = L.Length;
            double[][] result = LocalMatIdentity(n);

            for (int k = 0; k < n; ++k)
            {
                for (int j = 0; j < n; j++)
                {
                    for (int i = 0; i < k; i++)
                    {
                        result[k][j] -= result[i][j] * L[k][i];
                    }
                    result[k][j] /= L[k][k];
                }
            }

            for (int k = n - 1; k >= 0; --k)
            {
                for (int j = 0; j < n; j++)
                {
                    for (int i = k + 1; i < n; i++)
                    {
                        result[k][j] -= result[i][j] * L[i][k];
                    }
                    result[k][j] /= L[k][k];
                }
            }
            return result;
        } // LocalMatInverseFromCholesky

        // ------------------------------------------------------

        static double
          LocalMatDeterminantFromCholesky(double[][] L)
        {
            // helper for LocalMatGaussianPdf()
            // product of squared diag elements of L
            double result = 1.0;
            int n = L.Length;
            for (int i = 0; i < n; ++i)
                result *= L[i][i] * L[i][i];
            return result;
        }

        // --------------------------------------------------------

        static double[][] LocalMatIdentity(int n)
        {
            // used by LocalMatInverseFromCholesky and covers init
            double[][] result = LocalMatCreate(n, n);
            for (int i = 0; i < n; ++i)
                result[i][i] = 1.0;
            return result;
        }

        // ------------------------------------------------------

        static double[][] LocalMatCreate(int rows,
          int cols)
        {
            // used by many of the helper functions
            double[][] result = new double[rows][];
            for (int i = 0; i < rows; ++i)
                result[i] = new double[cols];
            return result;
        }

        // ------------------------------------------------------

        static double[][][] LocalMat3DCreate(int factors,
          int rows, int cols)
        {
            // used to init this.covars
            double[][][] result = new double[factors][][];
            for (int f = 0; f < factors; ++f)
            {
                result[f] = LocalMatCreate(rows, cols);
            }
            return result;
        }

        // ------------------------------------------------------

        static void LocalMatShow(double[][] m, int dec, int wid)
        {
            // used by verbose == true methods
            for (int i = 0; i < m.Length; ++i)
            {
                for (int j = 0; j < m[0].Length; ++j)
                {
                    double v = m[i][j];
                    if (Math.Abs(v) < 1.0e-5) v = 0.0;  // avoid "-0"
                    Console.Write(v.ToString("F" + dec).PadLeft(wid));
                }
                Console.WriteLine("");
            }
        }

        // ------------------------------------------------------

        static void LocalVecShow(double[] vec, int dec, int wid)
        {
            // used by verbose == true methods
            for (int i = 0; i < vec.Length; ++i)
            {
                double x = vec[i];
                if (Math.Abs(x) < 1.0e-5) x = 0.0;  // avoid "-0"
                Console.Write(x.ToString("F" + dec).PadLeft(wid));
            }
            Console.WriteLine("");
        }

        // ------------------------------------------------------

    } // class GMM

    // ========================================================

} // ns
