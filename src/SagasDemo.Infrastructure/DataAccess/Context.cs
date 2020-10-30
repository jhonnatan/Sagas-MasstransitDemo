using Microsoft.EntityFrameworkCore;
using SagasDemo.Infrastructure.DataAccess.Map;
using SagasDemo.Infrastructure.MassTransit.StateMachines;
using System;
using System.Collections.Generic;

namespace SagasDemo.Infrastructure.DataAccess
{
    public class Context : DbContext
    {
        public Context(DbContextOptions options) : base(options)
        {
        }

        public Context(){}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {            
            modelBuilder.ApplyConfiguration(new PaymentInstanceMap());
        }

        public DbSet<PaymentInstance> PaymentInstances { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (Environment.GetEnvironmentVariable("DB_CONN") != null)
            {
                optionsBuilder.UseNpgsql(Environment.GetEnvironmentVariable("DB_CONN"), npgsqlOptionsAction: options =>
                {
                    options.EnableRetryOnFailure(2, TimeSpan.FromSeconds(5), new List<string>());
                    options.MigrationsHistoryTable("_MigrationHistory", "sagasdemo");
                });
            }
            else
            {
                optionsBuilder.UseInMemoryDatabase("SagasDemoInMemory");
            }
        }
    }
}
