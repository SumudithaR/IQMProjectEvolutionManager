// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SmtpUtility.cs" company="IQm Software">
//   Sumuditha Ranawaka 2014. 
// </copyright>
// <summary>
//   Defines the SmtpUtility type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace IQMProjectEvolutionManager.Utility
{
    using System.Configuration;
    using System.Diagnostics.CodeAnalysis;
    using System.Net.Mail;
    using System.Text;

    /// <summary>
    /// The smtp utility.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
    public static class SmtpUtility
    {
        /// <summary>
        /// The send password reset email.
        /// </summary>
        /// <param name="email">
        /// The email.
        /// </param>
        /// <param name="subject">
        /// The subject.
        /// </param>
        /// <param name="body">
        /// The body.
        /// </param>
        public static void SendPasswordResetEmail(string email, string subject, string body)
        {
            var message = new MailMessage(new MailAddress(ConfigurationManager.AppSettings["SmtpFromAddress"], ConfigurationManager.AppSettings["SmptFromFriendlyName"]), new MailAddress(email)) { Subject = subject, Body = body, IsBodyHtml = true };
            var client = new SmtpClient { EnableSsl = true };
            client.Send(message);
        }
    }
}