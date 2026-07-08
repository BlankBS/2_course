using lab_4_5.Models;
using lab_4_5.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace lab_4_5.Repositories
{
    public class SkinRepository : Repository<Skin>, ISkinRepository
    {
        public SkinRepository(MarketContext context) : base(context) { }

        public async Task<IEnumerable<Skin>> GetSkinsAsync(string search, string sortOrder)
        {
            var query = _dbSet.Include(s => s.Category).AsQueryable();

            if (!string.IsNullOrEmpty(search))
                query = query.Where(s => s.ShortName.Contains(search));

            switch (sortOrder)
            {
                case "Price": query = query.OrderBy(s => s.Price); break;
                default:      query = query.OrderBy(s => s.ShortName); break;
            }

            return await query.ToListAsync();
        }

        public async Task<Skin> GetByIdWithCategoryAsync(Guid id)
        {
            return await _dbSet.Include(s => s.Category)
                               .FirstOrDefaultAsync(s => s.Id == id);
        }
    }
}
