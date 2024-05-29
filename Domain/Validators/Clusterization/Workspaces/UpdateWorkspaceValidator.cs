using Domain.DTOs.ClusterizationDTOs.WorkspaceDTOs.ModelDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Validators.Clusterization.Workspaces
{
    public class UpdateWorkspaceValidator: AbstractValidator<UpdateClusterizationWorkspaceRequest>
    {
        public UpdateWorkspaceValidator()
        {
            this.RuleFor(e => e.Title)
                .NotEmpty()
                .NotNull()
                .MaximumLength(100);

            this.RuleFor(e => e.Description)
                .MaximumLength(3000);
        }
    }
}
