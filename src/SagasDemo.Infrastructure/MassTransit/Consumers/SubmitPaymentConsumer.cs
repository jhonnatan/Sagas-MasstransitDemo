using MassTransit;
using SagasDemo.Contracts;
using SagasDemo.Domain;
using System.Threading.Tasks;

namespace SagasDemo.Infrastructure.MassTransit.Consumers
{
    public class SubmitPaymentConsumer : IConsumer<ISubmitPayment>
    {
        public async Task Consume(ConsumeContext<ISubmitPayment> context)
        {
            var received = CreateReceivedEvent(context.Message);
            await context.Publish(received).ConfigureAwait(false);
        }

        public IPaymentReceived CreateReceivedEvent(ISubmitPayment message)
        {
            return new PaymentReceived(message.PaymentId, message.PaymentDate, message.PaymentAmount);
        }
    }
}
