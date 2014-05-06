// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AccountController.cs" company="IQm Software">
//   Sumuditha Ranawaka 2014. 
// </copyright>
// <summary>
//   The accounts controller
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace IQMProjectEvolutionManager.Controllers
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Text.RegularExpressions;
    using System.Web.Mvc;
    using System.Web.Security;

    using FiftyOne.Foundation.UI.Web;

    using FluentNHibernate;

    using IQMProjectEvolutionManager.Core.Domain;
    using IQMProjectEvolutionManager.Core.DTO;
    using IQMProjectEvolutionManager.Core.Interfaces.Services;
    using IQMProjectEvolutionManager.Models;
    using IQMProjectEvolutionManager.Utility;

    using WebMatrix.WebData;

    /// <summary>
    /// The accounts controller
    /// </summary>
    public class AccountController : Controller
    {
        /// <summary>
        /// Logins the specified return URL.
        /// </summary>
        /// <param name="returnUrl">The return URL.</param>
        /// <returns>The ActionResult</returns>
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            // var provider = Membership.Provider;
            // string name = provider.ApplicationName;
            ViewBag.ReturnUrl = returnUrl;
            return this.View();
        }

        /// <summary>
        /// Logins the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="returnUrl">The return URL.</param>
        /// <returns> Action Result </returns>
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            if (Membership.ValidateUser(model.UserName, model.Password))
            {
                FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                if (Url.IsLocalUrl(returnUrl))
                {
                    return this.Redirect(returnUrl);
                }

                return this.RedirectToAction("Index", "Project");
            }

            ModelState.AddModelError(string.Empty, "The user name or password provided is incorrect. Please try again.");

            // If we got this far, something failed, redisplay form
            return this.View(model);
        }

        /// <summary>
        /// Registers a user.
        /// </summary>
        /// <returns>The ActionResult</returns>
        [AllowAnonymous]
        public ActionResult Register()
        {
            return this.View(new RegisterModel());
        }

        /// <summary>
        /// Registers the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns> Action Result </returns>
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            this.ValidateEmailNotificationDetails(model.Email, model.CalendarSubscriber);
            this.ValidateSmsNotificationDetails(model.Mobile, model.SmsNotificationPeriod, model.SmsSubscriber);

            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            MembershipCreateStatus status;
            var membershipUser = Membership.CreateUser(model.UserName, model.Password, model.Email, "Question1", "Answer1", true, out status);
            switch (status)
            {
                case MembershipCreateStatus.Success:
                    this.CreateSubscription(model, membershipUser);
                    return this.RedirectToAction("Login");
                case MembershipCreateStatus.DuplicateUserName:
                    ModelState.AddModelError(string.Empty, "Sorry another user with the username '" + model.UserName + "' has already been registered. Please try again with a different username.");
                    break;
                default:
                    ModelState.AddModelError(string.Empty, "Sorry the user was not created successfully. Please try again.");
                    break;
            }

            return this.View(model);
        }

        /// <summary>
        /// The forgot password.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            this.ViewData.Add("Message", string.Empty);
            return this.View(new ForgotPasswordModel());
        }

        /// <summary>
        /// The forgot password.
        /// </summary>
        /// <param name="model">
        /// The model.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [AllowAnonymous]
        [HttpPost]
        public ActionResult ForgotPassword(ForgotPasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            var currentUser = Membership.GetUser(model.UserName);

            if (currentUser == null)
            {
                this.ModelState.AddModelError(string.Empty, "Sorry, no such user exists on the system.");
                return this.View(model);
            }

            var resetLink = "<a href='" + Url.Action("ResetPassword", "Account", new { userName = model.UserName }, "http") + "'>Reset Password</a>";

            try
            {
                SmtpUtility.SendPasswordResetEmail(currentUser.Email, "Password Reset Token", "<b>Please find the Password Reset Token</b><br/>" + resetLink);
                this.ViewData.Add("Message", "A link to reset the password has been sent to the user's email.");
            }
            catch (Exception ex)
            {
                this.ViewData.Add("Message", "Error occurred while sending the password reset email to the user. Please try again." + ex.Message);
            }

            this.ModelState.Clear();
            return this.View(new ForgotPasswordModel());
        }

        /// <summary>
        /// The reset password.
        /// </summary>
        /// <param name="userName">
        /// The user name.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [AllowAnonymous]
        public ActionResult ResetPassword(string userName)
        {
            var currentUser = Membership.GetUser(userName);

            if (currentUser == null)
            {
                this.ViewData.Add("Message", "Sorry, no such user exists on the system.");
                return this.View();
            }

            var newPassword = GetRandomPassword(6);
            var response = currentUser.ChangePassword(currentUser.ResetPassword(), newPassword);

            if (response)
            {
                try
                {
                    SmtpUtility.SendPasswordResetEmail(currentUser.Email, "New Password", "<b>Please find the New Password</b><br/>" + newPassword);
                }
                catch (Exception ex)
                {
                    this.ViewData.Add("Message", "Error occurred while sending the new password to the user's email." + ex.Message);
                }

                this.ViewData.Add("Message", "Success! A new password has been sent to the user's email. Your New Password Is " + newPassword);
            }
            else
            {
                this.ViewData.Add("Message", "Please, avoid random request on this page.");
            }

            return this.View();
        }

        /// <summary>
        /// The update details.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult UserSettings()
        {
            return this.View();
        }

        /// <summary>
        /// Passwords the settings.
        /// </summary>
        /// <returns> Action Result </returns>
        public ActionResult PasswordSettings()
        {
            return this.PartialView(new PasswordSettingsModel());
        }

        /// <summary>
        /// Passwords the settings.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns> Action Result </returns>
        [HttpPost]
        public ActionResult PasswordSettings(PasswordSettingsModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.PartialView(model);
            }

            var currentUser = Membership.GetUser(User.Identity.Name);

            if (currentUser != null && currentUser.ChangePassword(model.OldPassword, model.NewPassword))
            {
                Membership.UpdateUser(currentUser);
                this.ModelState.Clear();
                return this.PartialView(new PasswordSettingsModel());
            }

            this.ModelState.AddModelError(string.Empty, "Sorry, the new password did not save successfully. This could be due to an invalid old password. Please try again.");

            return this.PartialView(model);
        }

        /// <summary>
        /// Notifications the settings.
        /// </summary>
        /// <returns> Action Result </returns>
        public ActionResult NotificationSettings()
        {
            //var subscription =
            //    DependencyResolver.Current.GetService<ISubscriberService>().GetByUserName(User.Identity.Name);
            //var currentUser = Membership.GetUser(User.Identity.Name);

            //if (currentUser == null)
            //{
            //    return this.PartialView(new NotificationSettingsModel());
            //}

            //var model = new NotificationSettingsModel
            //                {
            //                    CalendarSubscriber = subscription.IsCalendarSubscriber,
            //                    SmsSubscriber = subscription.IsSmsSubscriber,
            //                    Email = currentUser.Email,
            //                    Mobile = subscription.Mobile,
            //                    SmsNotificationPeriod = subscription.SmsNotificationPeriod
            //                };

            return this.PartialView(new NotificationSettingsModel());
        }

        /// <summary>
        /// Notifications the settings.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns> Action Result </returns>
        [HttpPost]
        public ActionResult NotificationSettings(NotificationSettingsModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.PartialView(model);
            }

            this.UpdateSubscription(model);

            if (!ModelState.IsValid)
            {
                return this.PartialView(model);
            }

            var currentUser = Membership.GetUser(User.Identity.Name);

            if (currentUser == null)
            {
                this.ModelState.AddModelError(string.Empty, "Sorry, the system failed to find the logged in user. Please try again after signing out and logging back in.");
                return this.PartialView(model);
            }

            currentUser.Email = model.Email;
            Membership.UpdateUser(currentUser);

            this.ModelState.Clear();
            return this.PartialView(new NotificationSettingsModel());
        }

        /// <summary>
        /// Logs the off.
        /// </summary>
        /// <returns> Action Result </returns>
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return this.RedirectToAction("Index", "Project");
        }

        /// <summary>
        /// Creates the subscription.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="user">The user.</param>
        public void CreateSubscription(RegisterModel model, MembershipUser user)
        {
            var subscriberNotifierService = DependencyResolver.Current.GetService<ISubscriberNotifierService>();
            var subscriberNotifierTypeService = DependencyResolver.Current.GetService<ISubscriberNotifierTypeService>();
            var subscriberNotifierPurposeService =
                DependencyResolver.Current.GetService<ISubscriberNotifierPurposeService>();

            if (user.ProviderUserKey == null)
            {
                return;
            }

            var subscriber = new Subscriber
                                 {
                                     UserName = user.UserName,
                                     Mobile = model.Mobile,
                                     Email = model.Email,
                                     IsCalendarSubscriber = model.CalendarSubscriber,
                                     IsSmsSubscriber = model.SmsSubscriber,
                                     SmsNotificationPeriod =
                                         (model.SmsNotificationPeriod != null)
                                             ? (int)model.SmsNotificationPeriod
                                             : 0
                                 };

            if (subscriber.IsCalendarSubscriber)
            {
                subscriberNotifierService.InsertOrUpdate(
                    new SubscriberNotifier
                        {
                            Subscriber = subscriber,
                            SubscriberNotifierPurpose =
                                subscriberNotifierPurposeService.GetByName("Release"),
                            SubscriberNotifierType =
                                subscriberNotifierTypeService.GetByName("Calendar")
                        });
            }

            if (subscriber.IsSmsSubscriber)
            {
                subscriberNotifierService.InsertOrUpdate(
                    new SubscriberNotifier
                        {
                            Subscriber = subscriber,
                            SubscriberNotifierPurpose =
                                subscriberNotifierPurposeService.GetByName("Release"),
                            SubscriberNotifierType = subscriberNotifierTypeService.GetByName("SMS"),
                        });
            }
        }

        /// <summary>
        /// The update subscription.
        /// </summary>
        /// <param name="model">
        /// The model.
        /// </param>
        public void UpdateSubscription(NotificationSettingsModel model)
        {
            if (model != null)
            {
                return;
            }

            var subscriberService = DependencyResolver.Current.GetService<ISubscriberService>();
            var currentSubscriber = subscriberService.GetByUserName(this.User.Identity.Name);

            if (currentSubscriber == null)
            {
                this.ModelState.AddModelError(
                    string.Empty,
                    "Sorry, the system failed to find the subscription for the logged in user. Please try again after signing out and logging back in.");
            }

            currentSubscriber.Mobile = model.Mobile;
            currentSubscriber.Email = model.Email;
            currentSubscriber.IsSmsSubscriber = model.SmsSubscriber;
            currentSubscriber.SmsNotificationPeriod = (model.SmsNotificationPeriod != null)
                                                          ? (int)model.SmsNotificationPeriod
                                                          : 0;
            currentSubscriber.IsCalendarSubscriber = model.CalendarSubscriber;

            subscriberService.InsertOrUpdate(currentSubscriber);
        }

        /// <summary>
        /// The validate email notification details.
        /// </summary>
        /// <param name="email">
        /// The email.
        /// </param>
        /// <param name="calendarSubscriber">
        /// The calendar subscriber.
        /// </param>
        public void ValidateEmailNotificationDetails(string email, bool calendarSubscriber)
        {
            if (email == null || email.Equals(string.Empty)
                || (!Regex.Match(email, "^[A-Za-z0-9._%+-]+@gmail.com$").Success && !Regex.Match(email, "^[A-Za-z0-9._%+-]+@googlemail.com$").Success) && calendarSubscriber)
            {
                ModelState.AddModelError(
                    string.Empty,
                    "Please enter a Google email address to receive calendar updates. If you don't wish to receive calendar updates please uncheck 'Subscribe to Google Calendar updates'.");
            }
        }

        /// <summary>
        /// The get random password.
        /// </summary>
        /// <param name="length">
        /// The length.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private static string GetRandomPassword(int length)
        {
            const string CharSet = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789!@$?_-*&#+";
            var passChars = new char[length];
            var random = new Random();

            for (var i = 0; i < length; i++)
            {
                passChars[i] = CharSet[random.Next(0, CharSet.Length)];
            }

            return new string(passChars);
        }

        /// <summary>
        /// The validate sms notification details.
        /// </summary>
        /// <param name="mobile">
        /// The mobile.
        /// </param>
        /// <param name="notificationPeriod">
        /// The notification period.
        /// </param>
        /// <param name="smsSubscriber">
        /// The sms subscriber.
        /// </param>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        public void ValidateSmsNotificationDetails(string mobile, int? notificationPeriod, bool smsSubscriber)
        {
            if ((mobile == null || mobile.Equals(string.Empty)) && smsSubscriber)
            {
                ModelState.AddModelError(string.Empty, "Please enter a mobile number to receive SMS updates. If you don't wish to receive SMS updates please uncheck 'Subscribe to SMS updates'.");
            }

            if (notificationPeriod == null && smsSubscriber)
            {
                ModelState.AddModelError(string.Empty, "Please select a notification period to receive SMS updates. If you don't wish to receive SMS updates please uncheck 'Subscribe to SMS updates'.");
            }
        }

        /// <summary>
        /// The profiles.
        /// </summary>
        /// <returns>
        /// The <see cref="JsonResult"/>.
        /// </returns>
        public JsonResult Profiles()
        {
            return this.Json(new RoleProfiles(), JsonRequestBehavior.AllowGet);
        }
    }
}