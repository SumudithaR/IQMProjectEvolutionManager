using System.Collections.Generic;
using System.Web.Mvc;
using IQM.Common;
using ProjectName.Core;
using NHibernate;
using NHibernate.Context;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;
using Ninject;
using Ninject.Modules;
using IQM.Common.Web.Utility;

namespace ProjectName.Tests
{
    /// <summary>
    /// The NHibernate test fixture.
    /// </summary>
    public class NHibernateTestFixtureBase
    {
        /// <summary>
        /// Gets or sets the kernel.
        /// </summary>
        /// <value>
        /// The kernel.
        /// </value>
        public IKernel Kernel { get; set; }

        /// <summary>
        /// Setups the dependency injection.
        /// </summary>
        /// <returns>The currently configured kernel</returns>
        public IKernel SetupDependencyInjection()
        {
            var modules = new List<INinjectModule>();
            modules.Add(new CoreServiceModule());

            // Create Ninject DI Kernel 
            IKernel kernel = new StandardKernel(modules.ToArray());

            // Tell ASP.NET MVC 3 to use our Ninject DI Container 
            ////MvcServiceLocator.SetCurrent(new NinjectServiceLocator(kernel));

            ////kernel.Bind<IDocumentStorageService>().To<S3DocumentStorageService>();

            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));

            return kernel;
        }

        /// <summary>
        /// Creates the initial data.
        /// </summary>
        public virtual void CreateInitialData() { }

        /// <summary>
        /// Setups the context.
        /// </summary>
        [SetUp]
        public virtual void SetupContext()
        {
            Kernel = this.SetupDependencyInjection();
            var session = NHibernateHelper.CreateSessionFactory().OpenSession();
            CurrentSessionContext.Bind(session);
            var se = new SchemaExport(NHibernateHelper.Configuration);
            // drop the old schema
            se.Drop(false, true);
            // create the new schema
            se.Create(false, true);

            CreateInitialData();
        }

        public ISession GetSession()
        {
            return NHibernateHelper.CreateSessionFactory().GetCurrentSession();
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [TearDown]
        public virtual void TearDown()
        {
            try
            {
                ISession currentSession = CurrentSessionContext.Unbind(NHibernateHelper.CreateSessionFactory());
                if (currentSession != null)
                {
                    currentSession.Close();
                    currentSession.Dispose();
                }
            }
            catch
            {
                
            }
        }
    }
}
