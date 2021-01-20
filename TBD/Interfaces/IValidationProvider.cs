using System.Collections;

namespace TBD.Interfaces
{
    public interface IValidationProvider
    {
        void Validate(object entity);
        void ValidateAll(IEnumerable entities);
    }
}
