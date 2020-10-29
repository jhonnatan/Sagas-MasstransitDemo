using Autofac;
using Microsoft.EntityFrameworkCore;
using SagasDemo.Infrastructure.DataAccess;
using System;

namespace SagasDemo.Infrastructure.Modules
{
    public class DataAcessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var connection = Environment.GetEnvironmentVariable("DB_CONN");

            builder.RegisterAssemblyTypes(typeof(Context).Assembly)                
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
            
            builder.RegisterType<Context>().InstancePerLifetimeScope();
            
            if (!string.IsNullOrEmpty(connection))
            {
                using (var context = new Context())
                {
                    context.Database.Migrate();                    
                }
            }
        }
    }
}
