////////////////////////////////////////////////////////////////////////////////////////////////////
// file:	Extensions\EmailSenderExtensions.cs
//
// summary:	Implements the email sender extensions class
////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using NNMetrics.Services;

namespace NNMetrics.Services
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   An email sender extensions. </summary>
    ///
    /// <remarks>   Administrator, 12/06/2018. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public static class EmailSenderExtensions
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// An IEmailSender extension method that sends an email confirmation asynchronous.
        /// </summary>
        ///
        /// <remarks>   Administrator, 12/06/2018. </remarks>
        ///
        /// <param name="emailSender">  The emailSender to act on. </param>
        /// <param name="email">        The email. </param>
        /// <param name="link">         The link. </param>
        ///
        /// <returns>   An asynchronous result. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public static Task SendEmailConfirmationAsync(this IEmailSender emailSender, string email, string link)
        {
            return emailSender.SendEmailAsync(email, "Confirm your email",
                $"Please confirm your account by clicking this link: <a href='{HtmlEncoder.Default.Encode(link)}'>link</a>");
        }
    }
}
