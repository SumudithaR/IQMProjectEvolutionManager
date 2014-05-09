using System;
using System.Collections.Generic;
using System.Configuration;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace IQMProjectEvolutionManager.Tests.UI.Views
{
    [TestFixture]
    public class AccountTestFixture
    {
        private IWebDriver _driver;
        private WebDriverWait _driverWait;
        private string _baseUrl;

        /// <summary>
        /// Registers a user with the specified registration details.
        /// </summary>
        /// <param name="registrationDetails">The registration details.</param>
        public void Register(Dictionary<string, string> registrationDetails)
        {
            if (registrationDetails != null)
            {
                _driver.Navigate().GoToUrl(_baseUrl);
                _driverWait.Until(ExpectedConditions.ElementIsVisible(By.LinkText("Register")));
                _driver.FindElement(By.LinkText("Register")).Click();

                if (registrationDetails.ContainsKey("Username") && !registrationDetails["Username"].Equals(string.Empty))
                {
                    var usernameField = _driver.FindElement(By.Id("UserName"));
                    usernameField.Clear();
                    usernameField.SendKeys(registrationDetails["Username"]);
                    usernameField.SendKeys(Keys.Tab);
                }

                if (registrationDetails.ContainsKey("Password") && !registrationDetails["Password"].Equals(string.Empty))
                {
                    var passwordField = _driver.FindElement(By.Id("Password"));
                    passwordField.Clear();
                    passwordField.SendKeys(registrationDetails["Password"]);
                }

                if (registrationDetails.ContainsKey("ConfirmPassword") && !registrationDetails["ConfirmPassword"].Equals(string.Empty))
                {
                    var confirmPasswordField = _driver.FindElement(By.Id("ConfirmPassword"));
                    confirmPasswordField.Clear();
                    confirmPasswordField.SendKeys(registrationDetails["ConfirmPassword"]);
                }

                if (registrationDetails.ContainsKey("Mobile") && !registrationDetails["Mobile"].Equals(string.Empty))
                {
                    var mobileField = _driver.FindElement(By.Id("Mobile"));
                    mobileField.Clear();
                    mobileField.SendKeys(registrationDetails["Mobile"]);
                }

                if (registrationDetails.ContainsKey("Email") && !registrationDetails["Email"].Equals(string.Empty))
                {
                    var emailField = _driver.FindElement(By.Id("Email"));
                    emailField.Clear();
                    emailField.SendKeys(registrationDetails["Email"]);
                }

                if (registrationDetails.ContainsKey("SecurityQuestion") && !registrationDetails["SecurityQuestion"].Equals(string.Empty))
                {
                    var securityQuestionField = _driver.FindElement(By.Id("SecurityQuestion"));
                    securityQuestionField.Clear();
                    securityQuestionField.SendKeys(registrationDetails["SecurityQuestion"]);
                }

                if (registrationDetails.ContainsKey("SecurityAnswer") && !registrationDetails["SecurityAnswer"].Equals(string.Empty))
                {
                    var securityAnswerField = _driver.FindElement(By.Id("SecurityAnswer"));
                    securityAnswerField.Clear();
                    securityAnswerField.SendKeys(registrationDetails["SecurityAnswer"]);
                }

                if (registrationDetails.ContainsKey("MobileSubscriber"))
                {
                    var smsSubscriberField = _driver.FindElement(By.Id("MobileSubscriber"));
                    if ((!smsSubscriberField.Selected && registrationDetails["MobileSubscriber"].Equals("true"))
                        || (smsSubscriberField.Selected && registrationDetails["MobileSubscriber"].Equals("false")))
                    {
                        smsSubscriberField.Click();
                    }
                }

                if (registrationDetails.ContainsKey("MobileNotificationPeriod") && !registrationDetails["MobileNotificationPeriod"].Equals(string.Empty))
                {
                    var smsNotificationPeriodField = new SelectElement(_driver.FindElement(By.Id("MobileNotificationPeriod")));
                    smsNotificationPeriodField.SelectByText(registrationDetails["MobileNotificationPeriod"]);
                }

                if (registrationDetails.ContainsKey("InstantMessagingSubscriber"))
                {
                    var googleHangoutsSubscriberField = _driver.FindElement(By.Id("InstantMessagingSubscriber"));
                    if ((!googleHangoutsSubscriberField.Selected && registrationDetails["InstantMessagingSubscriber"].Equals("true"))
                        || (googleHangoutsSubscriberField.Selected && registrationDetails["InstantMessagingSubscriber"].Equals("false")))
                    {
                        googleHangoutsSubscriberField.Click();
                    }
                }

                if (registrationDetails.ContainsKey("InstantMessagingNotificationPeriod") && !registrationDetails["InstantMessagingNotificationPeriod"].Equals(string.Empty))
                {
                    var instantMessagingNotificationPeriodField = new SelectElement(_driver.FindElement(By.Id("InstantMessagingNotificationPeriod")));
                    instantMessagingNotificationPeriodField.SelectByText(registrationDetails["InstantMessagingNotificationPeriod"]);
                }

                if (registrationDetails.ContainsKey("CalendarSubscriber"))
                {
                    var googleCalendarSubsriberField = _driver.FindElement(By.Id("CalendarSubscriber"));
                    if ((!googleCalendarSubsriberField.Selected && registrationDetails["CalendarSubscriber"].Equals("true"))
                        || (googleCalendarSubsriberField.Selected && registrationDetails["CalendarSubscriber"].Equals("false")))
                    {
                        googleCalendarSubsriberField.Click();
                    }
                }

                _driver.FindElement(By.Id("CalendarSubscriber")).Submit();
            }
            else
            {
                Assert.Fail("No registration details were provided for the new user.");
            }
        }

        /// <summary>
        /// Logs in a user with the specified login details.
        /// </summary>
        /// <param name="loginDetails">The login details.</param>
        public void Login(Dictionary<string, string> loginDetails)
        {
            if (loginDetails != null)
            {
                _driver.Navigate().GoToUrl(_baseUrl);
                _driverWait.Until(ExpectedConditions.ElementIsVisible(By.Id("loginForm")));

                if (loginDetails.ContainsKey("Username") && !loginDetails["Username"].Equals(string.Empty))
                {
                    var usernameField = _driver.FindElement(By.Id("UserName"));
                    usernameField.Clear();
                    usernameField.SendKeys(loginDetails["Username"]);
                }

                if (loginDetails.ContainsKey("Password") && !loginDetails["Password"].Equals(string.Empty))
                {
                    var passwordField = _driver.FindElement(By.Id("Password"));
                    passwordField.Clear();
                    passwordField.SendKeys(loginDetails["Password"]);
                }

                if (loginDetails.ContainsKey("RememberMe"))
                {
                    var rememberMeField = _driver.FindElement(By.Id("RememberMe"));
                    if ((!rememberMeField.Selected && loginDetails["RememberMe"].Equals("true"))
                        || (rememberMeField.Selected && loginDetails["RememberMe"].Equals("false")))
                    {
                        rememberMeField.Click();
                    }
                }

                _driver.FindElement(By.Id("RememberMe")).Submit();
            }
            else
            {
                Assert.Fail("No login details were provided to log in user.");
            }
        }

        /// <summary>
        /// Setups this instance.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            _baseUrl = ConfigurationManager.AppSettings["baseUrl"];
            _driver = new ChromeDriver(@"..\..\Resources");
            _driverWait = new WebDriverWait(_driver, TimeSpan.FromMilliseconds(5000));
        }

        /// <summary>
        /// Determines whether a user can register with valid details.
        /// </summary>
        [TestCase]
        public void CanRegisterWithValidDetails()
        {
            var userDetails = new Dictionary<string, string>
                                  {
                                      { "Username", "Summy" },
                                      { "Password", "S987560" },
                                      { "ConfirmPassword", "S987560" },
                                      { "Mobile", "07733156534" },
                                      { "Email", "sumuditha.ranawaka@gmail.com" },
                                      { "SecurityQuestion", "What is your birthplace?" },
                                      { "SecurityAnswer", "Colombo" },
                                      { "MobileSubscriber", "true" },
                                      { "MobileNotificationPeriod", "5" },
                                      { "InstantMessagingSubscriber", "true" },
                                      { "InstantMessagingNotificationPeriod", "5" },
                                      { "CalendarSubscriber", "true" }
                                  };

            Register(userDetails);

            _driverWait.Until(ExpectedConditions.ElementExists(By.Id("loginForm")));

            Assert.AreEqual(1, _driver.FindElements(By.Id("loginForm")).Count, "The user was not successfully registered on the system.");
        }

        /// <summary>
        /// Determines whether a user can login with valid details.
        /// </summary>
        [TestCase]
        public void CanLoginWithValidDetails()
        {
            var userDetails = new Dictionary<string, string>
                                  {
                                      { "Username", "Pack" },
                                      { "Password", "Q763THY" },
                                      { "ConfirmPassword", "Q763THY" },
                                      { "Mobile", "07965941236" },
                                      { "Email", "pack.m@gmail.com" },
                                      { "SecurityQuestion", "What is the colour of your car?" },
                                      { "SecurityAnswer", "Red" },
                                      { "MobileSubscriber", "true" },
                                      { "MobileNotificationPeriod", "10" },
                                      { "InstantMessagingSubscriber", "true" },
                                      { "InstantMessagingNotificationPeriod", "10" },
                                      { "CalendarSubscriber", "true" }
                                  };

            Register(userDetails);

            _driverWait.Until(ExpectedConditions.ElementExists(By.Id("loginForm")));

            Login(userDetails);

            _driverWait.Until(ExpectedConditions.ElementIsVisible(By.Id("multiple-project-content-div")));

            Assert.AreEqual(1, _driver.FindElements(By.Id("multiple-project-content-div")).Count, "The user was not successfully logged in.");
        }

        /// <summary>
        /// Determines whether a user can not login without username.
        /// </summary>
        [TestCase]
        public void CanNotLoginWithoutUsername()
        {
            var userDetails = new Dictionary<string, string>
            {
                {"Username", "Sam"},
                {"Password", "YE736487GH"},
                {"ConfirmPassword", "YE736487GH"},
                {"Mobile", "07968345192"},
                {"Email", "sam.j@gmail.com"},
                {"SecurityQuestion", "What is the colour of your car?"},
                {"SecurityAnswer", "Blue"},
                {"MobileSubscriber", "true"},
                {"MobileNotificationPeriod", "10"},
                {"InstantMessagingSubscriber", "true"},
                {"InstantMessagingNotificationPeriod", "10"},
                {"CalendarSubscriber", "false"}
            };

            Register(userDetails);

            _driverWait.Until(ExpectedConditions.ElementExists(By.Id("loginForm")));

            userDetails.Remove("Username");

            Login(userDetails);

            Assert.AreEqual(1, _driver.FindElements(By.XPath("//*[@id='loginForm']/form/fieldset/ol/li[1]/span")).Count, "The system allowed a user to login without a username.");
        }

        /// <summary>
        /// Determines whether a user can not login without password.
        /// </summary>
        [TestCase]
        public void CanNotLoginWithoutPassword()
        {
        }

        /// <summary>
        /// Determines whether a user can not register without matching passwords.
        /// </summary>
        [TestCase]
        public void CanNotRegisterWithoutMatchingPasswords()
        {
            var userDetails = new Dictionary<string, string>
            {
                {"Username", "Doll"},
                {"Password", "D78346GHS"},
                {"ConfirmPassword", "NotMatching"},
                {"Mobile", "07541671092"},
                {"Email", "doll.e@gmail.com"},
                {"SecurityQuestion", "What is the colour of your house?"},
                {"SecurityAnswer", "White"},
                {"MobileSubscriber", "true"},
                {"MobileNotificationPeriod", "15"},
                {"InstantMessagingSubscriber", "true"},
                {"InstantMessagingNotificationPeriod", "15"},
                {"CalendarSubscriber", "false"}
            };

            Register(userDetails);

            Assert.AreEqual(1, _driver.FindElements(By.XPath("//*[@id='main']/form/div/ul/li[1]")).Count, "The system allowed a user to register without matching passwords.");
        }

        /// <summary>
        /// Determines whether a user can not register without a security question.
        /// </summary>
        [TestCase]
        public void CanNotRegisterWithoutSecurityQuestion()
        {
        }

        /// <summary>
        /// Determines whether a user can not register without a security answer.
        /// </summary>
        [TestCase]
        public void CanNotRegisterWithoutSecurityAnswer()
        {

        }

        /// <summary>
        /// Determines whether a user can not subscribe to calendar updates without an email address.
        /// </summary>
        [TestCase]
        public void CanNotSubscribeToCalendarUpdatesWithoutEmail()
        {

        }

        /// <summary>
        /// Determines whether a user can not subscribe to SMS updates without a mobile number.
        /// </summary>
        [TestCase]
        public void CanNotSubscribeToSmsUpdatesWithoutMobile()
        {

        }

        /// <summary>
        /// Determines whether a user can not subscribe to SMS updates without a notification period.
        /// </summary>
        [TestCase]
        public void CanNotSubscribeToSmsUpdatesWithoutNotificationPeriod()
        {

        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            _driver.Quit();
        }
    }
}