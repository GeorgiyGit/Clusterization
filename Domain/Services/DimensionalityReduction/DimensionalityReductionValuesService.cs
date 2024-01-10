using Accord.Math;
using Accord.Statistics.Analysis;
using Accord.Statistics.Kernels;
using Domain.Entities.Clusterization;
using Domain.Entities.DimensionalityReduction;
using Domain.Entities.Embeddings;
using Domain.Interfaces;
using Domain.Interfaces.DimensionalityReduction;
using Domain.Resources.Types;
using Accord.MachineLearning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accord.MachineLearning.Clustering;
using Domain.HelpModels;
using Newtonsoft.Json.Linq;
using MathNet.Numerics.LinearAlgebra.Complex;
using MathNet.Numerics.LinearAlgebra;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Reflection.Emit;
using Domain.Exceptions;
using Domain.Resources.Localization.Errors;
using Microsoft.Extensions.Localization;
using System.Net;
using Microsoft.Extensions.Primitives;

namespace Domain.Services.DimensionalityReduction
{
    public class DimensionalityReductionValuesService : IDimensionalityReductionValuesService
    {
        private readonly IRepository<EmbeddingData> embeddingData_repository;
        private readonly IRepository<ClusterizationEntity> entities_repository;
        private readonly IRepository<DimensionalityReductionValue> drValues_repository;
        private readonly IRepository<EmbeddingDimensionValue> dimension_repository;
        private readonly IStringLocalizer<ErrorMessages> localizer;
        private readonly IRepository<ClusterizationWorkspaceDRTechnique> workspaceDRTechniques_repository;
        public DimensionalityReductionValuesService(IRepository<EmbeddingData> embeddingData_repository,
                                                    IRepository<ClusterizationEntity> entities_repository,
                                                    IRepository<DimensionalityReductionValue> drValues_repository,
                                                    IStringLocalizer<ErrorMessages> localizer,
                                                    IRepository<EmbeddingDimensionValue> dimension_repository,
                                                    IRepository<ClusterizationWorkspaceDRTechnique> workspaceDRTechniques_repository)
        {
            this.embeddingData_repository = embeddingData_repository;
            this.entities_repository = entities_repository;
            this.drValues_repository = drValues_repository;
            this.dimension_repository = dimension_repository;
            this.localizer = localizer;
            this.workspaceDRTechniques_repository = workspaceDRTechniques_repository;
        }
        public async Task AddEmbeddingValues(int workspaceId, string DRTechniqueId, int numberOfDimensions)
        {
            var entities = (await entities_repository.GetAsync(e => e.WorkspaceId == workspaceId, includeProperties: $"{nameof(ClusterizationEntity.EmbeddingData)},{nameof(ClusterizationEntity.DimensionalityReductionValues)}"));

            List<AddEmbeddingsWithDRHelpModel> helpModels = new List<AddEmbeddingsWithDRHelpModel>(entities.Count());

            foreach (var entity in entities)
            {
                var drValue = (await drValues_repository.GetAsync(e => e.TechniqueId == DimensionalityReductionTechniques.JSL && e.EmbeddingDataId == entity.EmbeddingDataId, includeProperties: $"{nameof(DimensionalityReductionValue.Embeddings)}")).FirstOrDefault();

                if (drValue == null) throw new HttpException(localizer[ErrorMessagePatterns.DRValueNotFound], HttpStatusCode.NotFound);

                var dimensionValue = drValue.Embeddings.First(e => e.DimensionTypeId == 1536);

                double[] embeddingValues = dimensionValue.ValuesString.Split(' ').Select(double.Parse).ToArray();
                helpModels.Add(new AddEmbeddingsWithDRHelpModel()
                {
                    Entity = entity,
                    DataPoints = embeddingValues
                });
            }

            if (helpModels.Count == 0) return;

            bool flag = true;
            var workspaceDRTechnique = (await workspaceDRTechniques_repository.GetAsync(e => e.DRTechnique.Id == DRTechniqueId && e.WorkspaceId == workspaceId, includeProperties: $"{nameof(ClusterizationWorkspaceDRTechnique.DRValues)}")).FirstOrDefault();

            if(workspaceDRTechnique == null)
            {
                ClusterizationWorkspaceDRTechnique clusterizationWorkspaceDRTechnique = new ClusterizationWorkspaceDRTechnique()
                {
                    WorkspaceId = workspaceId,
                    DRTechniqueId = DRTechniqueId
                };
                await workspaceDRTechniques_repository.AddAsync(clusterizationWorkspaceDRTechnique);
                await workspaceDRTechniques_repository.SaveChangesAsync();
                
                workspaceDRTechnique = (await workspaceDRTechniques_repository.GetAsync(e => e.DRTechnique.Id == DRTechniqueId && e.WorkspaceId == workspaceId, includeProperties: $"{nameof(ClusterizationWorkspaceDRTechnique.DRValues)}")).FirstOrDefault();
                flag = false;
            }

            bool flag2 = false;
            bool isHaveTwo = false;
            bool isHaveCurrent = false;

            bool isHaveTwoNotFull = false;
            bool isHaveCurrentNotFull = false;
            if (flag)
            {
                if (workspaceDRTechnique.DRValues.Count() != helpModels.Count)
                {
                    flag2 = true;
                    isHaveTwo = false;
                    isHaveCurrent = false;
                }
                else
                {
                    foreach(var value in workspaceDRTechnique.DRValues)
                    {
                        var dimensinalEmbeddings = (await dimension_repository.GetAsync(e => e.DimensionalityReductionValueId == value.Id, includeProperties: $"{nameof(EmbeddingDimensionValue.DimensionType)}")).ToList();

                        if (numberOfDimensions == 2)
                        {
                            if (dimensinalEmbeddings.Any(e => e.DimensionTypeId == 2))
                            {
                                isHaveTwo = true;
                                isHaveCurrent = true;
                            }
                            else if (isHaveTwo)
                            {
                                isHaveTwoNotFull = true;
                                isHaveCurrentNotFull = true;
                                break;
                            }
                        }
                        else
                        {
                            if(dimensinalEmbeddings.Any(e => e.DimensionTypeId == 2))
                            {
                                isHaveTwo = true;
                            }
                            else if (isHaveTwo)
                            {
                                isHaveCurrent = false;
                            }
                            if (dimensinalEmbeddings.Any(e => e.DimensionTypeId == numberOfDimensions))
                            {
                                isHaveCurrent = true;
                            }
                            else if (isHaveCurrent)
                            {
                                isHaveCurrentNotFull = true;
                            }
                        }
                    }
                }
            }

            if (workspaceDRTechnique.DRValues.Count() > 0)
            {
                if (flag2 || (isHaveTwoNotFull && isHaveCurrentNotFull))
                {
                    foreach (var value in workspaceDRTechnique.DRValues)
                    {
                        var dimensinalEmbeddings = (await dimension_repository.GetAsync(e => e.DimensionalityReductionValueId == value.Id, includeProperties: $"{nameof(EmbeddingDimensionValue.DimensionType)}")).ToList();

                        EmbeddingDimensionValue? deType2 = dimensinalEmbeddings.Find(e => e.DimensionTypeId == 2);

                        string includeStr = $"{nameof(EmbeddingDimensionValue.DimensionType)},{nameof(EmbeddingDimensionValue.DimensionalityReductionValue)},{nameof(EmbeddingDimensionValue.EmbeddingData)}";

                        if (deType2 != null)
                        {
                            var deType2Full = (await dimension_repository.GetAsync(e => e.Id == deType2.Id, includeProperties: includeStr)).FirstOrDefault();

                            dimension_repository.Remove(deType2Full);
                        }

                        if (numberOfDimensions != 2)
                        {
                            EmbeddingDimensionValue? deTypeCurrent = dimensinalEmbeddings.Find(e => e.DimensionTypeId == numberOfDimensions);

                            if (deTypeCurrent != null)
                            {
                                var deTypeCurrentFull = (await dimension_repository.GetAsync(e => e.Id == deTypeCurrent.Id, includeProperties: includeStr)).FirstOrDefault();

                                dimension_repository.Remove(deTypeCurrentFull);
                            }
                        }
                    }
                }
                else if (isHaveTwoNotFull)
                {
                    foreach (var value in workspaceDRTechnique.DRValues)
                    {
                        var dimensinalEmbeddings = (await dimension_repository.GetAsync(e => e.DimensionalityReductionValueId == value.Id, includeProperties: $"{nameof(EmbeddingDimensionValue.DimensionType)}")).ToList();

                        EmbeddingDimensionValue? deType2 = dimensinalEmbeddings.Find(e => e.DimensionTypeId == 2);

                        string includeStr = $"{nameof(EmbeddingDimensionValue.DimensionType)},{nameof(EmbeddingDimensionValue.DimensionalityReductionValue)},{nameof(EmbeddingDimensionValue.EmbeddingData)}";

                        if (deType2 != null)
                        {
                            var deType2Full = (await dimension_repository.GetAsync(e => e.Id == deType2.Id, includeProperties: includeStr)).FirstOrDefault();

                            dimension_repository.Remove(deType2Full);
                        }
                    }
                }
                else if (isHaveCurrentNotFull && numberOfDimensions != 2)
                {
                    foreach (var value in workspaceDRTechnique.DRValues)
                    {
                        var dimensinalEmbeddings = (await dimension_repository.GetAsync(e => e.DimensionalityReductionValueId == value.Id, includeProperties: $"{nameof(EmbeddingDimensionValue.DimensionType)}")).ToList();

                        EmbeddingDimensionValue? deType2 = dimensinalEmbeddings.Find(e => e.DimensionTypeId == 2);

                        string includeStr = $"{nameof(EmbeddingDimensionValue.DimensionType)},{nameof(EmbeddingDimensionValue.DimensionalityReductionValue)},{nameof(EmbeddingDimensionValue.EmbeddingData)}";

                        EmbeddingDimensionValue? deTypeCurrent = dimensinalEmbeddings.Find(e => e.DimensionTypeId == numberOfDimensions);

                        if (deTypeCurrent != null)
                        {
                            var deTypeCurrentFull = (await dimension_repository.GetAsync(e => e.Id == deTypeCurrent.Id, includeProperties: includeStr)).FirstOrDefault();

                            dimension_repository.Remove(deTypeCurrentFull);
                        }
                    }
                }
            }


            if ((!isHaveCurrent || isHaveCurrentNotFull) && numberOfDimensions != 1536 && numberOfDimensions != 2)
            {
                await ProjectData(helpModels, DRTechniqueId, numberOfDimensions, workspaceDRTechnique);
            }
            if (!isHaveTwo || isHaveTwoNotFull)
            {
                await ProjectData(helpModels, DRTechniqueId, 2, workspaceDRTechnique);
            }
        }
        public async Task ProjectData(List<AddEmbeddingsWithDRHelpModel> helpModels,string DRTechniqueId, int numberOfDimensions, ClusterizationWorkspaceDRTechnique clusterizationWorkspaceDRTechnique)
        {
            var values = helpModels.Select(e => e.DataPoints).ToArray();
            double[][] reducedDimensionality = new double[1][];
            if (DRTechniqueId == DimensionalityReductionTechniques.PCA)
            {
                var pca = new PrincipalComponentAnalysis()
                {
                    Method = PrincipalComponentMethod.Center,
                    Whiten = true
                };

                pca.Learn(values);

                reducedDimensionality = pca.Transform(values, numberOfDimensions);
            }
            else if (DRTechniqueId == DimensionalityReductionTechniques.t_SNE)
            {
                var tsne = new TSNE()
                {
                    NumberOfOutputs = numberOfDimensions, // The number of dimensions for the output
                    Perplexity = 30d
                };

                if (values.Length < 30)
                {
                    tsne.Perplexity = values.Length;
                }
                if (values.Length < 5)
                {
                    tsne.Perplexity = 1d;
                }

                reducedDimensionality = tsne.Transform(values);
            }
            else if (DRTechniqueId == DimensionalityReductionTechniques.LLE)
            {
                // Number of neighbors for local relationship preservation
                int numNeighbors = 10;

                // Perform LLE
                Matrix<double> matrix = Matrix<double>.Build.Dense(values.Length, values[0].Length);

                for (int i = 0; i < values.Length; i++)
                {
                    for (int j = 0; j < values[0].Length; j++)
                    {
                        matrix[i, j] = values[i][j];
                    }
                }

                Matrix<double> resMatrix = LLE(matrix, numNeighbors, numberOfDimensions);

                reducedDimensionality = new double[values.Length][];
                for (int i = 0; i < values.Length; i++)
                {
                    reducedDimensionality[i] = new double[values[0].Length];
                    for (int j = 0; j < values[0].Length; j++)
                    {
                        reducedDimensionality[i][j] = resMatrix[i, j];
                    }
                }
            }
            else
            {
                throw new HttpException(localizer[ErrorMessagePatterns.DRTechniqueNotFound], HttpStatusCode.NotFound);
            }

            for (int i = 0; i < helpModels.Count; i++)
            {
                DimensionalityReductionValue DRv = (await drValues_repository.GetAsync(e => e.ClusterizationEntityId == helpModels[i].Entity.Id, includeProperties: $"{nameof(DimensionalityReductionValue.Embeddings)}")).FirstOrDefault();

                if (DRv == null)
                {
                    DRv = new DimensionalityReductionValue()
                    {
                        ClusterizationEntity = helpModels[i].Entity,
                        TechniqueId = DRTechniqueId,
                        ClusterizationWorkspaceDRTechniqueId = clusterizationWorkspaceDRTechnique.Id
                    };
                    await drValues_repository.AddAsync(DRv);
                    await drValues_repository.SaveChangesAsync();

                    DRv = (await drValues_repository.GetAsync(e => e.ClusterizationEntityId == helpModels[i].Entity.Id, includeProperties: $"{nameof(DimensionalityReductionValue.Embeddings)}")).FirstOrDefault();
                }

                var dimensionValue = new EmbeddingDimensionValue()
                {
                    DimensionTypeId = numberOfDimensions,
                    DimensionalityReductionValue = DRv,
                };

                DRv.Embeddings.Add(dimensionValue);

                await dimension_repository.AddAsync(dimensionValue);

                string valuesString = "";
                for (int j = 0; j < numberOfDimensions; j++)
                {
                    valuesString += reducedDimensionality[i][j] + " ";
                }
                valuesString = valuesString.TrimEnd(' ');

                dimensionValue.ValuesString = valuesString;
            }

            await dimension_repository.SaveChangesAsync();
        }

