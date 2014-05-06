using System.Linq;
using System.Collections.Generic;
using IQM.Common.Services;
using IQM.Common.Interfaces;
using ProjectName.Core.Domain;
using ProjectName.Core.Enums;

namespace ProjectName.Core.Services
{
    /// <summary>
    /// Service for DropDownItems
    /// </summary>
    public class DropDownItemService : BasicGenericService<DropDownItem, IGenericRepository<DropDownItem>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DropDownItemService"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public DropDownItemService(IGenericRepository<DropDownItem> repository)
        {
            Repository = repository;
        }

        /// <summary>
        /// Gets the visible item.
        /// </summary>
        /// <param name="dropDownItemType">Type of the drop down.</param>
        /// <returns>
        /// List of visible items ordered by their order.
        /// </returns>
        public IList<DropDownItem> GetVisible(DropDownItemType dropDownItemType)
        {
            return Repository.Linq.Where(x => x.Visible && x.DropDownItemType == dropDownItemType).OrderBy(x => x.Order).ToList();
        }
    }
}
