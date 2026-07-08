using lab_4_5.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_4_5.Services
{
    public class EfDatabaseService
    {
        public async Task<List<Skin>> GetSkinsAsync(string search, string sortOrder)
        {
            using (var db = new MarketContext())
            {
                var query = db.Skins.Include(s => s.Category);

                if (!string.IsNullOrEmpty(search))
                    query = query.Where(s => s.ShortName.Contains(search));

                switch (sortOrder)
                {
                    case "Price": query = query.OrderBy(s => s.Price); break;
                    default: query = query.OrderBy(s => s.ShortName); break;
                }

                return await query.ToListAsync();
            }
        }

        public async Task AddSkinAsync(Skin skin)
        {
            using (var db = new MarketContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        db.Skins.Add(skin);
                        await db.SaveChangesAsync();
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public async Task UpdateSkinAsync(Skin updatedSkin)
        {
            using (var db = new MarketContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        var existingSkin = await db.Skins.FindAsync(updatedSkin.Id);

                        if (existingSkin != null)
                        {
                            existingSkin.ShortName = updatedSkin.ShortName;
                            existingSkin.Price = updatedSkin.Price;
                            existingSkin.Quantity = updatedSkin.Quantity;
                            existingSkin.CategoryId = updatedSkin.CategoryId;
                            existingSkin.ImageBytes = updatedSkin.ImageBytes;
                            existingSkin.Rarity = updatedSkin.Rarity;

                            await db.SaveChangesAsync();

                            transaction.Commit();
                        }
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            using (var db = new MarketContext())
            {
                return await db.Categories.ToListAsync();
            }
        }

        public async Task DeleteSkinAsync(Guid id)
        {
            using (var db = new MarketContext())
            {
                var skin = await db.Skins.FindAsync(id);
                if (skin != null)
                {
                    db.Skins.Remove(skin);
                    await db.SaveChangesAsync();
                }
            }
        }
    }
}
