using Automatonymous;
using SagasDemo.Contracts;
using SagasDemo.Infrastructure.MassTransit.Activities.PaymentCompleted;
using SagasDemo.Infrastructure.MassTransit.Activities.PaymentReceived;
using System;
using System.Threading.Tasks;

namespace SagasDemo.Infrastructure.MassTransit.StateMachines
{
    public class PaymentStateMachine : MassTransitStateMachine<PaymentInstance>
    {        
        public PaymentStateMachine()
        {
            InstanceState(x=>x.CurrentState);

            Event(() => EventPaymentReceived, x => { x.CorrelateById(m => m.Message.PaymentId); });
            Event(() => EventPaymentCompleted, x => { x.CorrelateById(m => m.Message.PaymentId); });
            Event(() => EventPaymentFailed, x => { x.CorrelateById(m => m.Message.PaymentId); });

            Initially(
                When(EventPaymentReceived)
                    .Then(Initialize)
                    .Activity(x => x.OfType<PaymentReceivedActivity>())
                    .TransitionTo(Received));

            During(Received,
                When(EventPaymentCompleted)
                    .Then(Register)
                    .Activity(x => x.OfType<PaymentCompletedActivity>())
                    .TransitionTo(Completed),
                When(EventPaymentFailed)
                    .Then(PaymentFailure)
                    .TransitionTo(Failure));            
           
        }        

        private void Initialize(BehaviorContext<PaymentInstance, IPaymentReceived> context)
        {
            InitializeInstance(context.Instance, context.Data);
        }

        private void InitializeInstance(PaymentInstance instance, IPaymentReceived data)
        {
            instance.PaymentId = data.PaymentId;
            instance.PaymentDate = data.PaymentDate;
            instance.PaymentAmount = data.PaymentAmount;
        }

        private void PaymentFailure(BehaviorContext<PaymentInstance, IPaymentFailed> context)
        {
            Console.WriteLine($"Payment Failed -> PaymentId: {context.Data.PaymentId}, PaymentDate: {context.Data.PaymentDate}, PaymentAmount: {context.Data.PaymentAmount}");
        }

        private void Register(BehaviorContext<PaymentInstance, IPaymentCompleted> context)
        {
            Console.WriteLine($"Registered -> PaymentId: {context.Data.PaymentId}, PaymentDate: {context.Data.PaymentDate}, PaymentAmount: {context.Data.PaymentAmount}");
        }

        public State Received { get; private set; }
        public State Completed { get; private set; }
        public State Failure { get; private set; }
        
        public Event<IPaymentReceived> EventPaymentReceived { get; private set; }
        public Event<IPaymentCompleted> EventPaymentCompleted { get; private set; }
        public Event<IPaymentFailed> EventPaymentFailed { get; private set; }

    }
}
