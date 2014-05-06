using System.ComponentModel.DataAnnotations;
using IQM.Common.Web.ViewModels.SearchViewModels;
using ProjectName.Core.Domain;

namespace ProjectName.Core.ViewModels.SearchViewModels
{
    /// <summary>
    /// The search view model for the addresses
    /// </summary>
    public class AddressPagedSearchViewModel : PagedSearchViewModel<Address>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddressPagedSearchViewModel"/> class.
        /// </summary>
        public AddressPagedSearchViewModel() : base(new ApplicationSettings())
        {
        }

        /// <summary>
        /// Gets or sets the post code.
        /// </summary>
        /// <value>
        /// The post code.
        /// </value>
        [Display(Name = "Post code")]
        public string PostCode { get; set; }

        /// <summary>
        /// Copies the model.
        /// </summary>
        /// <returns>A copy of the search view model</returns>
        public override object CopyModel()
        {
            return new AddressPagedSearchViewModel
                       {
                           PostCode = this.PostCode
                       };
        }
    }
}