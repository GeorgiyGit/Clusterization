using Domain.DTOs.ClusterizationDTOs.WorkspaceDTOs.ModelDTOs;
using FluentValidation;

namespace Domain.Validators.Clusterization.Workspaces
{
    public class AddWorkspaceValidator : AbstractValidator<AddClusterizationWorkspaceRequest>
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
