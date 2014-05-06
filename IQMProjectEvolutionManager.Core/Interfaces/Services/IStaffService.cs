using IQMProjectEvolutionManager.Core.Domain;
using IQMProjectEvolutionManager.Core.DomainWrappers;
using IQMProjectEvolutionManager.Core.DomainWrappers.SearchDomainWrappers;
using IQMProjectEvolutionManager.Core.Interfaces.Services.BaseServices;
using IQMProjectEvolutionManager.Core.Services.BaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IQMProjectEvolutionManager.Core.Interfaces.Services
{
    public interface IStaffService : IOnTimeDomainBaseService<Staff, StaffDomainWrapper, StaffPagedSearchDomainWrapper>
    {
        bool IsUpdated(Staff staff);
        void InsertOrUpdate(Staff staff);
    }
}