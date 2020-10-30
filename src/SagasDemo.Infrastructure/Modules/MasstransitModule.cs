using Autofac;
using System;
using MassTransit;
using MassTransit.EntityFrameworkCoreIntegration;
using SagasDemo.Infrastructure.StateMachines;
using SagasDemo.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using SagasDemo.Infrastructure.Consumers;
using SagasDemo.Infrastructure.Activities;

namespace SagasDemo.Infrastructure.Modules
{
    public class MasstransitModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var teste = Environment.GetEnvironmentVariable("DB_CONN");
            builder.AddMassTransit(cfg =>
            {
                cfg.SetKebabCaseEndpointNameFormatter();
                cfg.AddSagaStateMachine<PaymentStateMachine, PaymentInstance>(typeof(PaymentStateMachineDefinition))
                    .EntityFrameworkRepository(r =>
                    {
                        r.ConcurrencyMode = ConcurrencyMode.Optimistic;

                        r.AddDbContext<DbContext, Context>((provider, optionsBuilder) =>
                        {
                            optionsBuilder.UseNpgsql(Environment.GetEnvironmentVariable("DB_CONN"));
                        });
                    });
                cfg.AddConsumersFromNamespaceContaining<ConsumersAnchor>();
                cfg.AddActivitiesFromNamespaceContaining<ActivitiesAnchor>();

                cfg.UsingRabbitMq((x, y) =>
                {
                    y.Host("rabbitmq", "/", h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });
                    y.ConfigureEndpoints(x);
                });

                //cfg.AddBus(context => Bus.Factory.CreateUsingRabbitMq(c =>
                //{
                //    c.Host("rabbitmq", "/", u => {
                //        u.Password("guest");
                //        u.Username("guest");
                //    });                    
                //    c.ConfigureEndpoints(context);
                //}));


            });
            
            
        }
    }
}
