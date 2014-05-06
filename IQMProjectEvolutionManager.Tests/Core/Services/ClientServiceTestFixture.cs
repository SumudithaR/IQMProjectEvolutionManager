using System;
using IQM.Common;
using ProjectName.Core.ViewModels.SearchViewModels;
using Ninject;
using NUnit.Framework;
using ProjectName.Core.Services;
using ProjectName.Core.Domain;

namespace ProjectName.Tests.Core.Services
{
    /// <summary>
    /// The test fixutre for the Client Service
    /// </summary>
    [TestFixture]
    public class ClientServiceTestFixture : NHibernateTestFixtureBase
    {
        #region Data
        /// <summary>
        /// The list of clients
        /// </summary>
        private Client[] _clients = new[]
            {
                new Client 
                {
                    CompanyName = "So Square",
                    Email = "admin@sosquare.com",
                    Telephone = "02085731885",
                    LiabilityInsuranceExpiry = DateTime.Today.AddDays(30)
                },
                new Client
                {
                    CompanyName = "Muffins and Cupcakes",
                    Email = "TracyMinkins@mandc.co.uk",
                    Telephone = "02080391744",
                    LiabilityInsuranceExpiry = DateTime.Today.AddDays(-15).AddMonths(10),
                }
            };
        #endregion

        /// <summary>
        /// Creates the initial data.
        /// </summary>
        public override void CreateInitialData()
        {
            var session = NHibernateHelper.GetSession();

            foreach (var client in _clients)
            {
                session.Save(client);
            }

 	        base.CreateInitialData();
        }

        /// <summary>
        /// Determines whether this instance [can get by id].
        /// </summary>
        [Test]
        public void CanGetById()
        {
            var client = Kernel.Get<ClientService>();
            var amountOfClients = client.GetAll().Count;
            for (long i = 1; i <= amountOfClients; i++)
            {
                Assert.IsNotNull(client.GetById(i).Data);
            }
        }

        /// <summary>
        /// Tries to get viewmodel of non exsiting client
        /// </summary>
        [Test]
        public void CanNotGetById()
        {
            var client = Kernel.Get<ClientService>();
            long amountOfClients = client.GetAll().Count;
            var viewModel = client.GetById(amountOfClients + 1).Data;
            Assert.IsNull(viewModel);
        }

        /// <summary>
        /// Determines whether this instance can save.
        /// </summary>
        [Test]
        public void CanSave()
        {
            // Get the service.
            var client = Kernel.Get<ClientService>();

            // Create a new client to save.
            var newClient = new Client
            {
                CompanyName = "Noodles R Us",
                Email = "PeteGolding@noodles.co.uk",
                Telephone = "03459123455",
                LiabilityInsuranceExpiry = DateTime.Today.AddDays(5).AddMonths(7),
            };

            // Save the client.
            client.Save(newClient);
            var newClientId = newClient.ClientId;

            // retrieve the client 
            newClient = client.GetById(newClientId).Data;

            // Assert that it is saved.
            Assert.IsNotNull(newClient);
        }

        /// <summary>
        /// Determines whether this instance can remove.
        /// </summary>
        [Test]
        public void CanRemove()
        {
            // Get the service
            var client = Kernel.Get<ClientService>();

            // Create a client
            var newClient = new Client
            {
                CompanyName = "Noodles R Us",
                Email = "PeteGolding@noodles.co.uk",
                Telephone = "03459123455",
                LiabilityInsuranceExpiry = DateTime.Today.AddDays(5).AddMonths(7),
            };

            // Save the client.
            client.Save(newClient);
            var newClientId = newClient.ClientId;

            // Retrieve the client we just saved to make sure its not null.
            newClient = client.GetById(newClientId).Data;
            Assert.IsNotNull(newClient);

            // Remove the client we just saved.
            client.Remove(newClient);

            // Assert that it is no longer there.
            Assert.IsNull(client.GetById(newClientId).Data);
         }

        /// <summary>
        /// Determines whether this instance [can get all].
        /// </summary>
        [Test]
        public void CanGetAll()
        {
            var client = Kernel.Get<ClientService>();
            var amountOfClients = client.GetAll().Count;
            Assert.AreEqual(amountOfClients, _clients.Length);
        }

        /// <summary>
        /// Determines whether this instance [can search client].
        /// </summary>
        [Test]
        public void CanSearchClient()
        {
            var client = Kernel.Get<ClientService>();
            var a = new ClientPagedSearchViewModel { PageNumber = 10 };
            client.Search(a, 1, 1);
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        public override void TearDown()
        {
            foreach (var client in _clients)
            {
                client.ClientId = 0;
            }

            base.TearDown();
        }
    }
}
