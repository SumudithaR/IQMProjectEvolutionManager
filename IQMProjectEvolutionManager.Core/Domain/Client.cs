using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectName.Core.Domain
{
    /// <summary>
    /// Client class
    /// </summary>
    public class Client
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Client"/> class.
        /// </summary>
        public Client()
        {
            Addresses = new List<Address>();
        }

        /// <summary>
        /// Gets or sets the client id.
        /// </summary>
        /// <value>
        /// The client id.
        /// </value>
        public virtual long ClientId { get; set; }

        /// <summary>
        /// Gets or sets the name of the client.
        /// </summary>
        /// <value>
        /// The name of the client.
        /// </value>
        public virtual string CompanyName { get; set; }

        /// <summary>
        /// Gets or sets the SAP number.
        /// </summary>
        /// <value>
        /// The SAP number.
        /// </value>
        public virtual string SAPNumber { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public virtual string Email { get; set; }

        /// <summary>
        /// Gets or sets the telephone.
        /// </summary>
        /// <value>
        /// The telephone.
        /// </value>
        public virtual string Telephone { get; set; }

        /// <summary>
        /// Gets or sets the addresses.
        /// </summary>
        /// <value>
        /// The addresses.
        /// </value>
        public virtual IList<Address> Addresses { get; set; }

        /// <summary>
        /// Gets or sets the liability insurance expiry.
        /// </summary>
        /// <value>
        /// The liability insurance expiry.
        /// </value>
        [Display(Name = "Liabilty Insuarance Expiry")]
        [DataType(DataType.Date)]
        public virtual DateTime? LiabilityInsuranceExpiry { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [ins copy in folder].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [ins copy in folder]; otherwise, <c>false</c>.
        /// </value>
        public virtual bool InsCopyInFolder { get; set; }
    }
}
