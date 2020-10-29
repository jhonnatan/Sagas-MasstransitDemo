using MassTransit;
using MassTransit.Definition;

namespace SagasDemo.Infrastructure.StateMachines
{
    public class PaymentStateMachineDefinition : SagaDefinition<PaymentInstance>
    {
        public PaymentStateMachineDefinition()
        {
            //ConcurrentMessageLimit = 10;
        }

        protected override void ConfigureSaga(IReceiveEndpointConfigurator endpointConfigurator, ISagaConfigurator<PaymentInstance> sagaConfigurator)
        {
            //sagaConfigurator.UseMessageRetry(r => r.Immediate(5));
            //sagaConfigurator.UseInMemoryOutbox();

            //var partition = endpointConfigurator.CreatePartitioner(10);

            //sagaConfigurator.Message<IPaymentReceived>(x => x.UsePartitioner(partition, m => m.Message.PaymentId));            
        }

    }
}
