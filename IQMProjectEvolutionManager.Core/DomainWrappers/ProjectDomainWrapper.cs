// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProjectDomainWrapper.cs" company="IQM Software">
//   Sumuditha Ranawaka 2014
// </copyright>
// <summary>
//   Project View Model
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace IQMProjectEvolutionManager.Core.DomainWrappers
{
    using IQM.Common.Web.ViewModels;

    using IQMProjectEvolutionManager.Core.Domain;
    using IQMProjectEvolutionManager.Core.ViewModels.SearchViewModels;

    /// <summary>
    /// Project View Model
    /// </summary>
    public class ProjectDomainWrapper : BaseViewModel<Project>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectDomainWrapper"/> class.
        /// </summary>
        public ProjectDomainWrapper()
            : base(new ApplicationSettings())
        {
        }

        /// <summary>
        /// Gets or sets the next active release.
        /// </summary>
        public Release NextActiveRelease { get; set; }

        /// <summary>
        /// Gets or sets the next in active release.
        /// </summary>
        public Release NextInActiveRelease { get; set; }

        /// <summary>
        /// Gets the name of the domain object.
        /// </summary>
        /// <returns>The friendly name of the data object.</returns>
        public override string GetDomainObjectName()
        {
            return "Project";
        }

        /// <summary>
        /// Gets the data id.
        /// </summary>
        /// <returns>the data objects id.</returns>
        public override long? GetDataId()
        {
            return Data.ProjectId;
        }
    }
}