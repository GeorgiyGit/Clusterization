using Domain.DTOs.ClusterizationDTOs.AlghorithmDTOs.Non_hierarchical.DBScanDTOs;
using Domain.DTOs.ClusterizationDTOs.AlghorithmDTOs.Non_hierarchical.KMeansDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Validators.Clusterization.Algorithms
{
    public class AddDBSCANAlgorithmValidator : AbstractValidator<AddDBSCANAlgorithmRequest>
    {
        public AddDBSCANAlgorithmValidator()
        {
            this.RuleFor(e => e.Epsilon)
                .GreaterThanOrEqualTo(0)
                .LessThanOrEqualTo(100);

            this.RuleFor(e => e.MinimumPointsPerCluster)
                .GreaterThanOrEqualTo(1);
        }
    }
}
