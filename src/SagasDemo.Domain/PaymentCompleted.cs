using SagasDemo.Contracts;
using System;

namespace SagasDemo.Domain
{
    public class PaymentCompleted : IPaymentCompleted
    {
        public PaymentCompleted(Guid paymentId, DateTime paymentDate, double paymentAmount)
        {
            PaymentId = paymentId;
            PaymentDate = paymentDate;
            PaymentAmount = paymentAmount;
        }

        public Guid PaymentId { get; set; }
        public DateTime PaymentDate { get; set; }
        public double PaymentAmount { get; set; }
    }
}
