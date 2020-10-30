using MassTransit;
using SagasDemo.Contracts;
using System.Threading.Tasks;

namespace SagasDemo.Infrastructure.MassTransit.Consumers
{
    public class PaymentCompletedConsumer// : IConsumer<IPaymentCompleted>
    {
        public Task Consume(ConsumeContext<IPaymentCompleted> context)
        {
            return context.ConsumeCompleted;
        }
    }
}
