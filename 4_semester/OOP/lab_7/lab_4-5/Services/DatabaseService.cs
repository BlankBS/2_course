using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Threading.Tasks;
using lab_4_5.Models;

namespace lab_4_5.Services
{
    public class DatabaseService
    {
        private readonly string _connString = ConfigurationManager.ConnectionStrings["PubgDb"].ConnectionString;
        private readonly string _dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "PubgMarket.mdf");

        public DatabaseService()
        {
            EnsureDatabaseCreated();
        }

        private void EnsureDatabaseCreated()
        {
            if (!File.Exists(_dbPath)) CreateDatabase();
        }

        private void CreateDatabase()
        {
            string masterConn = @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True";
            using (var conn = new SqlConnection(masterConn))
            {
                conn.Open();
                new SqlCommand($@"CREATE DATABASE [PubgMarket] ON (NAME = 'PubgMarket', FILENAME = '{_dbPath}')", conn).ExecuteNonQuery();
            }

            using (var conn = new SqlConnection(_connString))
            {
                conn.Open();
                string script = @"
                    CREATE TABLE Skins (
                        Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
                        ShortName NVARCHAR(100),
                        Price DECIMAL(18,2),
                        Quantity INT,
                        Category NVARCHAR(50),
                        ImageBytes VARBINARY(MAX)
                    );
                    CREATE TABLE SalesLog (
                        Id INT PRIMARY KEY IDENTITY,
                        SkinId UNIQUEIDENTIFIER,
                        SaleDate DATETIME DEFAULT GETDATE()
                    );";
                new SqlCommand(script, conn).ExecuteNonQuery();
            }
        }

        public async Task<List<Skin>> GetAllSkinsAsync()
        {
            var list = new List<Skin>();
            using (var conn = new SqlConnection(_connString))
            {
                await conn.OpenAsync();
                var cmd = new SqlCommand("SELECT * FROM Skins", conn);
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new Skin
                        {
                            Id = reader.GetGuid(0),
                            ShortName = reader.GetString(1),
                            Price = reader.GetDecimal(2),
                            Quantity = reader.GetInt32(3),
                            Category = reader.GetString(4),
                            ImageBytes = reader["ImageBytes"] as byte[]
                        });
                    }
                }
            }
            return list;
        }

        public void AddSkin(Skin skin, byte[] image)
        {
            using (var conn = new SqlConnection(_connString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    var cmd = new SqlCommand(@"INSERT INTO Skins (ShortName, Price, Quantity, Category, ImageBytes) 
                                               VALUES (@n, @p, @q, @c, @i)", conn, trans);
                    cmd.Parameters.AddWithValue("@n", skin.ShortName);
                    cmd.Parameters.AddWithValue("@p", skin.Price);
                    cmd.Parameters.AddWithValue("@q", skin.Quantity);
                    cmd.Parameters.AddWithValue("@c", skin.Category);
                    cmd.Parameters.Add("@i", SqlDbType.VarBinary).Value = (object)image ?? DBNull.Value;

                    cmd.ExecuteNonQuery();
                    trans.Commit();
                }
                catch { trans.Rollback(); throw; }
            }
        }

        public void DeleteSkin(Guid id)
        {
            using (var conn = new SqlConnection(_connString))
            {
                conn.Open();
                var cmd = new SqlCommand("DELETE FROM Skins WHERE Id = @id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}