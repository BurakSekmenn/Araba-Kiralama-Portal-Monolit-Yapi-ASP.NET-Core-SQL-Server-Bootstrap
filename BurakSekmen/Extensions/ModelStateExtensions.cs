using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BurakSekmen.Extensions
{
    public static class ModelStateExtensions
    {
        public static void AddModelErroList(this ModelStateDictionary modelState, List<string> errors)
        {
            errors.ForEach(e =>
            {
                modelState.AddModelError(string.Empty, e);
            });


        }
    }
}
