using SharpArch.Domain.PersistenceSupport;

namespace BE.ModelosIII.Domain.Contracts.Repositories
{
    public interface IPriceRepository : IRepository<Price>
    {
        Price GetGeneralPrice();
    }
}