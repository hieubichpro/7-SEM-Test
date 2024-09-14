using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lab_03.BL.Models;
using Microsoft.EntityFrameworkCore;

namespace lab_01.DA.dbContext
{
    public partial class AppDbContext : DbContext
    {
        public DbSet<User> users { get; set; }
        public DbSet<League> leagues { get; set; }
        public DbSet<Club> clubs { get; set; }
        public DbSet<Match> matches { get; set; }
        public DbSet<ClubLeague> clubleagues { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");
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

            modelBuilder.Entity<Club>(entity =>
            {
                entity.ToTable("clubs");
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Id)
                .IsRequired()
                .HasColumnName("id");

                entity.Property(x => x.Name)
                .IsRequired()
                .HasColumnName ("name");
            });

            modelBuilder.Entity<Match>(entity =>
            {
                entity.ToTable("matches");
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Id).IsRequired().HasColumnName("id");
                entity.Property(x => x.GoalHomeTeam).HasColumnName("goal_home_club");
                entity.Property(x => x.GoalGuestTeam).HasColumnName("goal_guest_club");
                entity.Property(x => x.IdLeague).IsRequired().HasColumnName("id_league");
                entity.Property(x => x.IdHomeTeam).IsRequired().HasColumnName("id_home_club");
                entity.Property(x => x.IdGuestTeam).IsRequired().HasColumnName("id_guest_club");
            });

            modelBuilder.Entity<League>(entity =>
            {
                entity.ToTable("leagues");
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Id)
                .IsRequired()
                .HasColumnName("id");

                entity.Property(x => x.Name)
                .IsRequired()
                .HasColumnName("name");

                entity.Property(x => x.IdUser)
                .IsRequired()
                .HasColumnName("id_user");
            });

            modelBuilder.Entity<ClubLeague>(entity =>
            {
                entity.ToTable("leagueclub");
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Id)
                .IsRequired()
                .HasColumnName("id");

                entity.Property(x => x.IdClub)
                .IsRequired()
                .HasColumnName("id_club");

                entity.Property(x => x.IdLeague)
                .IsRequired()
                .HasColumnName("id_league");
            });
        }
    }
}

