using IQM.Common.Repositories;
using IQM.Common.Interfaces;


namespace ProjectName.Core.Repositories
{
    public class BaseRepository<T> : GenericRepository<T>, IGenericRepository<T>
    {
    }
}
