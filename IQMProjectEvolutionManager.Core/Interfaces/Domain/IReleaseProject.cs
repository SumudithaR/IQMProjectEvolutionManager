using IQMProjectEvolutionManager.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IQMProjectEvolutionManager.Core.Interfaces.Domain
{
    public interface IReleaseProject : IDomain
    {
        long ReleaseProjectId { get; set; }
        Release Release { get; set; }
        Project Project { get; set; }
    }
}