        #region Perplexity
        static double EuclideanDistance(double[] pointA, double[] pointB)
        {
            double sum = 0;
            for (int i = 0; i < pointA.Length; i++)
            {
                double diff = pointA[i] - pointB[i];
                sum += diff * diff;
            }
            return Math.Sqrt(sum);
        }

        static double[] ComputeConditionalProbability(double[] point, double[][] data, double sigma)
        {
            int dataSize = data.Length;
            double[] conditionalProbabilities = new double[dataSize];

            for (int i = 0; i < dataSize; i++)
            {
                if (!point.SequenceEqual(data[i]))
                {
                    double distance = EuclideanDistance(point, data[i]);
                    double similarity = Math.Exp(-distance / (2 * sigma * sigma));
                    conditionalProbabilities[i] = similarity;
                }
            }

            double sum = conditionalProbabilities.Sum();
            for (int i = 0; i < dataSize; i++)
            {
                conditionalProbabilities[i] /= sum;
            }

            return conditionalProbabilities;
        }

        static double ShannonEntropy(double[] probabilities)
        {
            double entropy = 0;
            foreach (double p in probabilities)
            {
                entropy -= p * Math.Log(p, 2);
            }
            return entropy;
        }

        static double CalculatePerplexity(double[][] data, double targetPerplexity)
        {
            int dataSize = data.Length;
            double[] perplexities = new double[dataSize];

            for (int i = 0; i < dataSize; i++)
            {
                double[] point = data[i];
                double initialSigma = 1.0;
                double minSigma = 1e-20;
                double maxSigma = double.MaxValue;
                double sigma = initialSigma;

                // Binary search to find sigma that yields target perplexity for each data point
                int maxIterations = 50;
                for (int iter = 0; iter < maxIterations; iter++)
                {
                    double[] conditionalProbabilities = ComputeConditionalProbability(point, data, sigma);
                    double entropy = ShannonEntropy(conditionalProbabilities);
                    double currentPerplexity = Math.Pow(2, entropy);

                    double perplexityDiff = currentPerplexity - targetPerplexity;

                    if (Math.Abs(perplexityDiff) < 1e-5) // Tolerance for the target perplexity
                        break;

                    if (perplexityDiff > 0)
                        maxSigma = sigma;
                    else
                        minSigma = sigma;

                    sigma = (maxSigma + minSigma) / 2.0;
                }

                perplexities[i] = Math.Pow(2, ShannonEntropy(ComputeConditionalProbability(point, data, sigma)));
            }

            return perplexities.Average();
        }
        #endregion

