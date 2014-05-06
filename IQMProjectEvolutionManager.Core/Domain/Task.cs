using IQM.Common.Domain;
using IQM.Common.Interfaces;
using IQMProjectEvolutionManager.Core.Interfaces.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IQMProjectEvolutionManager.Core.Domain
{
    public class Task : ITask
    {
        public virtual long TaskId { get; set; }
        public virtual int OnTimeId { get; set; }
        public virtual string Name { get; set; }
        public virtual Staff AssignedStaffMember { get; set; }
        public virtual Release Release { get; set; }
    }
}