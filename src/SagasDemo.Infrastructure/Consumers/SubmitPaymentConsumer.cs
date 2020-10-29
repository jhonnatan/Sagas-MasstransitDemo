using MassTransit;
using SagasDemo.Contracts;
using System;
using System.Threading.Tasks;

namespace SagasDemo.Infrastructure.Consumers
{
    public class SubmitPaymentConsumer : IConsumer<ISubmitPayment>
    {
        public Task Consume(ConsumeContext<ISubmitPayment> context)
        {
            throw new NotImplementedException();
        }
    }
}
