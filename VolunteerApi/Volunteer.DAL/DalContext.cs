using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Volunteer.DAL.Entities;

namespace Volunteer.DAL
{
    public class DalContext : DbContext
    {
        public DalContext(DbContextOptions<DalContext> options) : base(options) { }

        public DbSet<UserAccount> Users { get; set; }
    }
}
