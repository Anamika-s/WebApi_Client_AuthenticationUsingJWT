using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication16.Models
{
    [Table("tblempl")]
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Batch { get; set; }
        public int Marks { get; set; }

    }

    public class StudentDbContext : DbContext
    {
        public StudentDbContext(DbContextOptions<StudentDbContext> options)
            : base(options) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasData(new User
                {
                    Id = 1,
                    UserName = "user1",
                    Password = "user1"
                },
                new User
                {
                    Id = 2,
                    UserName = "user2",
                    Password = "user2"
                },
                new User
                {
                    Id = 3,
                    UserName = "user3",
                    Password = "user3"
                }
                );
        }
        }
    }
