using lab_4_5.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace lab_4_5.Repositories
{
    public interface ISkinRepository : IRepository<Skin>
    {
        Task<IEnumerable<Skin>> GetSkinsAsync(string search, string sortOrder);
        Task<Skin> GetByIdWithCategoryAsync(Guid id);
    }
}
