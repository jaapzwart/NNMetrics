﻿////////////////////////////////////////////////////////////////////////////////////////////////////
// file:	Models\ManageViewModels\IndexViewModel.cs
//
// summary:	Implements the index view model class
////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NNMetrics.Models.ManageViewModels
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   A ViewModel for the index. </summary>
    ///
    /// <remarks>   Administrator, 12/06/2018. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public class IndexViewModel
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the username. </summary>
        ///
        /// <value> The username. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public string Username { get; set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Gets or sets a value indicating whether this object is email confirmed.
        /// </summary>
        ///
        /// <value> True if this object is email confirmed, false if not. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public bool IsEmailConfirmed { get; set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the email. </summary>
        ///
        /// <value> The email. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the phone number. </summary>
        ///
        /// <value> The phone number. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [Phone]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets a message describing the status. </summary>
        ///
        /// <value> A message describing the status. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public string StatusMessage { get; set; }
    }
}
