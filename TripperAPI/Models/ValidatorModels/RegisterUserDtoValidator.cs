using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripperAPI.Entities;

namespace TripperAPI.Models.ValidatorModels
{
    public class RegisterUserDtoValidator : AbstractValidator<RegisterNewUserDto>
    {
        public RegisterUserDtoValidator(DatabaseContext databaseContext)
        {

            RuleFor(e => e.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(p => p.Password)
                .MinimumLength(10);

            RuleFor(p => p.ConfirmPassword)
                .Equal(x => x.Password);

            RuleFor(e => e.Email)
                .Custom((value, context) =>
                {
                    var arleadyInUse = databaseContext.Users.Any(e => e.Email == value);
                    if (arleadyInUse)
                        context.AddFailure("Email", "That email is taken");
                });
        }
    }
}
