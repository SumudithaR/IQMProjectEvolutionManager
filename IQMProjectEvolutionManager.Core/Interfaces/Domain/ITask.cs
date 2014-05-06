using IQMProjectEvolutionManager.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IQMProjectEvolutionManager.Core.Interfaces.Domain
{
    public interface ITask : IDomain, IDataDomain
    {
        long TaskId { get; set; }
        int OnTimeId { get; set; }
        string Name { get; set; }
        Staff AssignedStaffMember { get; set; }
        Release Release { get; set; }
    }
}