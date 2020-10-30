using Autofac;
using MassTransit;
using SagasDemo.Contracts;
using SagasDemo.Domain;
using SagasDemo.Infrastructure.Modules;
using System;
using System.Collections.Generic;
using System.Threading;

namespace SagasDemo.Service
{
    class Program
    {
        private static readonly AutoResetEvent autoResetEvent = new AutoResetEvent(false);
        static void Main(string[] args)
        {
            Console.WriteLine("Service started.");

            var container = RegisterContainers();
            
            var bus = container.Resolve<IBusControl>();

            try
            {
                bus.Start();

                // ---------------------------------------------
                // Send Messages to test

                //// Create a Payments to publish 
                var payments = new List<Payment>()
                {
                    new Payment() // OK
                    {
                        PaymentId = NewId.NextGuid(),
                        PaymentDate = DateTime.Now,
                        PaymentAmount = 199.99
                    },
                    new Payment() // Error
                    {
                        PaymentId = NewId.NextGuid(),
                        PaymentDate = DateTime.Now,
                        PaymentAmount = 0
                    }
                };
                
                foreach (var payment in payments)
                {
                    bus.Publish<ISubmitPayment>(payment);
                }                

                // ---------------------------------------------
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }

            AppDomain.CurrentDomain.ProcessExit += (o, e) =>
            {
                bus.Stop();
                Console.WriteLine("Terminating...");
                autoResetEvent.Set();
            };

            autoResetEvent.WaitOne();
        }

        private static IContainer RegisterContainers()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<DataAcessModule>();
            builder.RegisterModule<MasstransitModule>();
            return builder.Build();

        }
    }
}
