// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReleaseDomainWrapper.cs" company="IQM Software">
//   Sumuditha Ranawaka 2014
// </copyright>
// <summary>
//   Active Release View Model
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace IQMProjectEvolutionManager.Core.DomainWrappers
{
    using System;

    using IQM.Common.Web.ViewModels;

    using IQMProjectEvolutionManager.Core.Domain;
    using IQMProjectEvolutionManager.Core.Enums;
    using IQMProjectEvolutionManager.Core.ViewModels.SearchViewModels;

    /// <summary>
    /// Active Release View Model
    /// </summary>
    public class ReleaseDomainWrapper : BaseViewModel<Release>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReleaseDomainWrapper"/> class.
        /// </summary>
        public ReleaseDomainWrapper() : base(new ApplicationSettings())
        {
        }

        /// <summary>
        /// Gets or sets the associated project id.
        /// </summary>
        public long AssociatedProjectId { get; set; }

        /// <summary>
        /// Gets the name of the domain object.
        /// </summary>
        /// <returns>The friendly name of the data object.</returns>
        public override string GetDomainObjectName()
        {
            return "Release";
        }

        /// <summary>
        /// Gets the data id.
        /// </summary>
        /// <returns>the data objects id.</returns>
        public override long? GetDataId()
        {
            return Data.ReleaseId;
        }

        /// <summary>
        /// Gets the status of the release based on the due date and the remaining hours
        /// </summary>
        /// <value>
        /// The release status.
        /// </value>
        /// <returns>
        /// The <see cref="ReleaseStatus"/>.
        /// </returns>
        public ReleaseStatus GetReleaseStatus()
        {
            var ts = Data.DueDate - DateTime.Today;

            if (ts.TotalHours < 0 && Data.HoursRemaining > 0)
            {
                return ReleaseStatus.Ugly;
            }

            if (ts.TotalHours > 0 && (ts.TotalDays * 8) < Data.HoursRemaining)
            {
                return ReleaseStatus.Bad;
            }

            if (ts.TotalHours > 0 && (ts.TotalDays * 8) > Data.HoursRemaining)
            {
                return ReleaseStatus.Good;
            }

            return ReleaseStatus.Good;
        }
    }
}