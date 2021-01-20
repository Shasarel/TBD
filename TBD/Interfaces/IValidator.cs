using System.Collections.Generic;
using TBD.Core.Validation;

namespace TBD.Interfaces
{
    public interface IValidator
    {
        IEnumerable<ValidationResult> Validate(object entity);
    }
}
