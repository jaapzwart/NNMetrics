////////////////////////////////////////////////////////////////////////////////////////////////////
// file:	Services\EmailSender.cs
//
// summary:	Implements the email sender class
////////////////////////////////////////////////////////////////////////////////////////////////////

using System.Threading.Tasks;

namespace NNMetrics.Services
{
    // This class is used by the application to send email for account confirmation and password reset.
    // For more details see https://go.microsoft.com/fwlink/?LinkID=532713

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   An email sender. </summary>
    ///
    /// <remarks>   Administrator, 12/06/2018. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public class EmailSender : IEmailSender
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Sends an email asynchronous. </summary>
        ///
        /// <remarks>   Administrator, 12/06/2018. </remarks>
        ///
        /// <param name="email">    The email. </param>
        /// <param name="subject">  The subject. </param>
        /// <param name="message">  The message. </param>
        ///
        /// <returns>   An asynchronous result. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public Task SendEmailAsync(string email, string subject, string message)
        {
            return Task.CompletedTask;
        }
    }
}