using IQM.Common.Domain;
using IQM.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IQMProjectEvolutionManager.Core.Interfaces.Domain
{
    public interface IDomain : IAudit, IDeletable
    {
    }
}