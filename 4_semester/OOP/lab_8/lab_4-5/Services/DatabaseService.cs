using lab_4_5.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;

namespace lab_4_5.Services
{
    public class DatabaseService
    {
        private readonly string _connectionString;
        private readonly string _masterString;
        private readonly string _databaseName;

        public DatabaseService()
        {
            try
            {
                _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                _masterString = ConfigurationManager.ConnectionStrings["MasterConnection"].ConnectionString;

                var builder = new SqlConnectionStringBuilder(_connectionString);
                _databaseName = builder.InitialCatalog;

                InitializeDatabase();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Критическая ошибка при запуске БД: " + ex.Message);
            }
        }

        private void InitializeDatabase()
        {
            using (SqlConnection masterConn = new SqlConnection(_masterString))
            {
                masterConn.Open();

                string checkDbSql = $"SELECT database_id FROM sys.databases WHERE name = '{_databaseName}'";
                SqlCommand checkCmd = new SqlCommand(checkDbSql, masterConn);
                object result = checkCmd.ExecuteScalar();

                if (result == null)
                {
                    string createDbSql = $"CREATE DATABASE [{_databaseName}]";
                    SqlCommand createCmd = new SqlCommand(createDbSql, masterConn);
                    createCmd.ExecuteNonQuery();

                    CreateTables();
                }
            }
        }

        private void CreateTables()
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string script = @"
                    IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Categories' AND xtype='U')
                    CREATE TABLE Categories (
                        Id INT PRIMARY KEY IDENTITY,
                        Name NVARCHAR(50) NOT NULL
                    );

                    IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Skins' AND xtype='U')
                    CREATE TABLE Skins (
                        Id UNIQUEIDENTIFIER PRIMARY KEY,
                        ShortName NVARCHAR(100) NOT NULL,
                        Price DECIMAL(18,2),
                        Quantity INT,
                        Category NVARCHAR(50),
                        Rarity NVARCHAR(50),
                        ImageBytes VARBINARY(MAX),
                        SoldCount INT DEFAULT 0
                    );

                    IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Users' AND xtype='U')
                    CREATE TABLE Users (
                        Id INT PRIMARY KEY IDENTITY,
                        Name NVARCHAR(50),
                        Email NVARCHAR(50)
                    );
                ";
                new SqlCommand(script, conn).ExecuteNonQuery();

                var checkCat = new SqlCommand("SELECT COUNT(*) FROM Categories", conn);
                if ((int)checkCat.ExecuteScalar() == 0)
                {
                    new SqlCommand("INSERT INTO Categories (Name) VALUES ('Оружие'), ('Одежда'), ('Транспорт')", conn).ExecuteNonQuery();
                }
            }
        }
        public async Task<List<Skin>> GetAllSkinsAsync(string filterName = "", string sortOrder = "ShortName")
        {
            List<Skin> skins = new List<Skin>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                string query = $"SELECT * FROM Skins WHERE ShortName LIKE @name ORDER BY {sortOrder}";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@name", "%" + filterName + "%");

                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        skins.Add(new Skin
                        {
                            Id = reader.GetGuid(reader.GetOrdinal("Id")),
                            ShortName = reader.GetString(reader.GetOrdinal("ShortName")),
                            Price = reader.GetDecimal(reader.GetOrdinal("Price")),
                            Quantity = reader.GetInt32(reader.GetOrdinal("Quantity")),
                            Category = reader.GetString(reader.GetOrdinal("Category")),
                            Rarity = reader.GetString(reader.GetOrdinal("Rarity")),

                            ImageBytes = reader["ImageBytes"] as byte[],

                            SoldCount = reader.GetInt32(reader.GetOrdinal("SoldCount"))
                        });
                    }
                }
            }
            return skins;
        }
        public void AddSkin(Skin skin, int categoryId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    string sql = @"INSERT INTO Skins (ShortName, Price, Quantity, CategoryId, ImageBytes) 
                           VALUES (@name, @price, @qty, @catId, @img)";

                    using (SqlCommand cmd = new SqlCommand(sql, conn, trans))
                    {
                        cmd.Parameters.AddWithValue("@name", skin.ShortName);
                        cmd.Parameters.AddWithValue("@price", skin.Price);
                        cmd.Parameters.AddWithValue("@qty", skin.Quantity);
                        cmd.Parameters.AddWithValue("@catId", categoryId);

                        byte[] imageData = null;
                        if (!string.IsNullOrEmpty(skin.ImagePath) && File.Exists(skin.ImagePath))
                        {
                            imageData = File.ReadAllBytes(skin.ImagePath);
                        }

                        cmd.Parameters.Add("@img", SqlDbType.VarBinary).Value = (object)imageData ?? DBNull.Value;

                        cmd.ExecuteNonQuery();
                    }
                    trans.Commit();
                }
                catch { trans.Rollback(); throw; }
            }
        }
        public void SaveSkin(Skin skin, bool isNew)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();
                try
                {
                    SqlCommand cmd;
                    if (isNew)
                    {
                        if (skin.Id == Guid.Empty) skin.Id = Guid.NewGuid();

                        cmd = new SqlCommand(@"INSERT INTO Skins (Id, ShortName, Price, Quantity, Category, Rarity, ImageBytes, SoldCount) 
                                     VALUES (@Id, @Name, @Price, @Qty, @Cat, @Rar, @Bytes, @Sold)", conn, transaction);
                    }
                    else
                    {
                        cmd = new SqlCommand(@"UPDATE Skins SET ShortName=@Name, Price=@Price, Quantity=@Qty, 
                                     Category=@Cat, Rarity=@Rar, ImageBytes=@Bytes WHERE Id=@Id", conn, transaction);
                    }

                    cmd.Parameters.AddWithValue("@Id", skin.Id);
                    cmd.Parameters.AddWithValue("@Name", skin.ShortName);
                    cmd.Parameters.AddWithValue("@Price", skin.Price);
                    cmd.Parameters.AddWithValue("@Qty", skin.Quantity);
                    cmd.Parameters.AddWithValue("@Cat", skin.Category);
                    cmd.Parameters.AddWithValue("@Rar", skin.Rarity);

                    if (!string.IsNullOrEmpty(skin.ImagePath) && File.Exists(skin.ImagePath))
                    {
                        skin.ImageBytes = File.ReadAllBytes(skin.ImagePath);
                    }

                    cmd.Parameters.Add("@Bytes", SqlDbType.VarBinary).Value = (object)skin.ImageBytes ?? DBNull.Value;

                    if (isNew) cmd.Parameters.AddWithValue("@Sold", skin.SoldCount);

                    cmd.ExecuteNonQuery();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        public void DeleteSkin(Guid id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM Skins WHERE Id = @id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateStockProcedure(Guid skinId, int change)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("sp_UpdateStock", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SkinId", skinId);
                cmd.Parameters.AddWithValue("@Change", change);
                cmd.ExecuteNonQuery();
            }
        }
    }
}