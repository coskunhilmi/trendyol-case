using Entities.Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EntityFramework.Contexts
{
    public class TrendyolDbContext : DbContext
    {
        IConfiguration _configuration;
        public TrendyolDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public virtual DbSet<Request> Requests { get; set; }
        public virtual DbSet<Response> Responses { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_configuration.GetConnectionString("DefaultConnection"));
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
