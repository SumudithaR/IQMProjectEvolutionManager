using IQMProjectEvolutionManager.Core.Interfaces.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IQMProjectEvolutionManager.Core.Domain
{
    public class UserNotifierProvider : IUserNotifierProvider
    {
        public virtual long UserNotifierProviderId { get; set; }
        public virtual string Name { get; set; }
        public virtual ICollection<UserNotifier> UserNotifiers { get; set; }
    }
}
