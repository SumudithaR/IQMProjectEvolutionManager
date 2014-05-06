using IQM.Common.Interfaces;
using IQM.Common.Services;
using IQMProjectEvolutionManager.Core.Domain;
using IQMProjectEvolutionManager.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IQMProjectEvolutionManager.Core.Interfaces.Services
{
    public interface IMyManagedListItemService
    {
        /// <summary>
        /// Gets the visible item.
        /// </summary>
        /// <param name="managedListItemType">Type of the drop down.</param>
        /// <returns>
        /// List of visible items ordered by their order.
        /// </returns>
        IList<MyManagedListItem> GetVisible(MyManagedListItemType managedListItemType);

        /// <summary>
        /// Gets the first by meta data.
        /// </summary>
        /// <param name="managedListItemType">Type of the drop down.</param>
        /// <param name="metaData">The meta data.</param>
        /// <returns>ManagedListItem</returns>
        IList<MyManagedListItem> GetByMetaData(string metaData, MyManagedListItemType? managedListItemType = null);
    }
}