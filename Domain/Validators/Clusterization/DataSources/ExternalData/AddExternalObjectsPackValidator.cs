using Domain.DTOs.ExternalData;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Validators.Clusterization.DataSources.ExternalData
{
    public class AddExternalObjectsPackValidator : AbstractValidator<AddExternalDataRequest>
    {
        public AddExternalObjectsPackValidator()
        {
            this.RuleFor(e => e.Title)
                .MaximumLength(100);

            this.RuleFor(e => e.Description)
                .MaximumLength(3000);
        }
    }
}
