using System;
using System.Collections;
using System.Linq;
using TBD.Interfaces;

namespace TBD.Core.Validation
{
    sealed class ValidationProvider : IValidationProvider
    {
        private readonly Func<Type, IValidator> _validatorFactory;

        public ValidationProvider(Func<Type, IValidator> validatorFactory)
        {
            _validatorFactory = validatorFactory;
        }

        public void Validate(object entity)
        {
            var validator = _validatorFactory(entity.GetType());
            var results = validator.Validate(entity).ToArray();

            if (results.Length > 0)
                throw new ValidationException(results);
        }

        public void ValidateAll(IEnumerable entities)
        {
            var results = (
                    from entity in entities.Cast<object>()
                    let validator = this._validatorFactory(entity.GetType())
                    from result in validator.Validate(entity)
                    select result)
                .ToArray();

            if (results.Length > 0)
                throw new ValidationException(results);
        }
    }
}
