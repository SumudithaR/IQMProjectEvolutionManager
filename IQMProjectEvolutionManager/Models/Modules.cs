// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Modules.cs" company="IQM Software">
//   Sumuditha Ranawaka 2014.
// </copyright>
// <summary>
//   Modules
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace IQMProjectEvolutionManager.Models
{
    using System.ComponentModel;
    using System.Configuration;
    using System.Diagnostics.CodeAnalysis;
    using System.Reflection;

    /// <summary>
    /// Modules
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed. Suppression is OK here.")]
    public static class Modules
    {
        /// <summary>
        /// Gets the IQM product dashboard.
        /// </summary>
        public static string IQMProductDashboard
        {
            get { return GetSetting<string>("IQMProductDashboard"); }
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="Modules"/> is tracking.
        /// </summary>
        /// <value><c>true</c> if tracking; otherwise, <c>false</c>.</value>
        public static bool Tracking
        {
            get
            {
                return GetSetting<bool>("Tracking");
            }
        }

        /// <summary>
        /// Gets the version.
        /// </summary>
        public static string Version
        {
            get
            {
                var appName = Assembly.GetAssembly(typeof(MvcApplication)).Location;
                var assemblyName = AssemblyName.GetAssemblyName(appName);
                return assemblyName.Version.ToString();
            }
        }

        /// <summary>
        /// Gets the setting.
        /// </summary>
        /// <typeparam name="T">Type of Setting</typeparam>
        /// <param name="settingName">Name of the setting.</param>
        /// <returns>
        ///   <c>true</c> if enabled; otherwise, <c>false</c>.
        /// </returns>
        private static T GetSetting<T>(string settingName)
        {
            var converter = TypeDescriptor.GetConverter(typeof(T));
            return (T)converter.ConvertFromString(ConfigurationManager.AppSettings[settingName]);
        }
    }
}