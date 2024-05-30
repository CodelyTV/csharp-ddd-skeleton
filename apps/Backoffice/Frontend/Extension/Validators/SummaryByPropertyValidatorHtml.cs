using System.Globalization;
using System.Text;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CodelyTv.Apps.Backoffice.Frontend.Extension.Validators
{
    public static class SummaryByPropertyValidatorHtml
    {
        public static IHtmlContent ValidationSummaryByProperty<TModel>(this IHtmlHelper<TModel> helper,
            ModelStateDictionary dictionary, string property, string className)
        {
            if (helper == null)
            {
                throw new ArgumentNullException(nameof(helper));
            }

            if (dictionary == null)
            {
                throw new ArgumentNullException(nameof(dictionary));
            }

            if (string.IsNullOrEmpty(property))
            {
                throw new ArgumentNullException(nameof(property));
            }

            if (className == null)
            {
                throw new ArgumentNullException(nameof(className));
            }

            var builder = new StringBuilder();

            if (dictionary.TryGetValue(property, out var modelStateEntry) && modelStateEntry?.Errors != null)
            {
                foreach (var modelState in modelStateEntry.Errors)
                {
                    builder.Append(CultureInfo.CurrentCulture, $"<p class='{className}'>{modelState.ErrorMessage}</p>");
                }
            }

            return new HtmlString(builder.ToString());
        }
    }
}
