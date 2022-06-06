using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripperAPI.Models.ValidatorModels
{
    public class PlaceQueryValidator : AbstractValidator<QueryParameters>
    {
        private int[] allowedPageSizes = new[] { 5, 10, 15 };
        public PlaceQueryValidator()
        {
            RuleFor(r => r.pageNumber).GreaterThanOrEqualTo(1);
            RuleFor(r => r.pageSize).Custom((value, context) =>
            {
                if (!allowedPageSizes.Contains(value))
                {
                    context.AddFailure("PageSize", $"Allowed page sizes: [{string.Join(",", allowedPageSizes)}]");
                }
            });
        }
    }
}
