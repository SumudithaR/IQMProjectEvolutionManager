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
    public abstract class OnTimeDomainBaseService<TDomain, TDomainWrapper, TPagedSearchDomainWrapper> : GenericService<TDomain, TDomainWrapper, IGenericRepository<TDomain>>,
        IOnTimeDomainBaseService<TDomain, TDomainWrapper, TPagedSearchDomainWrapper>
        where TDomain : IOnTimeDomain
        where TDomainWrapper : IBaseViewModel<TDomain>
    {
        protected OnTimeDomainBaseService()
        {

        }

        public bool InDatabase(TDomain domainObject)
        {
            if (domainObject != null)
            {
                return GetAll().Any(domObject => domObject.OnTimeId == domainObject.OnTimeId);
            }
            return false;
        }

        public TDomain GetByOnTimeId(int Id)
        {
            return GetAll().SingleOrDefault(domObject => domObject.OnTimeId == Id);
        }

        public virtual TPagedSearchDomainWrapper Search(TPagedSearchDomainWrapper pagedSearchViewModel, int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }
    }
}