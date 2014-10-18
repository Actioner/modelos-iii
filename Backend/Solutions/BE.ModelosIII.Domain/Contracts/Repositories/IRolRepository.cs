using SharpArch.Domain.PersistenceSupport;

namespace BE.ModelosIII.Domain.Contracts.Repositories
{
    public interface IRolRepository : IRepository<Role>
    {
        Role GetByName(string name);
    }
}
