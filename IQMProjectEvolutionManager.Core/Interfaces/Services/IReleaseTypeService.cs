// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IReleaseTypeService.cs" company="IQm Software">
//   Sumuditha Ranawaka 2014
// </copyright>
// <summary>
//   The ReleaseTypeService interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace IQMProjectEvolutionManager.Core.Interfaces.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using IQMProjectEvolutionManager.Core.Domain;
    using IQMProjectEvolutionManager.Core.DomainWrappers;
    using IQMProjectEvolutionManager.Core.DomainWrappers.SearchDomainWrappers;
    using IQMProjectEvolutionManager.Core.Interfaces.Services.BaseServices;
    using IQMProjectEvolutionManager.Core.Services.BaseServices;

    /// <summary>
    /// The ReleaseTypeService interface.
    /// </summary>
    public interface IReleaseTypeService : IOnTimeDomainBaseService<ReleaseType, ReleaseTypeDomainWrapper, ReleaseTypePagedSearchDomainWrapper>
    {
        /// <summary>
        /// The is updated.
        /// </summary>
        /// <param name="releaseType">
        /// The release type.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        bool IsUpdated(ReleaseType releaseType);

        /// <summary>
        /// The insert or update.
        /// </summary>
        /// <param name="releaseType">
        /// The release type.
        /// </param>
        void InsertOrUpdate(ReleaseType releaseType);

        /// <summary>
        /// The get by name.
        /// </summary>
        /// <param name="releaseTypeName">
        /// The release type name.
        /// </param>
        /// <returns>
        /// The <see cref="ReleaseType"/>.
        /// </returns>
        ReleaseType GetByName(string releaseTypeName);
    }
}