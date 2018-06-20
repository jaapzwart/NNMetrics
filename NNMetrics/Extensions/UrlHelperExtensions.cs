////////////////////////////////////////////////////////////////////////////////////////////////////
// file:	Extensions\UrlHelperExtensions.cs
//
// summary:	Implements the URL helper extensions class
////////////////////////////////////////////////////////////////////////////////////////////////////

using NNMetrics.Controllers;

namespace Microsoft.AspNetCore.Mvc
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   An URL helper extensions. </summary>
    ///
    /// <remarks>   Administrator, 12/06/2018. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public static class UrlHelperExtensions
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   An IUrlHelper extension method that email confirmation link. </summary>
        ///
        /// <remarks>   Administrator, 12/06/2018. </remarks>
        ///
        /// <param name="urlHelper">    The urlHelper to act on. </param>
        /// <param name="userId">       Identifier for the user. </param>
        /// <param name="code">         The code. </param>
        /// <param name="scheme">       The scheme. </param>
        ///
        /// <returns>   A string. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public static string EmailConfirmationLink(this IUrlHelper urlHelper, string userId, string code, string scheme)
        {
            return urlHelper.Action(
                action: nameof(AccountController.ConfirmEmail),
                controller: "Account",
                values: new { userId, code },
                protocol: scheme);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   An IUrlHelper extension method that resets the password callback link. </summary>
        ///
        /// <remarks>   Administrator, 12/06/2018. </remarks>
        ///
        /// <param name="urlHelper">    The urlHelper to act on. </param>
        /// <param name="userId">       Identifier for the user. </param>
        /// <param name="code">         The code. </param>
        /// <param name="scheme">       The scheme. </param>
        ///
        /// <returns>   A string. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public static string ResetPasswordCallbackLink(this IUrlHelper urlHelper, string userId, string code, string scheme)
        {
            return urlHelper.Action(
                action: nameof(AccountController.ResetPassword),
                controller: "Account",
                values: new { userId, code },
                protocol: scheme);
        }
    }
}