using IQM.Common.Web.ViewModels;
using IQMProjectEvolutionManager.Core.Interfaces.Domain;

namespace IQMProjectEvolutionManager.Core.Domain
{
    public class MyStatusMessage : StatusMessage, IMyStatusMessage
    {
        public virtual long MyStatusMessageId { get; set; }
    }
}