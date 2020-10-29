﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SagasDemo.Infrastructure.StateMachines;

namespace SagasDemo.Infrastructure.DataAccess.Map
{
    public class PaymentInstanceMap : IEntityTypeConfiguration<PaymentInstance>
    {
        public void Configure(EntityTypeBuilder<PaymentInstance> builder)
        {
            builder.HasKey(x=>x.CorrelationId);

            builder.Property(x => x.CurrentState).IsRequired();
            builder.Property(x => x.PaymentId).IsRequired();
            builder.Property(x => x.PaymentDate).IsRequired();
            builder.Property(x => x.PaymentAmount).IsRequired();
        }
    }
}
