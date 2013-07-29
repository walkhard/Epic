using System;
using System.Linq.Expressions;
using Nancy.ViewEngines.Razor;
using Epic.ViewModels;

namespace Epic.Extensions
{
    public static class HtmlHelpersExtensions
    {
        /// <summary>
        /// Displays validation errors for specified property.
        /// </summary>
        /// <returns>Comma separated validation messages.</returns>
        /// <param name="helper">HtmlHelper.</param>
        /// <param name="property">Property.</param>
        /// <typeparam name="TModel">Model type.</typeparam>
        /// <typeparam name="TProperty">Property type.</typeparam>
        public static IHtmlString ValidationMessageFor<TModel, TProperty> (this HtmlHelpers<TModel> helper, Expression<Func<TModel, TProperty>> property)
        {
            string ret = string.Empty;

            if (helper.RenderContext.Context.Items.ContainsKey ("errors"))
            {
                MemberExpression member = (MemberExpression)property.Body;
                string propertyName = member.Member.Name;

                ErrorViewModel errors = helper.RenderContext.Context.Items ["errors"] as ErrorViewModel;

                if (errors != null && errors.ContainsKey (propertyName))
                {
                    ret = string.Join (",", errors [propertyName]);
                }
            }

            return new NonEncodedHtmlString (ret);
        }
    }
}