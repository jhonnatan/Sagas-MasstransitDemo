using System;

namespace SagasDemo.Contracts
{
    public interface ISubmitPayment
    {
        Guid PaymentId { get; }
        DateTime PaymentDate { get; }
        double PaymentAmount { get; }
    }
}
