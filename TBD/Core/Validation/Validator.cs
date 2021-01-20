using System;
using System.Collections.Generic;
using TBD.Interfaces;

namespace TBD.Core.Validation
{
    public abstract class Validator<T> : IValidator
    {
        public IEnumerable<ValidationResult> Validate(object entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");

            return Validate((T)entity);
        }

        protected abstract IEnumerable<ValidationResult> Validate(T entity);
    }
}
