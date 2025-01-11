using ASP_NET_MVC_Shop_Example.Models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace ASP_NET_MVC_Shop_Example.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Настройка для использования double в базе данных.

            // К сожалению, SQLite не имеет тип данных decimal.
            // Поэтому в модели товара используется тип double и здесь явно указано, что колонка БД будет типа double (иначе будет string).

            // Если всё-таки необходим тип decimal, то необходимо писать промежуточные классы Data Transfer Object (DTO) - классы, которые будут использоваться исключительно для БД.
            // После считывания данных они будут преобразовываться в классы модели и тип double из SQLite нужно конвертировать в decimal для C# классов.
            // Основной минус такого подхода -  увеличение количества (бессмысленных) классов-прослоек. 
            // Поэтому для примера мы просто поменяли тип на double в модели Product.

            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("REAL");
        }

        public void RegisterSQLiteToLower()
        {
            var connection = this.Database.GetDbConnection();
            connection.Open();

            // Регистрация функции LOWER
            if (connection is SqliteConnection sqliteConnection)
                sqliteConnection.CreateFunction("lower", new Func<string, string>((s) => s?.ToLowerInvariant()));
        }
    }
}