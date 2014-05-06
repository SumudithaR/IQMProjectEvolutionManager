using IQM.Common.Interfaces;
using IQM.Common.Services;
using IQM.Common.Web.Interfaces;
using IQMProjectEvolutionManager.Core.Interfaces.Domain;
using IQMProjectEvolutionManager.Core.Interfaces.Services.BaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IQMProjectEvolutionManager.Core.Services.BaseServices
{
    public abstract class DomainBaseService<TDomain, TDomainWrapper, TPagedSearchDomainWrapper> : GenericService<TDomain, TDomainWrapper, IGenericRepository<TDomain>>,
        IGenericService<TPagedSearchDomainWrapper, TDomainWrapper>, IDomainBaseService<TDomain, TDomainWrapper, TPagedSearchDomainWrapper>
        where TDomain : IDomain
        where TDomainWrapper : IBaseViewModel<TDomain>
    {
        public virtual TPagedSearchDomainWrapper Search(TPagedSearchDomainWrapper pagedSearchViewModel, int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }
    }
}