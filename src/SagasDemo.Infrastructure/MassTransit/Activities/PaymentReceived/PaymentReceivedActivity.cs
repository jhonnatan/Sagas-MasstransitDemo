using Automatonymous;
using GreenPipes;
using MassTransit;
using SagasDemo.Contracts;
using SagasDemo.Infrastructure.MassTransit.StateMachines;
using System;
using System.Threading.Tasks;

namespace SagasDemo.Infrastructure.MassTransit.Activities.PaymentReceived
{
    public class PaymentReceivedActivity : Activity<PaymentInstance, IPaymentReceived>
    {
        private readonly ConsumeContext _context;

        public PaymentReceivedActivity(ConsumeContext _context)
        {
            this._context = _context;
        }

        public void Accept(StateMachineVisitor visitor)
        {
            visitor.Visit(this);
        }

        public async Task Execute(BehaviorContext<PaymentInstance, IPaymentReceived> context, Behavior<PaymentInstance, IPaymentReceived> next)
        {
            if (PaymentValidation(context.Data.PaymentAmount))            
                await _context.Publish<IPaymentCompleted>( new { context.Data.PaymentId, context.Data.PaymentDate, context.Data.PaymentAmount} );            
            else
                await _context.Publish<IPaymentFailed>(new { context.Data.PaymentId, context.Data.PaymentDate, context.Data.PaymentAmount });

            await next.Execute(context).ConfigureAwait(false);
        }

        public Task Faulted<TException>(BehaviorExceptionContext<PaymentInstance, IPaymentReceived, TException> context, Behavior<PaymentInstance, IPaymentReceived> next) where TException : Exception
        {
            return next.Faulted(context);
        }

        public void Probe(ProbeContext context)
        {
            context.CreateScope("publish-payment-received");

        }

        private bool PaymentValidation(double amount)
        {
            return amount > 0;
        }
    }
}
