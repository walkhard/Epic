using System;
using System.Collections.Generic;
using System.Linq;
using Nancy.Validation;
using Epic.ViewModels;

namespace Epic.Extensions
{
    public static class ModelValidationResultExtensions
    {
        /// <summary>
        /// Convert ModelValidationResult to ErrorViewModel.
        /// </summary>
        /// <param name="result">Result.</param>
        public static ErrorViewModel ToErrorViewModel (this ModelValidationResult result)
        {
            IEnumerable<string> errors = result.
                Errors.
                    SelectMany (i => i.MemberNames).
                    Distinct ();

            ErrorViewModel viewModel = new ErrorViewModel ();

            foreach (var error in result.Errors)
            {
                foreach (var member in error.MemberNames)
                {
                    if (!viewModel.ContainsKey (member))
                    {
                        viewModel [member] = new List<string> ();
                    }

                    viewModel [member].Add (error.GetMessage (member));
                }
            }

            return viewModel;
        }
    }
}