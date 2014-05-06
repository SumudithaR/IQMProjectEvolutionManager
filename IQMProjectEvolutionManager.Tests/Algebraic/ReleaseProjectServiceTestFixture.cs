// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReleaseProjectServiceTestFixture.cs" company="IQm Software">
//   Sumuditha Ranawaka 2014
// </copyright>
// <summary>
//   Defines the ReleaseProjectServiceTestFixture type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace IQMProjectEvolutionManager.Tests.Algebraic
{
    using System;

    using IQM.Common.Repositories;

    using IQMProjectEvolutionManager.Core.Domain;
    using IQMProjectEvolutionManager.Core.Services;

    using NUnit.Framework;

    /// <summary>
    /// The release project service test fixture.
    /// </summary>
    [TestFixture]
    public class ReleaseProjectServiceTestFixture
    {
        /// <summary>
        /// The test boundary values for get by project and release ids method.
        /// </summary>
        [TestCase]
        [Ignore]
        public void TestArbitaryValuesForGetByProjectAndReleaseIds()
        {
            var random = new Random();

            try
            {
                new ReleaseProjectService(new BaseRepository<ReleaseProject>()).GetByProjectAndReleaseIds(random.Next(int.MinValue, int.MaxValue), random.Next(int.MinValue, int.MaxValue));
            }
            catch (Exception)
            {
                Assert.Fail("GetByProjectAndReleaseIds cannot handle some arbitrary values.");
            }
        }

        /// <summary>
        /// The test boundary values for get by project and release ids.
        /// </summary>
        [Ignore]
        [TestCase]
        public void TestBoundaryValuesForGetByProjectAndReleaseIds()
        {
            try
            {
                for (var i = 0; i < 50; i++)
                {
                    new ReleaseProjectService(new BaseRepository<ReleaseProject>()).GetByProjectAndReleaseIds(long.MaxValue, long.MaxValue);
                    new ReleaseProjectService(new BaseRepository<ReleaseProject>()).GetByProjectAndReleaseIds(long.MinValue, long.MinValue);
                    new ReleaseProjectService(new BaseRepository<ReleaseProject>()).GetByProjectAndReleaseIds(long.MaxValue, long.MinValue);
                    new ReleaseProjectService(new BaseRepository<ReleaseProject>()).GetByProjectAndReleaseIds(long.MinValue, long.MaxValue);
                }
            }
            catch (Exception)
            {
                Assert.Fail("GetByProjectAndReleaseIds cannot handle the boundary values for a long data type.");
            }
        }
    }
}
