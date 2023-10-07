using Domain.DTOs.ClusterizationDTOs.WorkspaceDTOs.ModelDTOs;
using FluentValidation;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Validators.Clusterization.Workspaces
{
    public class AddWorkspaceValidator : AbstractValidator<AddClusterizationWorkspaceDTO>
    {
        public AddWorkspaceValidator() : base()
        {
            this.RuleFor(e => e.Title)
                .NotEmpty()
                .NotNull()
                .MaximumLength(100);

            this.RuleFor(e => e.Description)
                .MaximumLength(3000);

            this.RuleFor(e => e.TypeId)
                .NotNull()
                .NotEmpty();
        }
    }
}
