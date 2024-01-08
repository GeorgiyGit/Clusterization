using Domain.Entities.Clusterization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.DimensionalityReduction
{
    public interface IDimensionalityReductionValuesService
    {
        public Task AddEmbeddingValues(int workspaceId, string DRTechnique, int numberOfDimensions);
    }
}
