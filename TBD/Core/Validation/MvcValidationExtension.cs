using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace TBD.Core.Validation
{
    public static class MvcValidationExtension
    {
        public static void AddModelErrors(
            this ModelStateDictionary state, ValidationException exception)
        {
            foreach (var error in exception.Errors)
            {
                state.AddModelError(error.Key, error.Message);
            }
        }
    }
}