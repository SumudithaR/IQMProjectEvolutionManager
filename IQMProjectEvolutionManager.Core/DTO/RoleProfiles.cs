using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace IQMProjectEvolutionManager.Core.DTO
{
    /// <summary>
    /// Role Profiles
    /// </summary>
    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Roles are self explainitory")]
    public class RoleProfiles
    {
        /// <summary>
        /// Profile Class
        /// </summary>
        public class Profile
        {
            /// <summary>
            /// Gets or sets the name.
            /// </summary>
            /// <value>The name.</value>
            public string Name { get; set; }

            /// <summary>
            /// Gets or sets the roles.
            /// </summary>
            /// <value>The roles.</value>
            public List<string> Roles { get; set; }
        }

        // Disable the warning tell us that we need Xml comments.
#pragma warning disable 1591
        public const string SystemAdmin = "IQMProjectEvolutionManager.Admin.SystemAdmin";
        public const string ContractEdit = "IQMProjectEvolutionManager.Contract.Edit";
        public const string AdminEdit = "IQMProjectEvolutionManager.Admin.Edit";
        public const string ReportingView = "IQMProjectEvolutionManager.Reporting.View";
        public const string AccountingEdit = "IQMProjectEvolutionManager.Accounting.Edit";
#pragma warning restore 1591

        /// <summary>
        /// Initializes a new instance of the <see cref="RoleProfiles"/> class.
        /// </summary>
        public RoleProfiles()
        {
            this.Profiles = new List<Profile>();
            this.Roles = new List<string>();

            this.Roles.Add(ContractEdit);
            this.Roles.Add(AdminEdit);
            this.Roles.Add(ReportingView);
            this.Roles.Add(AccountingEdit);

            var admin = new Profile { Name = "Admin", Roles = this.Roles };

            this.Profiles.Add(admin);
        }

        /// <summary>
        /// Gets or sets the profiles.
        /// </summary>
        /// <value>The profiles.</value>
        public List<Profile> Profiles { get; set; }

        /// <summary>
        /// Gets or sets the roles.
        /// </summary>
        /// <value>The roles.</value>
        public List<string> Roles { get; set; }
    }
}