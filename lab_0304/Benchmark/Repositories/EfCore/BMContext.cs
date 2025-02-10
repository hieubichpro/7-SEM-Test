using Benchmark.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benchmark.Repositories.EfCore
{
    public partial class BenchmarkContext : DbContext
    {
        public DbSet<User> users { get; set; }

        public BenchmarkContext(DbContextOptions<BenchmarkContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Id)
                .IsRequired()
                .HasColumnName("id");

                entity.Property(x => x.Login)
                .IsRequired()
                .HasColumnName("login");

                entity.Property(x => x.Password)
                .IsRequired()
                .HasColumnName("password");

                entity.Property(x => x.Role)
                .IsRequired()
                .HasColumnName("role");

                entity.Property(x => x.Name)
                .IsRequired()
                .HasColumnName("name");
            });
        }

        }
}
