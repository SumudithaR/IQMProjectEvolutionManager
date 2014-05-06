using IQM.Common.Web.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IQMProjectEvolutionManager.Core.Interfaces.Services.BaseServices
{
    public interface IDomainBaseService<TDomain, TDomainWrapper, TPagedSearchDomainWrapper> : IGenericService<TPagedSearchDomainWrapper, TDomainWrapper>
    {
        //TPagedSearchDomainWrapper Search(TPagedSearchDomainWrapper pagedSearchViewModel, int pageNumber, int pageSize);
    }
}