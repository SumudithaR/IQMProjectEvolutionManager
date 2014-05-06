// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CoreServiceModule.cs" company="IQM Software">
//   Sumuditha Ranawaka 2014
// </copyright>
// <summary>
//   The core service module
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace IQMProjectEvolutionManager.Core
{
    using IQM.Common.Interfaces;
    using IQM.Common.Repositories;

    using IQMProjectEvolutionManager.Core.Interfaces.Services;
    using IQMProjectEvolutionManager.Core.Services;

    using Ninject.Modules;

    /// <summary>
    /// The core service module
    /// </summary>
    public class CoreServiceModule : NinjectModule
    {
        /// <summary>
        /// Loads the module into the kernel.
        /// </summary>
        public override void Load()
        {
            Bind<IProjectService>().To<ProjectService>();
            Bind<IReleaseProjectService>().To<ReleaseProjectService>();
            Bind<IReleaseService>().To<ReleaseService>();
            Bind<IReleaseTypeService>().To<ReleaseTypeService>();
            Bind<IReleaseStatusTypeService>().To<ReleaseStatusTypeService>();
            Bind<IReleaseWorkLogService>().To<ReleaseWorkLogService>();
            Bind<IStaffService>().To<StaffService>();
            Bind<ISubscriberService>().To<SubscriberService>();
            Bind<ISubscriberNotifierLogService>().To<SubscriberNotifierLogService>();
            Bind<ISubscriberNotifierPurposeService>().To<SubscriberNotifierPurposeService>();
            Bind<ISubscriberNotifierService>().To<SubscriberNotifierService>();
            Bind<ISubscriberNotifierTypeService>().To<SubscriberNotifierTypeService>();

            // Bind<IMyStatusMessageService>().To<MyStatusMessageService>();
            Bind(typeof(IGenericRepository<>)).To(typeof(BaseRepository<>));
        }
    }
}