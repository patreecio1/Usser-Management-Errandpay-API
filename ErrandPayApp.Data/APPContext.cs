using Microsoft.EntityFrameworkCore;
using ErrandPayApp.Data.Entities;
using ErrandPayApp.Data.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErrandPayApp.Data
{
    public class APPContext : DbContext
    {
        public APPContext(DbContextOptions<APPContext> options)
       : base(options)
        {
        }


        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
           
        

            base.OnModelCreating(builder);
            FilterQuery(builder);
            IndexFields(builder);
           // builder.Seed();
        }
        private void IndexFields(ModelBuilder builder)
        {
            builder.Entity<AppUser>().HasIndex(x => x.Email).IsUnique();
            builder.Entity<Role>().HasIndex(x => x.RoleName).IsUnique();
            builder.Entity<Role>().HasIndex(x => x.RoleNameHash).IsUnique();
        }
        private void FilterQuery(ModelBuilder builder)
        {

        }
    }
}
