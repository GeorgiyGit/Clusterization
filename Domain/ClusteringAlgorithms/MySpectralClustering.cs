using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ClusteringAlgorithms
{
    public class MySpectralClustering
    {
        public int k;
        public double gamma;

        public MySpectralClustering(int k, double gamma)
        {
            this.k = k;
            this.gamma = gamma;
        } // ctor

        public int[] Cluster(double[][] X)
        {
            double[][] A = this.MakeAffinityRBF(X);  // RBF
            double[][] L = this.MakeLaplacian(A);    // normalized
            double[][] E = this.MakeEmbedding(L);    // eigenvectors
            int[] result = this.ProcessEmbedding(E); // k-means

            return result;
        }

        // ------------------------------------------------------

        private double[][] MakeAffinityRBF(double[][] X)
        {
            // 1s on diagonal (x1 == x2), towards 0 dissimilar
            int n = X.Length;
            double[][] result = MatMake(n, n);
            for (int i = 0; i < n; ++i)
            {
                for (int j = i; j < n; ++j) // upper
                {
                    double rbf = MyRBF(X[i], X[j], this.gamma);
                    result[i][j] = rbf;
                    result[j][i] = rbf;
                }
            }
            return result;
        }

        // ------------------------------------------------------

        private static double MyRBF(double[] v1, double[] v2,
          double gamma)
        {
            // similarity. when v1 == v2, rbf = 1.0
            // less similar returns small values between 0 and 1
            int dim = v1.Length;
            double sum = 0.0;
            for (int i = 0; i < dim; ++i)
                sum += (v1[i] - v2[i]) * (v1[i] - v2[i]);
            return Math.Exp(-gamma * sum);
        }

        // ------------------------------------------------------

        private double[][] MakeAffinityRNC(double[][] X)
        {
            // radius neighbors connectivity
            // 1 if x1 and x2 are close; 0 if not close
            int n = X.Length;
            double[][] result = MatMake(n, n);
            for (int i = 0; i < n; ++i)
            {
                for (int j = i; j < n; ++j) // upper
                {
                    double d = Distance(X[i], X[j]);
                    if (d < this.gamma)
                    {
                        result[i][j] = 1.0;
                        result[j][i] = 1.0;
                    }
                }
            }
            return result;
        }

        // ------------------------------------------------------

        private static double Distance(double[] v1,
         double[] v2)
        {
            // helper for MakeAffinityRNC()
            int dim = v1.Length;
            double sum = 0.0;
            for (int j = 0; j < dim; ++j)
                sum += (v1[j] - v2[j]) * (v1[j] - v2[j]);
            return Math.Sqrt(sum);
        }

        // ------------------------------------------------------

        private double[][] MakeLaplacian(double[][] A)
        {
            // unnormalized
            // clear but not very efficient to construct D
            // L = D - A
            // here A is an affinity-style adjaceny matrix
            int n = A.Length;

            double[][] D = MatMake(n, n);  // degree matrix
            for (int i = 0; i < n; ++i)
            {
                double rowSum = 0.0;
                for (int j = 0; j < n; ++j)
                    rowSum += A[i][j];
                D[i][i] = rowSum;
            }
            double[][] result = MatDifference(D, A);  // D-A
            return this.NormalizeLaplacian(result);

            // more efficient, but less clear
            //int n = A.Length;
            //double[] rowSums = new double[n];
            //for (int i = 0; i < n; ++i)
            //{
            //  double rowSum = 0.0;
            //  for (int j = 0; j < n; ++j)
            //    rowSum += A[i][j];
            //  rowSums[i] = rowSum;
            //}

            //double[][] result = MatMake(n, n);
            //for (int i = 0; i < n; ++i)
            //  result[i][i] = rowSums[i];  // degree
            //for (int i = 0; i < n; ++i)
            //  for (int j = 0; j < n; ++j)
            //    if (i == j)
            //      result[i][j] = rowSums[i] - A[i][j];
            //    else
            //      result[i][j] = -A[i][j];
            //return result;
        }

        // ------------------------------------------------------

        private double[][] NormalizeLaplacian(double[][] L)
        {
            // scipy library csgraph._laplacian technique
            int n = L.Length;
            double[][] result = MatCopy(L);
            for (int i = 0; i < n; ++i)
                result[i][i] = 0.0;  // zap away diagonal

            // sqrt of col sums
            double[] w = new double[n];
            for (int j = 0; j < n; ++j)
            {
                double colSum = 0.0;  // in-degree version
                for (int i = 0; i < n; ++i)
                    colSum += Math.Abs(result[i][j]);
                w[j] = Math.Sqrt(colSum);
            }

            for (int i = 0; i < n; ++i)
                for (int j = 0; j < n; ++j)
                    result[i][j] /= (w[j] * w[i]);

            // restore diagonal
            for (int i = 0; i < n; ++i)
                result[i][i] = 1.0;

            return result;
        }

        // ------------------------------------------------------

        private double[][] MakeEmbedding(double[][] L)
        {
            // eigenvectors for k-smallest eigenvalues
            // extremely deep graph theory
            double[] eigVals;
            double[][] eigVecs;
            EigenQR(L, out eigVals, out eigVecs); // QR algorithm
            int[] allIndices = ArgSort(eigVals);
            int[] indices = new int[this.k]; // small eigenvecs
            for (int i = 0; i < this.k; ++i)
                indices[i] = allIndices[i];
            double[][] extracted =
              MatExtractCols(eigVecs, indices);
            return extracted;
        }

        // ------------------------------------------------------

        private int[] ProcessEmbedding(double[][] E)
        {
            // cluster a complex transformation of source data
            KMeans km = new KMeans(E, this.k);
            int[] clustering = km.Cluster();
            return clustering;
        }

        // ------------------------------------------------------

        private static double[][] MatDifference(double[][] ma,
          double[][] mb)
        {
            int r = ma.Length;
            int c = ma[0].Length;
            double[][] result = MatMake(r, c);
            for (int i = 0; i < r; ++i)
                for (int j = 0; j < c; ++j)
                    result[i][j] = ma[i][j] - mb[i][j];
            return result;
        }

        // ------------------------------------------------------

        private static int[] ArgSort(double[] vec)
        {
            int n = vec.Length;
            int[] idxs = new int[n];
            for (int i = 0; i < n; ++i)
                idxs[i] = i;
            Array.Sort(vec, idxs);  // sort idxs based on vec
            return idxs;
        }

        // ------------------------------------------------------

        private static double[][] MatExtractCols(double[][] m,
          int[] cols)
        {
            int r = m.Length;
            int c = cols.Length;
            double[][] result = MatMake(r, c);

            for (int j = 0; j < cols.Length; ++j)
            {
                for (int i = 0; i < r; ++i)
                {
                    result[i][j] = m[i][cols[j]];
                }
            }
            return result;
        }

        // === Eigen functions ==================================

        private static void EigenQR(double[][] M,
          out double[] eigenVals, out double[][] eigenVecs)
        {
            // compute eigenvalues and eigenvectors same time
            // stats.stackexchange.com/questions/20643/finding-
            //   matrix-eigenvectors-using-qr-decomposition

            int n = M.Length;
            double[][] X = MatCopy(M);  // mat must be square
            double[][] Q; double[][] R;
            double[][] pq = MatIdentity(n);
            int maxCt = 200;

            int ct = 0;
            while (ct < maxCt)
            {
                MatDecomposeQR(X, out Q, out R, false);
                pq = MatProduct(pq, Q);
                X = MatProduct(R, Q);  // note order
                ++ct;

                if (MatIsUpperTri(X, 1.0e-8) == true)
                    break;
            }

            // eigenvalues are diag elements of X
            double[] evals = new double[n];
            for (int i = 0; i < n; ++i)
                evals[i] = X[i][i];

            // eigenvectors are columns of pq
            double[][] evecs = MatCopy(pq);

            eigenVals = evals;
            eigenVecs = evecs;
        }

        // ------------------------------------------------------

        private static bool MatIsUpperTri(double[][] mat,
          double tol)
        {
            int n = mat.Length;
            for (int i = 0; i < n; ++i)
            {
                for (int j = 0; j < i; ++j)
                {  // check lower vals
                    if (Math.Abs(mat[i][j]) > tol)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        // ------------------------------------------------------

        public static double[][] MatProduct(double[][] matA,
          double[][] matB)
        {
            int aRows = matA.Length;
            int aCols = matA[0].Length;
            int bRows = matB.Length;
            int bCols = matB[0].Length;
            if (aCols != bRows)
                throw new Exception("Non-conformable matrices");

            double[][] result = MatMake(aRows, bCols);

            for (int i = 0; i < aRows; ++i) // each row of A
                for (int j = 0; j < bCols; ++j) // each col of B
                    for (int k = 0; k < aCols; ++k)
                        result[i][j] += matA[i][k] * matB[k][j];

            return result;
        }

        // === QR decomposition functions =======================

        public static void MatDecomposeQR(double[][] mat,
          out double[][] q, out double[][] r,
          bool standardize)
        {
            // QR decomposition, Householder algorithm.
            // assumes square matrix

            int n = mat.Length;  // assumes mat is nxn
            int nCols = mat[0].Length;
            //if (n != nCols) 

            double[][] Q = MatIdentity(n);
            double[][] R = MatCopy(mat);
            for (int i = 0; i < n - 1; ++i)
            {
                double[][] H = MatIdentity(n);
                double[] a = new double[n - i];
                int k = 0;
                for (int ii = i; ii < n; ++ii)  // last part col [i]
                    a[k++] = R[ii][i];

                double normA = VecNorm(a);
                if (a[0] < 0.0) { normA = -normA; }
                double[] v = new double[a.Length];
                for (int j = 0; j < v.Length; ++j)
                    v[j] = a[j] / (a[0] + normA);
                v[0] = 1.0;

                double[][] h = MatIdentity(a.Length);
                double vvDot = VecDot(v, v);
                double[][] alpha = VecToMat(v, v.Length, 1);
                double[][] beta = VecToMat(v, 1, v.Length);
                double[][] aMultB = MatProduct(alpha, beta);

                for (int ii = 0; ii < h.Length; ++ii)
                    for (int jj = 0; jj < h[0].Length; ++jj)
                        h[ii][jj] -= (2.0 / vvDot) * aMultB[ii][jj];

                // copy h into lower right of H
                int d = n - h.Length;
                for (int ii = 0; ii < h.Length; ++ii)
                    for (int jj = 0; jj < h[0].Length; ++jj)
                        H[ii + d][jj + d] = h[ii][jj];

                Q = MatProduct(Q, H);
                R = MatProduct(H, R);
            } // i

            if (standardize == true)
            {
                // standardize so R diagonal is all positive
                double[][] D = MatMake(n, n);
                for (int i = 0; i < n; ++i)
                {
                    if (R[i][i] < 0.0) D[i][i] = -1.0;
                    else D[i][i] = 1.0;
                }
                Q = MatProduct(Q, D);
                R = MatProduct(D, R);
            }

            q = Q;
            r = R;
        } // MatDecomposeQR()

        // ------------------------------------------------------

        private static double VecDot(double[] v1,
          double[] v2)
        {
            double result = 0.0;
            int n = v1.Length;
            for (int i = 0; i < n; ++i)
                result += v1[i] * v2[i];
            return result;
        }

        // ------------------------------------------------------

        private static double VecNorm(double[] vec)
        {
            int n = vec.Length;
            double sum = 0.0;
            for (int i = 0; i < n; ++i)
                sum += vec[i] * vec[i];
            return Math.Sqrt(sum);
        }

        // ------------------------------------------------------

        private static double[][] VecToMat(double[] vec,
          int nRows, int nCols)
        {
            double[][] result = MatMake(nRows, nCols);
            int k = 0;
            for (int i = 0; i < nRows; ++i)
                for (int j = 0; j < nCols; ++j)
                    result[i][j] = vec[k++];
            return result;
        }

        // === common ===========================================

        private static double[][] MatMake(int rows, int cols)
        {
            double[][] result = new double[rows][];
            for (int i = 0; i < rows; ++i)
                result[i] = new double[cols];
            return result;
        }

        // ------------------------------------------------------

        private static double[][] MatCopy(double[][] mat)
        {
            int r = mat.Length;
            int c = mat[0].Length;
            double[][] result = MatMake(r, c);
            for (int i = 0; i < r; ++i)
                for (int j = 0; j < c; ++j)
                    result[i][j] = mat[i][j];
            return result;
        }

        // ------------------------------------------------------

        private static double[][] MatIdentity(int n)
        {
            double[][] result = MatMake(n, n);
            for (int i = 0; i < n; ++i)
                result[i][i] = 1.0;
            return result;
        }

        // === nested KMeans ====================================

        private class KMeans
        {
            public double[][] data;
            public int k;
            public int N;
            public int dim;
            public int trials;  // to find best
            public int maxIter; // inner loop
            public Random rnd;
            public int[] clustering;
            public double[][] means;

            // ----------------------------------------------------

            public KMeans(double[][] data, int k)
            {
                this.data = data;  // by ref
                this.k = k;
                this.N = data.Length;
                this.dim = data[0].Length;
                this.trials = N;  // for Cluster()
                this.maxIter = N * 2;  // for ClusterOnce()
                this.Initialize(0); // seed, means, clustering
            }

            // ----------------------------------------------------

            public void Initialize(int seed)
            {
                this.rnd = new Random(seed);
                this.clustering = new int[this.N];
                this.means = new double[this.k][];
                for (int i = 0; i < this.k; ++i)
                    this.means[i] = new double[this.dim];
                // Random Partition (not Forgy)
                int[] indices = new int[this.N];
                for (int i = 0; i < this.N; ++i)
                    indices[i] = i;
                Shuffle(indices);
                for (int i = 0; i < this.k; ++i)  // first k items
                    this.clustering[indices[i]] = i;
                for (int i = this.k; i < this.N; ++i)
                    this.clustering[indices[i]] =
                      this.rnd.Next(0, this.k); // remaining items

                this.UpdateMeans();
            }

            // ----------------------------------------------------

            private void Shuffle(int[] indices)
            {
                int n = indices.Length;
                for (int i = 0; i < n; ++i)
                {
                    int r = this.rnd.Next(i, n);
                    int tmp = indices[i];
                    indices[i] = indices[r];
                    indices[r] = tmp;
                }
            }

            // ----------------------------------------------------

            private static double SumSquared(double[] v1,
              double[] v2)
            {
                int dim = v1.Length;
                double sum = 0.0;
                for (int i = 0; i < dim; ++i)
                    sum += (v1[i] - v2[i]) * (v1[i] - v2[i]);
                return sum;
            }

            // ----------------------------------------------------

            private static double Distance(double[] item,
              double[] mean)
            {
                double ss = SumSquared(item, mean);
                return Math.Sqrt(ss);
            }

            // ----------------------------------------------------

            private static int ArgMin(double[] v)
            {
                int dim = v.Length;
                int minIdx = 0;
                double minVal = v[0];
                for (int i = 0; i < v.Length; ++i)
                {
                    if (v[i] < minVal)
                    {
                        minVal = v[i];
                        minIdx = i;
                    }
                }
                return minIdx;
            }

            // ----------------------------------------------------

            private static bool AreEqual(int[] a1, int[] a2)
            {
                int dim = a1.Length;
                for (int i = 0; i < dim; ++i)
                    if (a1[i] != a2[i]) return false;
                return true;
            }

            // ----------------------------------------------------

            private static int[] Copy(int[] arr)
            {
                int dim = arr.Length;
                int[] result = new int[dim];
                for (int i = 0; i < dim; ++i)
                    result[i] = arr[i];
                return result;
            }

            // ----------------------------------------------------

            public bool UpdateMeans()
            {
                // verify no zero-counts
                int[] counts = new int[this.k];
                for (int i = 0; i < this.N; ++i)
                {
                    int cid = this.clustering[i];
                    ++counts[cid];
                }
                for (int kk = 0; kk < this.k; ++kk)
                {
                    if (counts[kk] == 0)
                        throw
                          new Exception("0-count in UpdateMeans()");
                }

                // compute proposed new means
                for (int kk = 0; kk < this.k; ++kk)
                    counts[kk] = 0;  // reset
                double[][] newMeans = new double[this.k][];
                for (int i = 0; i < this.k; ++i)
                    newMeans[i] = new double[this.dim];
                for (int i = 0; i < this.N; ++i)
                {
                    int cid = this.clustering[i];
                    ++counts[cid];
                    for (int j = 0; j < this.dim; ++j)
                        newMeans[cid][j] += this.data[i][j];
                }
                for (int kk = 0; kk < this.k; ++kk)
                    if (counts[kk] == 0)
                        return false;  // bad attempt to update

                for (int kk = 0; kk < this.k; ++kk)
                    for (int j = 0; j < this.dim; ++j)
                        newMeans[kk][j] /= counts[kk];

                // copy new means
                for (int kk = 0; kk < this.k; ++kk)
                    for (int j = 0; j < this.dim; ++j)
                        this.means[kk][j] = newMeans[kk][j];

                return true;
            } // UpdateMeans()

            // ----------------------------------------------------

            public bool UpdateClustering()
            {
                // verify no zero-counts
                int[] counts = new int[this.k];
                for (int i = 0; i < this.N; ++i)
                {
                    int cid = this.clustering[i];
                    ++counts[cid];
                }
                for (int kk = 0; kk < this.k; ++kk)
                {
                    if (counts[kk] == 0)
                        throw new
                          Exception("0-count in UpdateClustering()");
                }

                // proposed new clustering
                int[] newClustering = new int[this.N];
                for (int i = 0; i < this.N; ++i)
                    newClustering[i] = this.clustering[i];

                double[] distances = new double[this.k];
                for (int i = 0; i < this.N; ++i)
                {
                    for (int kk = 0; kk < this.k; ++kk)
                    {
                        distances[kk] =
                          Distance(this.data[i], this.means[kk]);
                        int newID = ArgMin(distances);
                        newClustering[i] = newID;
                    }
                }

                if (AreEqual(this.clustering, newClustering) == true)
                    return false;  // no change; short-circuit

                // make sure no count went to 0
                for (int i = 0; i < this.k; ++i)
                    counts[i] = 0;  // reset
                for (int i = 0; i < this.N; ++i)
                {
                    int cid = newClustering[i];
                    ++counts[cid];
                }
                for (int kk = 0; kk < this.k; ++kk)
                    if (counts[kk] == 0)
                        return false;  // bad update attempt

                // no 0 counts so update
                for (int i = 0; i < this.N; ++i)
                    this.clustering[i] = newClustering[i];

                return true;
            } // UpdateClustering()

            // ----------------------------------------------------

            public int[] ClusterOnce()
            {
                bool ok = true;
                int sanityCt = 1;
                while (sanityCt <= this.maxIter)
                {
                    if ((ok = this.UpdateClustering() == false)) break;
                    if ((ok = this.UpdateMeans() == false)) break;
                    ++sanityCt;
                }
                return this.clustering;
            } // ClusterOnce()

            // ----------------------------------------------------

            public double WCSS()
            {
                // within-cluster sum of squares
                double sum = 0.0;
                for (int i = 0; i < this.N; ++i)
                {
                    int cid = this.clustering[i];
                    double[] mean = this.means[cid];
                    double ss = SumSquared(this.data[i], mean);
                    sum += ss;
                }
                return sum;
            }

            // ----------------------------------------------------

            public int[] Cluster()
            {
                double bestWCSS = this.WCSS();  // initial clustering
                int[] bestClustering = Copy(this.clustering);

                for (int i = 0; i < this.trials; ++i)
                {
                    this.Initialize(i);  // new seed, means, clustering
                    int[] clustering = this.ClusterOnce();
                    double wcss = this.WCSS();
                    if (wcss < bestWCSS)
                    {
                        bestWCSS = wcss;
                        bestClustering = Copy(clustering);
                    }
                }
                return bestClustering;
            } // Cluster()

        } // class KMeans

        // ======================================================

    } // Spectral

} // ns
