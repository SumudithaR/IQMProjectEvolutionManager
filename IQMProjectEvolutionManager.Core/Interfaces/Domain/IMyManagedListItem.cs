using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IQMProjectEvolutionManager.Core.Interfaces.Domain
{
    public interface IMyManagedListItem
    {
        long MyManagedListItemId { get; set; }
        long GetId();
    }
}
