using Autofac;
using MassTransit;
using SagasDemo.Contracts;
using SagasDemo.Domain;
using SagasDemo.Infrastructure.Modules;
using System;
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

                //// Create a Payment to publish 
                //var payment = new Payment()
                //{
                //    PaymentId = NewId.NextGuid(),
                //    PaymentDate = DateTime.Now,
                //    PaymentAmount = 10
                //};
            
                //bus.Publish<IPaymentReceived>(payment);

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
            builder.RegisterModule<MasstransitModule>();
            builder.RegisterModule<DataAcessModule>();
            return builder.Build();

        }
    }
}
