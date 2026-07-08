using lab_4_5.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_4_5.Services
{
    public class MarketContext : DbContext
    {
        public MarketContext() : base("name=MarketContext") 
        {
            Database.SetInitializer(new MarketInitializer());
        }

        public DbSet<Skin> Skins { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
    public class MarketInitializer : CreateDatabaseIfNotExists<MarketContext>
    {
        protected override void Seed(MarketContext context)
        {
            context.Categories.Add(new Category { Name = "Оружие" });
            context.Categories.Add(new Category { Name = "Одежда" });
            context.Categories.Add(new Category { Name = "Транспорт" });

            base.Seed(context);
        }
    }
}
