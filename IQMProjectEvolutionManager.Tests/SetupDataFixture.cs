// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SetupDataFixture.cs" company="IQM Software">
//   Sumuditha Ranawaka 2014. 
// </copyright>
// <summary>
//   Defines the SetupDataFixture type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace IQMProjectEvolutionManager.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    using IQM.Common.Test.Core;

    using IQMProjectEvolutionManager.Core;
    using IQMProjectEvolutionManager.Core.Domain;
    using IQMProjectEvolutionManager.Core.Enums;

    using NUnit.Framework;

    /// <summary>
    /// The test fixture responsible for setting up the database and adding the test data. 
    /// </summary>
    [TestFixture]
    public class SetupDataFixture : NHibernateTestFixtureBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SetupDataFixture"/> class.
        /// </summary>
        public SetupDataFixture()
            : base(new CoreServiceModule())
        {
        }

        /// <summary>
        /// Setups the databases.
        /// </summary>
        [Test]
        public void SetupDatabase()
        {
            Assert.That(true, "Setup Database Complete.");
        }

        /// <summary>
        /// Creates the initial data.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1123:DoNotPlaceRegionsWithinElements", Justification = "Reviewed. Suppression is OK here.")]
        public override void CreateInitialData()
        {
            var objectsToSave = new List<object>();

            #region Release Types

            ReleaseType[] releaseTypes = { new ReleaseType { Name = "Milestone", OnTimeId = 1, } };

            objectsToSave.AddRange(releaseTypes);

            #endregion

            #region Release Status Types

            ReleaseStatusType[] releaseStatusTypes =
                {
                    new ReleaseStatusType
                        {
                            Name = "In Progress",
                            OnTimeId = 1,
                            ReleaseTypeId = 1
                        }
                };

            objectsToSave.AddRange(releaseStatusTypes);

            #endregion

            #region Projects

            Project[] projects =
                {
                    new Project { Name = "TestProject1", OnTimeId = 1, IsActive = true },
                    new Project { Name = "TestProject2", OnTimeId = 2, IsActive = false }
                };

            objectsToSave.AddRange(projects);

            #endregion

            #region Releases

            Release[] releases =
                {
                    new Release
                        {
                            Name = "TestRelease1",
                            DueDate = DateTime.Now,
                            HoursRemaining = 10,
                            HoursWorked = 10,
                            OriginalEstimateForAllTasks = 20,
                            PercentageComplete = 50,
                            ReleaseNotes = "Test note1",
                            IsActive = true,
                            ReleaseType = releaseTypes[0],
                            ReleaseStatusType = releaseStatusTypes[0],
                            ParentReleaseId = 0,
                            OnTimeId = 1
                        },
                    new Release
                        {
                            Name = "TestRelease2",
                            DueDate = DateTime.Now,
                            HoursRemaining = 20,
                            HoursWorked = 20,
                            OriginalEstimateForAllTasks = 40,
                            PercentageComplete = 80,
                            ReleaseNotes = "Test note2",
                            IsActive = true,
                            ReleaseType = releaseTypes[0],
                            ReleaseStatusType = releaseStatusTypes[0],
                            ParentReleaseId = 0,
                            OnTimeId = 2
                        }
                };

            objectsToSave.AddRange(releases);

            #endregion

            #region Release Projects

            ReleaseProject[] releaseProjects =
                {
                    new ReleaseProject { Release = releases[0], Project = projects[0] },
                    new ReleaseProject { Release = releases[0], Project = projects[1] },
                    new ReleaseProject { Release = releases[1], Project = projects[0] }
                };

            objectsToSave.AddRange(releaseProjects);

            #endregion

            #region Staff

            Staff[] staff =
                {
                    new Staff
                        {
                            FirstName = "TestFirstName1",
                            LastName = "TestLastName1",
                            OnTimeId = 1,
                            IsActive = true
                        },
                    new Staff
                        {
                            FirstName = "TestFirstname2",
                            LastName = "TestLastName2",
                            OnTimeId = 2,
                            IsActive = false
                        }
                };

            objectsToSave.AddRange(staff);

            #endregion

            #region Release Work Logs

            ReleaseWorkLog[] releaseWorkLogs =
                {
                    new ReleaseWorkLog
                        {
                            Staff = staff[0],
                            Release = releases[0],
                            HoursRemainingOnRelease = 50,
                            HoursWorkedOnReleaseInLastWeek = 40,
                            HoursWorkedOnRelease = 80
                        }
                };

            objectsToSave.AddRange(releaseWorkLogs);

            #endregion

            #region Subscriber Notifier Purposes

            SubscriberNotifierPurpose[] subscriberNotifierPurposes =
                {
                    new SubscriberNotifierPurpose
                        {
                            Name =
                                "Release"
                        }
                };
            objectsToSave.AddRange(subscriberNotifierPurposes);

            #endregion

            #region Subscriber Notifier Types

            SubscriberNotifierType[] subscriberNotifierTypes =
                {
                    new SubscriberNotifierType { Name = "Calendar" },
                    new SubscriberNotifierType { Name = "SMS" }
                };
            objectsToSave.AddRange(subscriberNotifierTypes);

            #endregion

            #region DropDownItem

            #region PagingDataItems

            var defaultPaging = new MyManagedListItem
                                    {
                                        ListItemType = MyManagedListItemType.Paging,
                                        Name = "default-paging",
                                        MetaData = "10",
                                        Visible = true
                                    };
            var ajaxPaging = new MyManagedListItem
                                 {
                                     ListItemType = MyManagedListItemType.Paging,
                                     Name = "ajax-paging",
                                     MetaData = "100",
                                     Visible = true
                                 };

            objectsToSave.Add(defaultPaging);
            objectsToSave.Add(ajaxPaging);

            #endregion

            #endregion

            #region Saving Of Test Data

            var session = GetSession();
            using (var transaction = session.BeginTransaction())
            {
                foreach (var obj in objectsToSave)
                {
                    session.Save(obj);
                }

                transaction.Commit();
            }

            #endregion
        }

        /// <summary>
        /// Creates the database.
        /// </summary>
        [Test]
        public void CreateDatabase()
        {
        }
    }
}