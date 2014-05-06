using IQM.Common.Repositories;
using IQMProjectEvolutionManager.Core.Domain;
using IQMProjectEvolutionManager.Core.Services;
using NUnit.Framework;

namespace IQMProjectEvolutionManager.Tests.Core.Services
{
    [TestFixture]
    public class ReleaseTypeServiceTestFixture
    {
        /// <summary>
        /// Determines whether the Milestone ReleaseType inserted by the base test fixture can be retrieved by its name.
        /// </summary>
        [TestCase]
        public void CanGetTheReleaseTypeByName()
        {
            var releaseType = new ReleaseTypeService(new BaseRepository<ReleaseType>()).GetByName("Milestone");
            Assert.IsNotNull(releaseType);
        }
    }
}
