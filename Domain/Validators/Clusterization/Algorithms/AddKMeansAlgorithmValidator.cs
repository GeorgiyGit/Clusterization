using Domain.DTOs.ClusterizationDTOs.AlghorithmDTOs.Non_hierarchical.KMeansDTOs;
using Domain.DTOs.ClusterizationDTOs.WorkspaceDTOs.ModelDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Validators.Clusterization.Algorithms
{
    public class AddKMeansAlgorithmValidator : AbstractValidator<AddKMeansAlgorithmRequest>
    {
        public AddKMeansAlgorithmValidator() : base()
        {
            this.RuleFor(e => e.NumClusters)
                .GreaterThanOrEqualTo(1);
        }
    }
}
