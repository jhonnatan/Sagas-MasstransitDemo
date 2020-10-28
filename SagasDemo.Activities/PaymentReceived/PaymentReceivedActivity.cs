using Automatonymous;
using GreenPipes;
using SagasDemo.Contracts;
using SagasDemo.State;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SagasDemo.Activities.PaymentReceived
{
    public class PaymentReceivedActivity : Activity<PaymentStateInstance, IPaymentReceived>
    {
        public void Accept(StateMachineVisitor visitor)
        {
            throw new NotImplementedException();
        }

        public Task Execute(BehaviorContext<PaymentStateInstance, IPaymentReceived> context, Behavior<PaymentStateInstance, IPaymentReceived> next)
        {
            throw new NotImplementedException();
        }

        public Task Faulted<TException>(BehaviorExceptionContext<PaymentStateInstance, IPaymentReceived, TException> context, Behavior<PaymentStateInstance, IPaymentReceived> next) where TException : Exception
        {
            throw new NotImplementedException();
        }

        public void Probe(ProbeContext context)
        {
            throw new NotImplementedException();
        }
    }
}
