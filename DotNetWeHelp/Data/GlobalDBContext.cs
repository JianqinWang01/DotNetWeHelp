using System;
using MySql.Data.EntityFrameworkCore.Extensions;
using MySql.Data.MySqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using DotNetWeHelp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace DotNetWeHelp.Data
{
    public class GlobalDBContext:IdentityDbContext
    {
        public GlobalDBContext(DbContextOptions<GlobalDBContext> options)
            :base(options)
        {
        }
        public DbSet<News> News { get; set; }
        //public DbSet<AppUser> AppUsers { get; set; }
        //public DbSet<AppRole> AppRoles { get; set; }
        //protected override void OnModelCreating(ModelBuilder builder)
        //{

        //    base.OnModelCreating(builder);

        //}
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .ConfigureWarnings(w => w.Log(CoreEventId.ManyServiceProvidersCreatedWarning));
        }
    }
}
