// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PasswordSettingsModel.cs" company="IQM Software">
//   Sumuditha Ranawaka 2014.
// </copyright>
// <summary>
//   Defines the ChangePasswordModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace IQMProjectEvolutionManager.Models
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// The change password model.
    /// </summary>
    public class PasswordSettingsModel
    {
        /// <summary>
        /// Gets or sets the old password.
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        /// <summary>
        /// Gets or sets the new password.
        /// </summary>
        /// <value>
        /// The new password.
        /// </value>
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        /// <summary>
        /// Gets or sets the confirm password.
        /// </summary>
        /// <value>
        /// The confirm password.
        /// </value>
        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [System.Web.Mvc.CompareAttribute("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}