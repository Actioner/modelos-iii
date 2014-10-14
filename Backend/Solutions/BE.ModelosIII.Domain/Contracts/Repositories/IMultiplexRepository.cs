using SharpArch.Domain.PersistenceSupport;

namespace BE.ModelosIII.Domain.Contracts.Repositories
{
    public interface IMultiplexRepository : IRepository<Multiplex>
    {
        Multiplex GetByName(string name);
    }
}