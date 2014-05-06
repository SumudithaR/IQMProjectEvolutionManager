using IQM.Common.Web.ViewModels.SearchViewModels;
using IQMProjectEvolutionManager.Core.Domain;
using IQMProjectEvolutionManager.Core.ViewModels.SearchViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace IQMProjectEvolutionManager.Core.DomainWrappers.SearchDomainWrappers
{
    public class ReleasePagedSearchDomainWrapper : PagedSearchViewModel<Release>
    {
        public ReleasePagedSearchDomainWrapper() : base(new ApplicationSettings())
        {
        }

        public override object CopyModel()
        {
            return new ReleasePagedSearchDomainWrapper();
        }
    }
}
