using IQM.Common.Interfaces;
using IQM.Common.Services;
using IQMProjectEvolutionManager.Core.Domain;
using IQMProjectEvolutionManager.Core.Enums;
using IQMProjectEvolutionManager.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IQMProjectEvolutionManager.Core.Services
{
    public class MyManagedListItemService : BasicGenericService<MyManagedListItem, IGenericRepository<MyManagedListItem>>, IMyManagedListItemService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MyManagedListItemService"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public MyManagedListItemService(IGenericRepository<MyManagedListItem> repository)
        {
            Repository = repository;
        }

        /// <summary>
        /// Gets the visible item.
        /// </summary>
        /// <param name="managedListItemType">Type of the drop down.</param>
        /// <returns>
        /// List of visible items ordered by their order.
        /// </returns>
        public IList<MyManagedListItem> GetVisible(MyManagedListItemType managedListItemType)
        {
            return GetAll().Where(x => x.Visible && x.ListItemType == managedListItemType).OrderBy(x => x.Order).ToList();
        }

        /// <summary>
        /// Gets the first by meta data.
        /// </summary>
        /// <param name="managedListItemType">Type of the drop down.</param>
        /// <param name="metaData">The meta data.</param>
        /// <returns>ManagedListItem</returns>
        public IList<MyManagedListItem> GetByMetaData(string metaData, MyManagedListItemType? managedListItemType = null)
        {
            var query = (IEnumerable<MyManagedListItem>)GetAll();
            if (managedListItemType != null)
            {
                query = query.Where(x => x.ListItemType == managedListItemType);
            }

            // Get all be meta data.
            query = query.Where(x => x.Visible && x.MetaData == metaData);

            return query.ToList();
        }
    }
}