using System.ComponentModel.DataAnnotations;
using IQM.Common.Web.ViewModels.SearchViewModels;
using ProjectName.Core.Domain;

namespace ProjectName.Core.ViewModels.SearchViewModels
{
    /// <summary>
    /// The client search view model
    /// </summary>
    public class ClientPagedSearchViewModel : PagedSearchViewModel<Client>
    {
        public ClientPagedSearchViewModel() : base(new ApplicationSettings())
        {
        }

        /// <summary>
        /// Gets or sets the name of the company.
        /// </summary>
        /// <value>
        /// The name of the company.
        /// </value>
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }

        /// <summary>
        /// Gets or sets the sap number.
        /// </summary>
        /// <value>
        /// The sap number.
        /// </value>
        [Display(Name = "SAP Number")]
        public string SapNumber { get; set; }

        /// <summary>
        /// Copies the model.
        /// </summary>
        /// <returns>A copy of the view model</returns>
        public override object CopyModel()
        {
            return new ClientPagedSearchViewModel
                       {
                           CompanyName = this.CompanyName,
                           SapNumber = this.SapNumber
                       };
        }
    }
}