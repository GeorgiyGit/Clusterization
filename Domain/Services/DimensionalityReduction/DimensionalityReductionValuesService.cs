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

namespace Domain.Services.DimensionalityReduction
{
    public class DimensionalityReductionValuesService : IDimensionalityReductionValuesService
    {
        private readonly IRepository<EmbeddingData> embeddingData_repository;
        private readonly IRepository<EmbeddingValue> embeddingValues_repository;
        private readonly IRepository<ClusterizationEntity> entities_repository;
        private readonly IRepository<DimensionalityReductionValue> drValues_repository;
        private readonly IRepository<EmbeddingDimensionValue> dimension_repository;
        private readonly IStringLocalizer<ErrorMessages> localizer;
        public DimensionalityReductionValuesService(IRepository<EmbeddingData> embeddingData_repository,
                                                    IRepository<ClusterizationEntity> entities_repository,
                                                    IRepository<DimensionalityReductionValue> drValues_repository,
                                                    IRepository<EmbeddingValue> embeddingValues_repository,
                                                    IStringLocalizer<ErrorMessages> localizer,
                                                    IRepository<EmbeddingDimensionValue> dimension_repository)
        {
            this.embeddingData_repository = embeddingData_repository;
            this.entities_repository = entities_repository;
            this.drValues_repository = drValues_repository;
            this.embeddingValues_repository = embeddingValues_repository;
            this.dimension_repository = dimension_repository;
            this.localizer = localizer;
        }
        public async Task AddEmbeddingValues(int workspaceId, string DRTechniqueId)
        {
            var entities = (await entities_repository.GetAsync(e => e.WorkspaceId == workspaceId, includeProperties: $"{nameof(ClusterizationEntity.EmbeddingData)},{nameof(ClusterizationEntity.DimensionalityReductionValues)}"));

            List<AddEmbeddingsWithDRHelpModel> helpModels = new List<AddEmbeddingsWithDRHelpModel>(entities.Count());

            foreach (var entity in entities)
            {
                var value = await drValues_repository.GetAsync(e => e.ClusterizationEntityId == entity.Id && e.TechniqueId == DRTechniqueId);

                if (value == null)
                {
                    var embeddingValues = (await embeddingValues_repository.GetAsync(e => e.EmbeddingDimensionValue.EmbeddingDataId == entity.EmbeddingDataId));

                    helpModels.Add(new AddEmbeddingsWithDRHelpModel()
                    {
                        Entity = entity,
                        DataPoints = embeddingValues.Select(e => e.Value).ToArray()
                    });
                }
            }

            int numberOfDimensions = 2; // Desired number of dimensions

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
                    Perplexity = 30.0,
                };

                reducedDimensionality = tsne.Transform(values);
            }
            else if (DRTechniqueId == DimensionalityReductionTechniques.LLE)
            {
                // Number of neighbors for local relationship preservation
                int numNeighbors = 10;

                // Perform LLE
                double[][] lleResult = LLE(values, numNeighbors, numberOfDimensions);
            }
            else
            {
                throw new HttpException(localizer[ErrorMessagePatterns.DRTechniqueNotFound], HttpStatusCode.NotFound);
            }

            for (int i = 0; i < helpModels.Count; i++)
            {
                var DRv = new DimensionalityReductionValue()
                {
                    ClusterizationEntity = helpModels[i].Entity,
                    TechniqueId = DRTechniqueId
                };

                await drValues_repository.AddAsync(DRv);

                var dimensionValue = new EmbeddingDimensionValue()
                {
                    DimensionTypeId = numberOfDimensions,
                    DimensionalityReductionValue = DRv,
                };

                DRv.Embeddings.Add(dimensionValue);

                await dimension_repository.AddAsync(dimensionValue);

                for (int j = 0; j < 2; i++)
                {
                    var value = new EmbeddingValue()
                    {
                        EmbeddingDimensionValue = dimensionValue,
                        Value = reducedDimensionality[i][j]
                    };
                    dimensionValue.Values.Add(value);

                    await embeddingValues_repository.AddAsync(value);
                }
            }

            await dimension_repository.SaveChangesAsync();
        }
        #region LLE
        // Perform LLE
        static double[][] LLE(double[][] data, int numNeighbors, int numberOfDimensions)
        {
            int n = data.Length;
            int m = data[0].Length;

            // Create a distance matrix
            var distanceMatrix = new double[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    distanceMatrix[i, j] = CalculateEuclideanDistance(data[i], data[j]);
                }
            }

            // Create a weight matrix for local linear reconstruction
            var weightMatrix = new double[n, n];
            for (int i = 0; i < n; i++)
            {
                var neighbors = GetNearestNeighbors(i, distanceMatrix, numNeighbors);

                // Create a Matrix<double> from data for local linear reconstruction
                var x = Matrix<double>.Build.Dense(n, m, (row, col) => data[row][col]);

                var xi = x.Row(i);

                var H = Matrix<double>.Build.Dense(neighbors.Length, m, (row, col) => data[neighbors[row]][col]);
                H = H.Subtract(xi.ToRowMatrix());

                var G = H.Multiply(H.Transpose());

                var w = G.Inverse().Multiply(1.0);
                w = w.Divide(w.RowSums()[0]);

                for (int j = 0; j < numNeighbors; j++)
                {
                    weightMatrix[i, neighbors[j]] = w[j, 0];
                }
            }

            // Create the matrix that minimizes the cost function
            var I = Matrix<double>.Build.DenseIdentity(n);
            var M = I.Subtract(Matrix<double>.Build.DenseOfArray(weightMatrix));
            M = M.TransposeThisAndMultiply(M);

            // Perform eigenvalue decomposition
            var evd = M.Evd();
            var eigenvectors = evd.EigenVectors.ToColumnArrays();

            // Take the desired number of dimensions
            var lleResult = new double[n][];
            for (int i = 0; i < n; i++)
            {
                lleResult[i] = new double[numberOfDimensions];
                for (int j = 0; j < numberOfDimensions; j++)
                {
                    lleResult[i][j] = eigenvectors[j][i];
                }
            }

            return lleResult;
        }

        // Function to calculate Euclidean distance between two points
        static double CalculateEuclideanDistance(double[] point1, double[] point2)
        {
            double sumOfSquaredDifferences = 0;
            for (int i = 0; i < point1.Length; i++)
            {
                double diff = point1[i] - point2[i];
                sumOfSquaredDifferences += diff * diff;
            }
            return Math.Sqrt(sumOfSquaredDifferences);
        }

        // Function to get the indices of the nearest neighbors
        static int[] GetNearestNeighbors(int dataIndex, double[,] distanceMatrix, int numNeighbors)
        {
            int n = distanceMatrix.GetLength(0);
            var distances = new double[n];
            for (int i = 0; i < n; i++)
            {
                distances[i] = distanceMatrix[dataIndex, i];
            }

            var sortedIndices = distances.Select((value, index) => new { Value = value, Index = index })
                .OrderBy(x => x.Value)
                .Select(x => x.Index)
                .ToArray();

            return sortedIndices.Skip(1).Take(numNeighbors).ToArray();
        }
        #endregion
    }
}
