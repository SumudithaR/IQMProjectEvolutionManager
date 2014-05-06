using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Http;
using System.Web.Optimization;
using System.Web.Routing;
//using AssetManagement.App_Start;
//using AssetManagement.Core;
//using AssetManagement.Core.Domain;
//using AssetManagement.Core.ViewModels;
//using AssetManagement.DataBinders;
using IQM.Common.Web.Utility;
using Ninject;
using Ninject.Modules;
using IQMProjectEvolutionManager.Core;

namespace IQMProjectEvolutionManager
{
    using System.Web.WebPages;

    using IQMProjectEvolutionManager.Utility;

    using WebMatrix.WebData;

    public class MvcApplication : HttpApplication
    {
        /// <summary>
        /// Setups the dependency injection.
        /// </summary>
        /// <returns>The currently configured kernel</returns>
        public IKernel SetupDependencyInjection()
        {
            var modules = new List<INinjectModule>
                              {
                                  new CoreServiceModule()
                              };

            // Create the Ninject
            IKernel kernel = new StandardKernel(new NinjectSettings
            {
                UseReflectionBasedInjection = true
            }, modules.ToArray());
            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
            return kernel;
        }

        /// <summary>
        /// Registers the binders.
        /// </summary>
        public static void RegisterBinders()
        {
            //// TODO: REGISTER YOUR BINDERS HERE
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();

            var modules = new List<INinjectModule>
                              {
                                  new CoreServiceModule()
                              };

            // Register the view model data binders
            RegisterBinders();

            // Create the Ninject
            IKernel kernel = new StandardKernel(new NinjectSettings { UseReflectionBasedInjection = true }, modules.ToArray());
            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));

            DisplayModeProvider.Instance.Modes.Insert(0, new DefaultDisplayMode("iphone")
            {
                ContextCondition = Context =>
                                Context.Request.Browser["HardwareModel"] == "iPhone"
            });

            DisplayModeProvider.Instance.Modes.Insert(1, new DefaultDisplayMode("android")
            {
                ContextCondition = Context =>
                                Context.Request.Browser["PlatformName"] == "Android"
            });

            DisplayModeProvider.Instance.Modes.Insert(2, new DefaultDisplayMode("mobile")
            {
                ContextCondition = Context =>
                                Context.Request.Browser["IsMobile"] == "True"
            });

            //this.InitiliseDisplayModeProvider();
        }

        //protected void InitiliseDisplayModeProvider()
        //{
        //    var phone = new DefaultDisplayMode("Phone")
        //                    {
        //                        ContextCondition =
        //                            ctx =>
        //                            ctx.GetOverriddenUserAgent() != null
        //                            && ctx.GetOverriddenUserAgent().Contains("iPhone")
        //                    };

        //    var tablet = new DefaultDisplayMode("Tablet")
        //    {
        //        ContextCondition =
        //            ctx =>
        //            ctx.GetOverriddenUserAgent() != null
        //            && ctx.GetOverriddenUserAgent().Contains("iPad")
        //    };

        //    DisplayModeProvider.Instance.Modes.Insert(0, phone);
        //    DisplayModeProvider.Instance.Modes.Insert(1, tablet);
        //}

        /// <summary>
        /// Handles the EndRequest event of the Application control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void Application_EndRequest(object sender, EventArgs e)
        {
            // if Forms Authentication redirect happend
            if (Response.StatusCode != 302) return;
            const string pattern = @"(?<=ReturnUrl\=).+$";
            var redirectUrl = Server.UrlDecode(Response.RedirectLocation);
            var returnUrlQuery = Regex.Match(redirectUrl, pattern, RegexOptions.IgnoreCase).Value;

            if (returnUrlQuery == string.Empty) return;
            var hostUrl = Regex.Replace(redirectUrl, pattern, string.Empty);
            var returnUrl = HttpContext.Current.Request.Url.AbsoluteUri;

            // combine the hostname and ReturnUrl Query
            Response.RedirectLocation = hostUrl + Server.UrlEncode(returnUrl);
        }
    }
}