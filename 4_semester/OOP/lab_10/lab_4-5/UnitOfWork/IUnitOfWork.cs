using lab_4_5.Repositories;
using System;
using System.Threading.Tasks;

namespace lab_4_5.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        ISkinRepository Skins { get; }
        ICategoryRepository Categories { get; }
        Task<int> SaveAsync();
    }
}
