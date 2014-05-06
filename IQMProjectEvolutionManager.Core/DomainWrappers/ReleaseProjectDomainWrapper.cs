// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReleaseProjectDomainWrapper.cs" company="IQM Software">
//   Sumuditha Ranawaka 2014
// </copyright>
// <summary>
//   The release project domain wrapper.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace IQMProjectEvolutionManager.Core.DomainWrappers
{
    using IQM.Common.Web.ViewModels;

    using IQMProjectEvolutionManager.Core.Domain;
    using IQMProjectEvolutionManager.Core.ViewModels.SearchViewModels;

    /// <summary>
    /// The release project domain wrapper.
    /// </summary>
    public class ReleaseProjectDomainWrapper : BaseViewModel<ReleaseProject>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReleaseProjectDomainWrapper"/> class. 
        /// Initializes a new instance of the <see cref="ReleaseDomainWrapper"/> class.
        /// </summary>
        public ReleaseProjectDomainWrapper() : base(new ApplicationSettings())
        {
        }

        /// <summary>
        /// Gets the name of the domain object.
        /// </summary>
        /// <returns>The friendly name of the data object.</returns>
        public override string GetDomainObjectName()
        {
            return "Release Project";
        }

        /// <summary>
        /// Gets the data id.
        /// </summary>
        /// <returns>the data objects id.</returns>
        public override long? GetDataId()
        {
            return Data.ReleaseProjectId;
        }
    }
}
