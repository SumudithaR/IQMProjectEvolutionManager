// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ResetPasswordModel.cs" company="IQM Software">
//   Sumuditha Ranawaka 2014.
// </copyright>
// <summary>
//   The reset password model.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace IQMProjectEvolutionManager.Models
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// The reset password model.
    /// </summary>
    public class ForgotPasswordModel
    {
        /// <summary>
        /// Gets or sets the user name.
        /// </summary>
        [Required]
        [Display(Name = "Username")]
        public string UserName { get; set; }
    }
}