﻿////////////////////////////////////////////////////////////////////////////////////////////////////
// file:	Models\AccountViewModels\LoginWith2faViewModel.cs
//
// summary:	Implements the login with 2fa view model class
////////////////////////////////////////////////////////////////////////////////////////////////////

using System.ComponentModel.DataAnnotations;

namespace NNMetrics.Models.AccountViewModels
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   A ViewModel for the login with 2fa. </summary>
    ///
    /// <remarks>   Administrator, 12/06/2018. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public class LoginWith2faViewModel
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the two factor code. </summary>
        ///
        /// <value> The two factor code. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [Required]
        [StringLength(7, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Text)]
        [Display(Name = "Authenticator code")]
        public string TwoFactorCode { get; set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets a value indicating whether the remember machine. </summary>
        ///
        /// <value> True if remember machine, false if not. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [Display(Name = "Remember this machine")]
        public bool RememberMachine { get; set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets a value indicating whether the remember me. </summary>
        ///
        /// <value> True if remember me, false if not. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public bool RememberMe { get; set; }
    }
}