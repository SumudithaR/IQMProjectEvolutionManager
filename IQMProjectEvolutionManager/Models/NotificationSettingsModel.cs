// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NotificationSettingsModel.cs" company="IQM Software">
//   Sumuditha Ranawaka 2014.
// </copyright>
// <summary>
//   The notification settings model.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace IQMProjectEvolutionManager.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    /// <summary>
    /// The notification settings model.
    /// </summary>
    public class NotificationSettingsModel
    {
        /// <summary>
        /// The valid notification periods
        /// </summary>
        private readonly List<int> validNotificationPeriods = new List<int> { 5, 10, 15, 30 };

        /// <summary>
        /// Gets the notification periods.
        /// </summary>
        /// <value>
        /// The notification periods.
        /// </value>
        public IEnumerable<SelectListItem> NotificationPeriods
        {
            get { return new SelectList(this.validNotificationPeriods); }
        }

        /// <summary>
        /// Gets or sets the mobile.
        /// </summary>
        /// <value>
        /// The mobile.
        /// </value>
        [RegularExpression("^07[0-9]{9}$", ErrorMessage = "Please enter a valid mobile number with the country code.")]
        [Display(Name = "Mobile (In format - 07XXXXXXXXX)")]
        public string Mobile { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        [Required]
        [RegularExpression("^[A-Za-z0-9._%+-]+@[A-Za-z0-9._%+-]+.[a-z]{2,4}$", ErrorMessage = "Please enter a valid email address.")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [SMS subscriber].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [SMS subscriber]; otherwise, <c>false</c>.
        /// </value>
        [Required]
        [Display(Name = "Subscribe to SMS updates")]
        public bool SmsSubscriber { get; set; }

        /// <summary>
        /// Gets or sets the SMS notification period.
        /// </summary>
        /// <value>
        /// The SMS notification period.
        /// </value>
        [Display(Name = "SMS notification period")]
        public int? SmsNotificationPeriod { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [calendar subscriber].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [calendar subscriber]; otherwise, <c>false</c>.
        /// </value>
        [Required]
        [Display(Name = "Subscribe to Google Calendar updates")]
        public bool CalendarSubscriber { get; set; }
    }
}