using Domain.DTOs.ClusterizationDTOs.AlghorithmDTOs.Non_hierarchical.DBScanDTOs;
using Domain.DTOs.ClusterizationDTOs.AlghorithmDTOs.Non_hierarchical.GaussianMixtureDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Validators.Clusterization.Algorithms
{
    public class AddGaussianMixtureAlgorithmValidator : AbstractValidator<AddGaussianMixtureAlgorithmRequest>
    {
        public AddGaussianMixtureAlgorithmValidator()
        {
            this.RuleFor(e => e.NumberOfComponents)
                .GreaterThanOrEqualTo(1);
        }
    }
}
