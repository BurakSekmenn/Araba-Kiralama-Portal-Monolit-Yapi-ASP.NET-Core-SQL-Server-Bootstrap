using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BurakSekmen.Extensions
{
    public static class ModelStateExtensions
    {
        public static void AddModelErroList(this ModelStateDictionary modelState, List<string> errors)
        {
            errors.ForEach(x =>
            {
                modelState.AddModelError(string.Empty, x);
            });


        }
    }
}
