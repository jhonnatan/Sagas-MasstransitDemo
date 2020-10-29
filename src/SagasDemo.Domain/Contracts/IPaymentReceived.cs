using System;

namespace SagasDemo.Contracts
{
    public interface IPaymentReceived
    {
        Guid PaymentId { get; }
        DateTime PaymentDate { get; }
        double PaymentAmount { get; }
    }
}
