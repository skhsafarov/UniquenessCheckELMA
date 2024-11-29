using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using UniquenessCheckELMA.Domain.Entities;

namespace UniquenessCheckELMA.Infrastructure
{
    public class DataContext : DbContext
    {
        public readonly IConfiguration _configuration;

        public DataContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    //.UseModel(DataContextModel.Instance)
                    .UseNpgsql(_configuration.GetConnectionString("DefaultConnection"))
                    .LogTo(Console.WriteLine, new[] { CoreEventId.QueryCompilationStarting, RelationalEventId.CommandExecuted })
                    .EnableSensitiveDataLogging();
            }
        }

        public DbSet<Application> Applications { get; set; }
        public DbSet<PhysicalPerson> PhysicalPersons { get; set; }
    }
}
