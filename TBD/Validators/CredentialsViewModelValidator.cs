using System;
using System.Collections.Generic;
using System.Linq;
using TBD.Core.Validation;
using TBD.Enums;
using TBD.Models;

namespace TBD.Validators
{
    public sealed class UserViewModelValidator : Validator<UserViewModel>
    {
        private readonly TBDDbContext _context;
        public UserViewModelValidator(TBDDbContext context)
        {
            _context = context;
        }
        protected override IEnumerable<ValidationResult> Validate(
            UserViewModel entity)
        {
            if (entity.Login.Trim().Length == 0)
                yield return new ValidationResult("Login", "Nie może być puste");

            if (entity.Login.Trim().Contains(' '))
                yield return new ValidationResult("Login", "Nie może zawierać spacji");

            if(_context.User.Any(x => x.Login == entity.Login))
                yield return new ValidationResult("Login", $"{entity.Login} już istnieje");

            if (entity.Password.Length < 10)
                yield return new ValidationResult("Hasło", "Musi zawierać co najmiej 10 znaków");

            if (entity.Name.Trim().Length == 0)
                yield return new ValidationResult("Nazwa", "Nie może być puste");

            if(!Enum.IsDefined(typeof(Role),entity.Role))
                yield return new ValidationResult("Rola", "Nieprawidłowa wartość");

        }
    }
}
