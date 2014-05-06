using IQMProjectEvolutionManager.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IQMProjectEvolutionManager.Core.Interfaces.Domain
{
    public interface IStaff : IDomain, IOnTimeDomain
    {
        long StaffId { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        bool IsActive { get; set; }
        
        ICollection<ReleaseWorkLog> ReleaseWorkLogs { get; set; }
    }
}
