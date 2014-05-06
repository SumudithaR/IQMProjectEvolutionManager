using IQM.Common.Interfaces;
using IQM.Common.Services;
using IQM.Common.Web.Interfaces;
using IQMProjectEvolutionManager.Core.Domain;
using IQMProjectEvolutionManager.Core.DomainWrappers;
using IQMProjectEvolutionManager.Core.DomainWrappers.SearchDomainWrappers;
using IQMProjectEvolutionManager.Core.Services.BaseServices;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using IQMProjectEvolutionManager.Core.Interfaces.Services;

namespace IQMProjectEvolutionManager.Core.Services
{
    public class TaskService : DataDomainBaseService<Task, TaskDomainWrapper, TaskPagedSearchDomainWrapper>, ITaskService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ClientService"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public TaskService(IGenericRepository<Task> repository)
        {
            Repository = repository;
        }

        /// <summary>
        /// Searches the specified search view model.
        /// </summary>
        /// <param name="pagedSearchViewModel">The search view model.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>The Client Search View Model</returns>
        public override TaskPagedSearchDomainWrapper Search(TaskPagedSearchDomainWrapper pagedSearchViewModel, int pageNumber, int pageSize)
        {
            var query = Repository.GetAll();

            /*
             * Implement the criteria here.
             */
            //if (!string.IsNullOrEmpty(pagedSearchViewModel.CompanyName))
            //{
            //    query = query.Where(client => client.CompanyName.Contains(pagedSearchViewModel.CompanyName));
            //}

            var premises = query.ToList();
            pagedSearchViewModel.Data = new PagedList<Task>(premises.ToList(), pageNumber, pageSize);
            return pagedSearchViewModel;
        }

        public bool IsUpdated(Task task)
        {
            var currentTask = GetAll().SingleOrDefault(tsk => tsk.OnTimeId == task.OnTimeId);

            if (currentTask != null)
            {
                if (!(currentTask.Name.Equals(task.Name) || currentTask.Release.OnTimeId == task.OnTimeId)
                    //|| currentTask.ResponsibleMember.OnTimeId == task.OnTimeId
                    )
                {
                    return true;
                }
            }

            return false;
        }

        public void InsertOrUpdate(Task task)
        {
            if (InDatabase(task))
            {
                if (IsUpdated(task))
                {
                    Repository.Remove(GetByOnTimeId(task.OnTimeId));
                    Repository.Save(task);
                }
            }
            else
            {
                Repository.Save(task);
            }
        }
    }
}