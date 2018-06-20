////////////////////////////////////////////////////////////////////////////////////////////////////
// file:	Views\Manage\ManageNavPages.cs
//
// summary:	Implements the manage navigation pages class
////////////////////////////////////////////////////////////////////////////////////////////////////

using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;

namespace NNMetrics.Views.Manage
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   A manage navigation pages. </summary>
    ///
    /// <remarks>   Administrator, 12/06/2018. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public static class ManageNavPages
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets the active page key. </summary>
        ///
        /// <value> The active page key. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public static string ActivePageKey => "ActivePage";

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets the zero-based index of this object. </summary>
        ///
        /// <value> The index. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public static string Index => "Index";

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets the change password. </summary>
        ///
        /// <value> The change password. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public static string ChangePassword => "ChangePassword";

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets the external logins. </summary>
        ///
        /// <value> The external logins. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public static string ExternalLogins => "ExternalLogins";

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets the two factor authentication. </summary>
        ///
        /// <value> The two factor authentication. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public static string TwoFactorAuthentication => "TwoFactorAuthentication";

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Index navigation class. </summary>
        ///
        /// <remarks>   Administrator, 12/06/2018. </remarks>
        ///
        /// <param name="viewContext">  Context for the view. </param>
        ///
        /// <returns>   A string. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public static string IndexNavClass(ViewContext viewContext) => PageNavClass(viewContext, Index);

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Change password navigation class. </summary>
        ///
        /// <remarks>   Administrator, 12/06/2018. </remarks>
        ///
        /// <param name="viewContext">  Context for the view. </param>
        ///
        /// <returns>   A string. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public static string ChangePasswordNavClass(ViewContext viewContext) => PageNavClass(viewContext, ChangePassword);

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   External logins navigation class. </summary>
        ///
        /// <remarks>   Administrator, 12/06/2018. </remarks>
        ///
        /// <param name="viewContext">  Context for the view. </param>
        ///
        /// <returns>   A string. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public static string ExternalLoginsNavClass(ViewContext viewContext) => PageNavClass(viewContext, ExternalLogins);

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Two factor authentication navigation class. </summary>
        ///
        /// <remarks>   Administrator, 12/06/2018. </remarks>
        ///
        /// <param name="viewContext">  Context for the view. </param>
        ///
        /// <returns>   A string. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public static string TwoFactorAuthenticationNavClass(ViewContext viewContext) => PageNavClass(viewContext, TwoFactorAuthentication);

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Page navigation class. </summary>
        ///
        /// <remarks>   Administrator, 12/06/2018. </remarks>
        ///
        /// <param name="viewContext">  Context for the view. </param>
        /// <param name="page">         The page. </param>
        ///
        /// <returns>   A string. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public static string PageNavClass(ViewContext viewContext, string page)
        {
            var activePage = viewContext.ViewData["ActivePage"] as string;
            return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : null;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// A ViewDataDictionary extension method that adds an active page to 'activePage'.
        /// </summary>
        ///
        /// <remarks>   Administrator, 12/06/2018. </remarks>
        ///
        /// <param name="viewData">     The viewData to act on. </param>
        /// <param name="activePage">   The active page. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public static void AddActivePage(this ViewDataDictionary viewData, string activePage) => viewData[ActivePageKey] = activePage;
    }
}