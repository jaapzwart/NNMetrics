﻿////////////////////////////////////////////////////////////////////////////////////////////////////
// file:	Models\ManageViewModels\TwoFactorAuthenticationViewModel.cs
//
// summary:	Implements the two factor authentication view model class
////////////////////////////////////////////////////////////////////////////////////////////////////

namespace NNMetrics.Models.ManageViewModels
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   A ViewModel for the two factor authentication. </summary>
    ///
    /// <remarks>   Administrator, 12/06/2018. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public class TwoFactorAuthenticationViewModel
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets a value indicating whether this object has authenticator. </summary>
        ///
        /// <value> True if this object has authenticator, false if not. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public bool HasAuthenticator { get; set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the recovery codes left. </summary>
        ///
        /// <value> The recovery codes left. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public int RecoveryCodesLeft { get; set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets a value indicating whether the 2fa is enabled. </summary>
        ///
        /// <value> True if the 2fa is enabled, false if not. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public bool Is2faEnabled { get; set; }
    }
}