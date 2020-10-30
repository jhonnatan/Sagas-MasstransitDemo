using Automatonymous;
using GreenPipes;
using MassTransit;
using SagasDemo.Contracts;
using SagasDemo.Infrastructure.MassTransit.StateMachines;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SagasDemo.Infrastructure.MassTransit.Activities.PaymentCompleted
{
    public class PaymentCompletedActivity : Activity<PaymentInstance, IPaymentCompleted>
    {
        private readonly ConsumeContext _context;

        public PaymentCompletedActivity(ConsumeContext _context)
        {
            this._context = _context;
        }

        public void Accept(StateMachineVisitor visitor)
        {
            visitor.Visit(this);
        }

        public async Task Execute(BehaviorContext<PaymentInstance, IPaymentCompleted> context, Behavior<PaymentInstance, IPaymentCompleted> next)
        {
            // Do nothing
            await next.Execute(context).ConfigureAwait(false);
        }

        public Task Faulted<TException>(BehaviorExceptionContext<PaymentInstance, IPaymentCompleted, TException> context, Behavior<PaymentInstance, IPaymentCompleted> next) where TException : Exception
        {
            return next.Faulted(context);
        }

        public void Probe(ProbeContext context)
        {
            context.CreateScope("publish-payment-completed");
        }
    }
}
