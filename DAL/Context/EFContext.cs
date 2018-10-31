using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using EntityLayer.Login;

namespace DAL.Context
{
    public class EfContext : IdentityDbContext<AppUser>
    {
        public EfContext(DbContextOptions<EfContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        //public DbSet<Product> Product { get; set; }
    }
}
