using SagasDemo.Contracts;
using System;

namespace SagasDemo.Domain
{
    public class Payment : ISubmitPayment
    {
        public Guid PaymentId { get; set; }
        public DateTime PaymentDate { get; set; }
        public double PaymentAmount { get; set; }
    }
}
