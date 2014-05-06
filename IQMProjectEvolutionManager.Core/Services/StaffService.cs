using IQM.Common.Interfaces;
using IQM.Common.Services;
using IQM.Common.Web.Interfaces;
using IQMProjectEvolutionManager.Core.Domain;
using IQMProjectEvolutionManager.Core.DomainWrappers;
using IQMProjectEvolutionManager.Core.DomainWrappers.SearchDomainWrappers;
using IQMProjectEvolutionManager.Core.Interfaces.Services;
using IQMProjectEvolutionManager.Core.Services.BaseServices;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IQMProjectEvolutionManager.Core.Services
{
    public class StaffService : OnTimeDomainBaseService<Staff, StaffDomainWrapper, StaffPagedSearchDomainWrapper>, IStaffService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ClientService"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public StaffService(IGenericRepository<Staff> repository)
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
        public override StaffPagedSearchDomainWrapper Search(StaffPagedSearchDomainWrapper pagedSearchViewModel, int pageNumber, int pageSize)
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
            pagedSearchViewModel.Data = new PagedList<Staff>(premises.ToList(), pageNumber, pageSize);
            return pagedSearchViewModel;
        }

        public bool IsUpdated(Staff staff)
        {
            var currentStaffMember = GetAll().SingleOrDefault(stf => stf.OnTimeId == staff.OnTimeId);

            if (currentStaffMember != null)
            {
                if (!currentStaffMember.FirstName.Equals(staff.FirstName) || !currentStaffMember.LastName.Equals(staff.LastName) || currentStaffMember.IsActive != staff.IsActive)
                {
                    return true;
                }
            }

            return false;
        }

        public void InsertOrUpdate(Staff staff)
        {
            if (InDatabase(staff))
            {
                var currentStaffMember = GetByOnTimeId(staff.OnTimeId);

                currentStaffMember.FirstName = staff.FirstName;
                currentStaffMember.LastName = staff.LastName;
                currentStaffMember.IsActive = staff.IsActive;
                currentStaffMember.Edited = DateTime.Now;

                Repository.Save(currentStaffMember);
            }
            else
            {
                Repository.Save(staff);
            }
        }

        /// <summary>
        /// The get older by days.
        /// </summary>
        /// <param name="days">
        /// The days.
        /// </param>
        /// <returns>
        /// The <see cref="ICollection"/>.
        /// </returns>
        public ICollection<Staff> GetOlderByDays(int days)
        {
            return
                this.GetAll()
                    .Where(rele => rele.Edited != null && (DateTime)rele.Edited <= DateTime.Now.AddDays(days))
                    .ToList();
        }

        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="staffMembers">
        /// The staff members.
        /// </param>
        public void Delete(ICollection<Staff> staffMembers)
        {
            foreach (var staffMember in staffMembers.Where(staffMember => staffMember != null))
            {
                staffMember.DeleteOn = DateTime.Now;
                this.Repository.Remove(staffMember);
            }
        }
    }
}