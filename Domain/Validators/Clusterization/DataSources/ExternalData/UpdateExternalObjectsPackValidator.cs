using Domain.DTOs.DataSourcesDTOs.ExternalDataDTOs.Requests;
using Domain.DTOs.ExternalData;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Validators.Clusterization.DataSources.ExternalData
{
    public class UpdateExternalObjectsPackValidator : AbstractValidator<UpdateExternalDataPackRequest>
    {
        public UpdateExternalObjectsPackValidator()
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
