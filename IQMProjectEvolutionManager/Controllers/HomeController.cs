using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IQM.Common;
using IQM.Common.Web.Utility;
using IQM.Common.Web.ViewModels;
using log4net;

namespace IQMProjectEvolutionManager.Controllers
{
    using System.Diagnostics.CodeAnalysis;

    [TocAuthorize]
    public class HomeController : Controller
    {
        /// <summary>
        /// The index.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Index()
        {
            return View();
        }

        #region Update Schema
        /// <summary>
        /// Updates the data base schema.
        /// </summary>
        /// <returns>Redirects to Index View</returns>
        public ActionResult Update()
        {
            NHibernateHelper.UpdateSchema();
            this.TempData["Status"] = StatusMessage.Success("Successfully updated database schema");
            return this.RedirectToAction("Index");
        }
        #endregion

        /// <summary>
        /// Changes the log.
        /// </summary>
        /// <returns>Change Log</returns>
        public ActionResult ChangeLog()
        {
            return View();
        }

        /// <summary>
        /// Log javascript Errors.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="url">The URL.</param>
        /// <param name="line">The line.</param>
        /// <param name="referer">The referer.</param>
        /// <returns>throws an exception</returns>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly",
            Justification = "Reviewed. Suppression is OK here.")][HttpPost]
        public JsonResult JavascriptError(string message, string url, string line, string referer)
        {
            var errorMessage = string.Format("Javascript Error: Url: {0}, Line: {1}, Message: {2}, Referer: {3}", url, line, message, referer);
            LogManager.GetLogger(GetType()).Error(errorMessage);
            return this.Json(errorMessage);
        }
    }
}
