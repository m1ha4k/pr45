using System;
using API_Lobanov.Models;
using Microsoft.EntityFrameworkCore;

namespace API_Lobanov.Context
{
    public class UsersContext : DbContext
    {
        public DbSet<Users> Users { get; set; }
        public UsersContext() 
        { 
            Database.EnsureCreated();
            Users.Load();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=127.0.0.1;" +
                "uid=root;" +
                "port=3307;" +
                "pwd=;" +
                "database=TaskManager",
                new MySqlServerVersion(new Version(8, 0, 11))
                );
        }
    }
}
