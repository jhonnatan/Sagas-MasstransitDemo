using MassTransit;
using System.Threading.Tasks;

namespace SagasDemo.Infrastructure.MassTransit.Publishers
{
    public abstract class Publisher<T> where T : class
    {
        private readonly IBusControl bus;

        public Publisher(IBusControl bus)
        {
            this.bus = bus;
        }

        public async Task PublishAsync(T objectFile)
            => await bus.Publish(objectFile);
    }
}
