// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MyManagedListItem.cs" company="IQM Software">
//   Sumuditha Ranawaka 2014
// </copyright>
// <summary>
//   Defines the MyManagedListItem type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace IQMProjectEvolutionManager.Core.Domain
{
    using IQMProjectEvolutionManager.Core.Enums;
    using IQMProjectEvolutionManager.Core.Interfaces.Domain;

    /// <summary>
    /// The my managed list item.
    /// </summary>
    public class MyManagedListItem : IQM.Common.Domain.TypedListItem<MyManagedListItemType>, IMyManagedListItem
    {
        /// <summary>
        /// Gets or sets the my managed list item id.
        /// </summary>
        public virtual long MyManagedListItemId { get; set; }

        /// <summary>
        /// The get id.
        /// </summary>
        /// <returns>
        /// The <see cref="long"/>.
        /// </returns>
        public override long GetId()
        {
            return this.MyManagedListItemId;
        }
    }
}