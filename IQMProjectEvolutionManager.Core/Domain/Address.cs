using System.ComponentModel.DataAnnotations;

namespace ProjectName.Core.Domain
{
    /// <summary>
    /// Address Class
    /// </summary>
    public class Address
    {
        public Address()
        {
            AddressName = string.Empty;
            Line1 = string.Empty;
            Line2 = string.Empty;
            Line3 = string.Empty;
            Town = string.Empty;
            County = string.Empty;
            Postcode = string.Empty;
        }

        /// <summary>
        /// Gets or sets the address id.
        /// </summary>
        /// <value>
        /// The address id.
        /// </value>
        public virtual long AddressId { get; set; }

        /// <summary>
        /// Gets or sets the name of the address.
        /// </summary>
        /// <value>
        /// The name of the address.
        /// </value>
        [Display(Name = "Addres Name")]
        public virtual string AddressName { get; set; }
        
        /// <summary>
        /// Gets or sets the line1.
        /// </summary>
        /// <value>
        /// The line1.
        /// </value>
        [Display(Name = "Address")]
        public virtual string Line1 { get; set; }

        /// <summary>
        /// Gets or sets the line2.
        /// </summary>
        /// <value>
        /// The line2.
        /// </value>
        public virtual string Line2 { get; set; }

        /// <summary>
        /// Gets or sets the line3.
        /// </summary>
        /// <value>
        /// The line3.
        /// </value>
        public virtual string Line3 { get; set; }

        /// <summary>
        /// Gets or sets the town.
        /// </summary>
        /// <value>
        /// The town.
        /// </value>
        [Display(Name = "Town")]
        public virtual string Town { get; set; }

        /// <summary>
        /// Gets or sets the county.
        /// </summary>
        /// <value>
        /// The county.
        /// </value>
        [Display(Name = "County")]
        public virtual string County { get; set; }

        /// <summary>
        /// Gets or sets the postcode.
        /// </summary>
        /// <value>
        /// The postcode.
        /// </value>
        [Display(Name = "Postcode")]
        [RegularExpression("[ ]*[a-zA-Z]{1,2}[0-9]{1,2}[a-zA-Z]{0,1}[ ]*[0-9][a-zA-Z]{2}[ ]*")]
        public virtual string Postcode { get; set; }
    }
}
