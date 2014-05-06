using IQMProjectEvolutionManager.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IQMProjectEvolutionManager.Core.Interfaces.Domain
{
    public interface IWorkLog : IDomain, IDataDomain
    {
        long WorkLogId { get; set; }
        int OnTimeId { get; set; }
        Staff StaffMember { get; set; }
        Task Task { get; set; }
        float HoursWorked { get; set; }
    }
}