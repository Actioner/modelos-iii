using SharpArch.Domain.PersistenceSupport;

namespace BE.ModelosIII.Domain.Contracts.Repositories
{
    public interface IScenarioRepository : IRepository<Scenario>
    {
        Scenario GetByName(string name);
    }
}
