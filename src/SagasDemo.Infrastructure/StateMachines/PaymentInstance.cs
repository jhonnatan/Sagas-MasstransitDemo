using Automatonymous;
using System;

namespace SagasDemo.Infrastructure.StateMachines
{
    public class PaymentInstance : SagaStateMachineInstance
    {
        public Guid PaymentId { get; set; }
        public DateTime PaymentDate { get; set; }
        public double PaymentAmount { get; set; }

        public string CurrentState { get; set; }
        public Guid CorrelationId { get; set; }
    }
}
