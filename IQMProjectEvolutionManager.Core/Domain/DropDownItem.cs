using ProjectName.Core.Enums;

namespace ProjectName.Core.Domain
{
    /// <summary>
    /// Drop Down Item
    /// </summary>
    public class DropDownItem
    {
        /// <summary>
        /// Gets or sets the drop down item id.
        /// </summary>
        /// <value>-
        /// The drop down item id.
        /// </value>
        public virtual long DropDownItemId { get; set; }

        /// <summary>
        /// Gets or sets the type of the drop down.
        /// </summary>
        /// <value>
        /// The type of the drop down.
        /// </value>
        public virtual DropDownItemType DropDownItemType { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets or sets the short name.
        /// </summary>
        /// <value>The short name.</value>
        public virtual string ShortName { get; set; }

        /// <summary>
        /// Gets or sets the order.
        /// </summary>
        /// <value>The order.</value>
        public virtual int Order { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="DropDownItem"/> is visible.
        /// </summary>
        /// <value><c>true</c> if visible; otherwise, <c>false</c>.</value>
        public virtual bool Visible { get; set; }

        /// <summary>
        /// Gets or sets the meta data.
        /// </summary>
        /// <value>The meta data.</value>
        /// <remarks>
        ///     Payment - CASH CARD
        /// </remarks>
        public virtual string MetaData { get; set; }

        /// <summary>
        /// Gets the display name.
        /// </summary>
        /// <returns>Display Name</returns>
        public virtual string GetDisplayName()
        {
            if (!string.IsNullOrWhiteSpace(ShortName))
            {
                return string.Format("{0} - {1}", ShortName, Name);
            }

            return Name;
        }
    }
}