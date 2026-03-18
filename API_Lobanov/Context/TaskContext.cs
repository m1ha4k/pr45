using System;
using API_Lobanov.Models;
using Microsoft.EntityFrameworkCore;

namespace API_Lobanov.Context
{
    public class TaskContext : DbContext
    {
        public DbSet<Tasks> Tasks { get; set; }
        public TaskContext()
        {
            Database.EnsureCreated();
            Tasks.Load();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=127.0.0.1;" +
                "uid=root;" +
                "pwd=;" +
                "database=TaskManager",
                new MySqlServerVersion(new Version(8, 0, 11))
                );
        }
    }
}
