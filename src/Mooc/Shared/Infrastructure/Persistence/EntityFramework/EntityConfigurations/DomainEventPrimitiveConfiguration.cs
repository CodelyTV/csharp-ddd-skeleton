using System.Collections.Generic;
using CodelyTv.Shared.Domain;
using CodelyTv.Shared.Domain.Bus.Event;
using CodelyTv.Shared.Infrastructure.Persistence.EntityFramework.Extension;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;

namespace CodelyTv.Mooc.Shared.Infrastructure.Persistence.EntityFramework.EntityConfigurations
{
    public class DomainEventPrimitiveConfiguration : IEntityTypeConfiguration<DomainEventPrimitive>
    {
        public void Configure(EntityTypeBuilder<DomainEventPrimitive> builder)
        {
            builder.ToTable(nameof(MoocContext.DomainEvents).ToDatabaseFormat());

            builder.HasKey(x => x.AggregateId);

            builder.Property(x => x.Body)
                .HasConversion(v => JsonConvert.SerializeObject(v),
                    v => JsonConvert.DeserializeObject<Dictionary<string, string>>(v));

            builder.Property(x => x.AggregateId)
                .HasColumnName(nameof(DomainEventPrimitive.AggregateId).ToDatabaseFormat());

            builder.Property(x => x.OccurredOn)
                .HasConversion(v => Utils.StringToDate(v), v => Utils.DateToString(v))
                .HasColumnName(nameof(DomainEventPrimitive.OccurredOn).ToDatabaseFormat());
        }
    }
}
