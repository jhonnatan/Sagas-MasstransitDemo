using System;

namespace SagasDemo.Contracts
{
    public interface IPaymentCompleted
    {
        Guid PaymentId { get; }
        DateTime PaymentDate { get; }
        double PaymentAmount { get; }
    }
}
