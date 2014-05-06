// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WebSecurityInitializer.cs" company="IQM Software">
//   Sumuditha Ranawaka 2014.
// </copyright>
// <summary>
//   The web security initializer.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace IQMProjectEvolutionManager.Utility
{
    using System.Diagnostics.CodeAnalysis;

    using WebMatrix.WebData;

    /// <summary>
    /// The web security initializer.
    /// </summary>
    public class WebSecurityInitializer
    {
        /// <summary>
        /// The sync root.
        /// </summary>
        private readonly object syncRoot = new object();

        /// <summary>
        /// The is not init.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        private bool isNotInit = true;

        /// <summary>
        /// The instance.
        /// </summary>
        public static readonly WebSecurityInitializer Instance = new WebSecurityInitializer();

        /// <summary>
        /// Initializes a new instance of the <see cref="WebSecurityInitializer"/> class.
        /// </summary>
        private WebSecurityInitializer()
        {

        }

        /// <summary>
        /// The ensure initialize.
        /// </summary>
        public void EnsureInitialize()
        {
            if (!this.isNotInit)
            {
                return;
            }

            lock (this.syncRoot)
            {
                if (!this.isNotInit)
                {
                    return;
                }

                this.isNotInit = false;

                WebSecurity.InitializeDatabaseConnection(
                    "ApplicationServices",
                    userTableName: "Subscriber",
                    userIdColumn: "SubscriberId",
                    userNameColumn: "UserName",
                    autoCreateTables: true);
            }
        }
    }
}