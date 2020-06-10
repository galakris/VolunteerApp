using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Volunteer.DAL.Entities;
using Volunteer.DAL.Relations;

namespace Volunteer.DAL
{
    public class DalContext : DbContext
    {
        public DalContext(DbContextOptions<DalContext> options) : base(options) { }

        public DbSet<UserAccountEntity> Users { get; set; }

        public DbSet<RoleEntity> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RoleUserAccount>()
                .HasKey(x => new { x.RoleId, x.UserAccountId });

            modelBuilder.Entity<RoleUserAccount>()
                .HasOne(x => x.Role)
                .WithMany(x => x.RoleUserAccount)
                .HasForeignKey(x => x.RoleId);

            modelBuilder.Entity<RoleUserAccount>()
                .HasOne(x => x.UserAccount)
                .WithMany(x => x.RoleUserAccount)
                .HasForeignKey(x => x.UserAccount);
        }
    }
}
