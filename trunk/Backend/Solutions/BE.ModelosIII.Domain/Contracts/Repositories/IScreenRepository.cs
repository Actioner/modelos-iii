using System.Collections.Generic;
using SharpArch.Domain.PersistenceSupport;

namespace BE.ModelosIII.Domain.Contracts.Repositories
{
    public interface IScreenRepository : IRepository<Screen>
    {
        IList<Screen> GetByMultiplex(Multiplex multiplex);
        Screen GetByMultiplexAndName(Multiplex multiplex, string name);
    }
}