using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MVC_CORE.DDL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_CORE.DDL.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Book> books { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Admin",
                    NormalizedName = "admin",
                    ConcurrencyStamp = Guid.NewGuid().ToString()

                },
                new IdentityRole()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "User",
                    NormalizedName = "user",
                    ConcurrencyStamp = Guid.NewGuid().ToString()

                }

                );


            base.OnModelCreating(builder);
        }
    }
}
