using Automatonymous;
using GreenPipes;
using MassTransit;
using SagasDemo.Contracts;
using System;
using System.Threading.Tasks;

namespace SagasDemo.State.Activities.PaymentReceived
{
    public class PaymentReceivedActivity : Activity<PaymentStateInstance, IPaymentReceived>
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

        public async Task Execute(BehaviorContext<PaymentStateInstance, IPaymentReceived> context, Behavior<PaymentStateInstance, IPaymentReceived> next)
        {
            if (PaymentValidation(context.Data.PaymentAmount))            
                await _context.Publish<IPaymentCompleted>( new { context.Data.PaymentId, context.Data.PaymentDate, context.Data.PaymentAmount} );            
            else
                await _context.Publish<IPaymentFailed>(new { context.Data.PaymentId, context.Data.PaymentDate, context.Data.PaymentAmount });

            await next.Execute(context).ConfigureAwait(false);
        }

        public Task Faulted<TException>(BehaviorExceptionContext<PaymentStateInstance, IPaymentReceived, TException> context, Behavior<PaymentStateInstance, IPaymentReceived> next) where TException : Exception
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
