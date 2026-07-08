using lab_4_5.Models;
using lab_4_5.Services;

namespace lab_4_5.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(MarketContext context) : base(context) { }
    }
}
