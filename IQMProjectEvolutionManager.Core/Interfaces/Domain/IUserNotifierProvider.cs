using IQMProjectEvolutionManager.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IQMProjectEvolutionManager.Core.Interfaces.Domain
{
    public interface IUserNotifierProvider : IDomain
    {
        long UserNotifierProviderId { get; set; }
        string Name { get; set; }
        ICollection<UserNotifier> UserNotifiers { get; set; }
    }
}
