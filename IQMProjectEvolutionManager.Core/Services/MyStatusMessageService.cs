using IQM.Common.Interfaces;
using IQM.Common.Services;
using IQMProjectEvolutionManager.Core.Domain;
using IQMProjectEvolutionManager.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IQMProjectEvolutionManager.Core.Services
{
    public class MyStatusMessageService : BasicGenericService<MyStatusMessage, IGenericRepository<MyStatusMessage>>, IMyStatusMessageService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MyStatusMessageService"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public MyStatusMessageService(IGenericRepository<MyStatusMessage> repository)
        {
            Repository = repository;
        }
    }
}