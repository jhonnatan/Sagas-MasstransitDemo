using Automatonymous;
using GreenPipes;
using MassTransit;
using SagasDemo.Contracts;
using SagasDemo.State;
using System;
using System.Threading.Tasks;

namespace SagasDemo.Activities.PaymentSubmitted
{
    public class PaymentSubmittedActivity : Activity<PaymentStateInstance, ISubmitPayment>
    {
        private readonly ConsumeContext context;

        public PaymentSubmittedActivity(ConsumeContext context)
        {
            this.context = context;
        }
        public void Accept(StateMachineVisitor visitor)
        {
            visitor.Visit(this);
        }

        public async Task Execute(BehaviorContext<PaymentStateInstance, ISubmitPayment> context, Behavior<PaymentStateInstance, ISubmitPayment> next)
        {
            // do the activity thing            
            //await context.Publish<IPaymentReceived>(new { PaymentId = context.Instance.CorrelationId, PaymentDate = context.Instance.PaymentDate, 
            //    PaymentAmount = context.Instance.PaymentAmount }).ConfigureAwait(false);            

            // call the next activity in the behavior
            await next.Execute(context).ConfigureAwait(false);
        }

        public Task Faulted<TException>(BehaviorExceptionContext<PaymentStateInstance, ISubmitPayment, TException> context, Behavior<PaymentStateInstance, ISubmitPayment> next) where TException : Exception
        {
            return next.Faulted(context);
        }

        public void Probe(ProbeContext context)
        {
            context.CreateScope("publish-payment-submitted");
        }
        
    }

}
