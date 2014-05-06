using IQM.Common.Interfaces;
using IQM.Common.Web.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IQMProjectEvolutionManager.Core.Interfaces.Services.BaseServices
{
    public interface IOnTimeDomainBaseService<TDomain, TDomainWrapper, TPagedSearchDomainWrapper> : IGenericService<TPagedSearchDomainWrapper, TDomainWrapper>
    {
        bool InDatabase(TDomain domainObject);
        TDomain GetByOnTimeId(int Id);
        //TPagedSearchDomainWrapper Search(TPagedSearchDomainWrapper pagedSearchViewModel, int pageNumber, int pageSize);
    }
}