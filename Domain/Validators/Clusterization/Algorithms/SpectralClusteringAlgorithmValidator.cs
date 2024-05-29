using Domain.DTOs.ClusterizationDTOs.AlghorithmDTOs.Non_hierarchical.KMeansDTOs;
using Domain.DTOs.ClusterizationDTOs.AlghorithmDTOs.Non_hierarchical.SpectralClusteringDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Validators.Clusterization.Algorithms
{
    public class SpectralClusteringAlgorithmValidator : AbstractValidator<AddSpectralClusteringAlgorithmRequest>
    {
        public SpectralClusteringAlgorithmValidator()
        {
            this.RuleFor(e => e.NumClusters)
                .GreaterThanOrEqualTo(1);

            this.RuleFor(e => e.Gamma)
                .GreaterThan(0);
        }
    }
}
