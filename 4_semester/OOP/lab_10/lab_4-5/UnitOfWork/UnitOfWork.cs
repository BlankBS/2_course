using lab_4_5.Repositories;
using lab_4_5.Services;
using System.Threading.Tasks;

namespace lab_4_5.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MarketContext _context;
        private SkinRepository _skins;
        private CategoryRepository _categories;

        public UnitOfWork()
        {
            _context = new MarketContext();
        }

        public ISkinRepository Skins =>
            _skins ?? (_skins = new SkinRepository(_context));

        public ICategoryRepository Categories =>
            _categories ?? (_categories = new CategoryRepository(_context));

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
