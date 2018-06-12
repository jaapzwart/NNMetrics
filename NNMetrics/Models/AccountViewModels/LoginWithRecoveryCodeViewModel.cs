﻿////////////////////////////////////////////////////////////////////////////////////////////////////
// file:	Models\AccountViewModels\LoginWithRecoveryCodeViewModel.cs
//
// summary:	Implements the login with recovery code view model class
////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NNMetrics.Models.AccountViewModels
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   A ViewModel for the login with recovery code. </summary>
    ///
    /// <remarks>   Administrator, 12/06/2018. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public class LoginWithRecoveryCodeViewModel
    {
            ////////////////////////////////////////////////////////////////////////////////////////////////////
            /// <summary>   Gets or sets the recovery code. </summary>
            ///
            /// <value> The recovery code. </value>
            ////////////////////////////////////////////////////////////////////////////////////////////////////

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Recovery Code")]
            public string RecoveryCode { get; set; }
    }
}
