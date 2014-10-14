using System;
using System.Collections.Generic;
using SharpArch.Domain.PersistenceSupport;

namespace BE.ModelosIII.Domain.Contracts.Repositories
{
    public interface IPromotionRepository : IRepository<Promotion>
    {
        IList<Promotion> GetAvailable(DateTime date);
    }
}