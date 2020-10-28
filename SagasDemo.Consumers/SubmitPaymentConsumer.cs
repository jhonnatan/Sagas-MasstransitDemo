using MassTransit;
using SagasDemo.Contracts;
using System;
using System.Threading.Tasks;

namespace SagasDemo.Consumers
{
    public class SubmitPaymentConsumer : IConsumer<ISubmitPayment>
    {
        public Task Consume(ConsumeContext<ISubmitPayment> context)
        {
            throw new NotImplementedException();
        }
    }
}
