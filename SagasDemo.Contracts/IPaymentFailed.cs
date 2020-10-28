using MassTransit;
using System;

namespace SagasDemo.Contracts
{
    public interface IPaymentFailed
    {
        Guid PaymentId { get; }
        DateTime PaymentDate { get; }
        double PaymentAmount { get; }

        ExceptionInfo ExceptionInfo { get; }
    }
}