        #region LLE
        public static Matrix<double> LLE(Matrix<double> data, int numNeighbors, int targetDim)
        {
            int numDataPoints = data.RowCount;
            var weights = ComputeLocalWeights(data, numNeighbors);
            var targetMatrix = ComputeTargetMatrix(weights, targetDim);

            return targetMatrix;
        }

        private static Matrix<double> ComputeLocalWeights(Matrix<double> data, int numNeighbors)
        {
            int numDataPoints = data.RowCount;
            var weights = Matrix<double>.Build.Dense(numDataPoints, numDataPoints);

            for (int i = 0; i < numDataPoints; i++)
            {
                var xi = data.Row(i);
                var distances = new double[numDataPoints];

                for (int j = 0; j < numDataPoints; j++)
                {
                    var xj = data.Row(j);
                    distances[j] = (xi - xj).L2Norm();
                }

                int[] sortedIndices = distances
                    .Select((x, index) => new { Value = x, Index = index })
                    .OrderBy(x => x.Value)
                    .Select(x => x.Index)
                    .Take(numNeighbors)
                    .ToArray();

                var Zi = Matrix<double>.Build.Dense(numNeighbors, data.ColumnCount);

                for (int j = 0; j < numNeighbors; j++)
                {
                    Zi.SetRow(j, data.Row(sortedIndices[j]) - xi);
                }

                var covMatrix = Zi.Multiply(Zi.Transpose());
                var trace = covMatrix.Trace();
                var regularization = 0.001; // Small constant to prevent singular matrix
                covMatrix = covMatrix.Add(Matrix<double>.Build.DenseIdentity(numNeighbors, numNeighbors).Multiply(regularization));

                // Check for singular covariance matrix
                double determinant = covMatrix.Determinant();
                if (Math.Abs(determinant) < 1e-10)
                {
                    // Handle the case where the covariance matrix is singular
                    // You can skip computing weights or use an alternative approach.
                }
                else
                {
                    for (int j = 0; j < numNeighbors; j++)
                    {
                        for (int k = 0; k < numNeighbors; k++)
                        {
                            weights[i, sortedIndices[j]] = -Zi.Row(j).DotProduct(Zi.Row(k)) / (covMatrix[j, k]);
                        }
                    }
                }
            }

            // Normalize the weights
            for (int i = 0; i < numDataPoints; i++)
            {
                var rowSum = weights.Row(i).Sum();
                weights.SetRow(i, weights.Row(i).Divide(rowSum));
            }

            return weights;
        }

        private static Matrix<double> ComputeTargetMatrix(Matrix<double> weights, int targetDim)
        {
            int numDataPoints = weights.RowCount;
            var identity = Matrix<double>.Build.DenseIdentity(numDataPoints);
            var m = identity.Subtract(weights);

            var eigenDecomposition = m.Evd();
            var eigenVectors = eigenDecomposition.EigenVectors;

            // Select the top targetDim eigen vectors
            var targetMatrix = eigenVectors.SubMatrix(0, numDataPoints, 0, targetDim);

            return targetMatrix;
        }
        #endregion
    }
}
