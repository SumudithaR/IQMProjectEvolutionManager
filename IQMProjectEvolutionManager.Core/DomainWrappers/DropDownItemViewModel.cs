using System.Collections.Generic;
using IQM.Common.Interfaces;
using ProjectName.Core.Domain;
using ProjectName.Core.Enums;

namespace ProjectName.Core.ViewModels
{
    /// <summary>
    /// The view model for the drop down items
    /// </summary>
    public class DropDownItemViewModel : IViewModel<IList<DropDownItem>>
    {
        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        public IList<DropDownItem> Data { get; set; }

        /// <summary>
        /// Gets or sets the type of the drop down.
        /// </summary>
        /// <value>
        /// The type of the drop down.
        /// </value>
        public DropDownItemType DropDownItemType { get; set; }
    }
}
