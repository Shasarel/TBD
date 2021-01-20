using System;
using System.Collections.ObjectModel;

namespace TBD.Core.Validation
{
    public class ValidationException : Exception
    {
        public ValidationException(ValidationResult[] r) : base(r[0].Message)
        {
            Errors = new ReadOnlyCollection<ValidationResult>(r);
        }

        public ReadOnlyCollection<ValidationResult> Errors { get; }
    }
}
