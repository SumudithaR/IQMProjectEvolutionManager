using IQM.Common.Domain;
using IQMProjectEvolutionManager.Core.Interfaces.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IQMProjectEvolutionManager.Core.Domain
{
    public class WorkLog : IWorkLog
    {
        public virtual long WorkLogId { get; set; }
        public virtual int OnTimeId { get; set; }
        public virtual Staff StaffMember { get; set; }
        public virtual Task Task { get; set; }
        public virtual float HoursWorked { get; set; }
    }
}