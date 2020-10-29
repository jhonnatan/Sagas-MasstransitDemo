using MassTransit;
using SagasDemo.Infrastructure.StateMachines;
using System.Threading.Tasks;

namespace SagasDemo.Infrastructure.Publishers
{
    public class PaymentPublisher
    {
        private readonly IBusControl bus;

        public PaymentPublisher(IBusControl bus)
        {
            this.bus = bus;
        }

        public async Task PublishAsync(PaymentInstance instance)
            => await bus.Publish(instance);
    }
}
