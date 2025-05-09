﻿using Domain.Aggregates.AccountAggregate;
using Domain.Aggregates.InstructorAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations.InstructorAggregateConfigurations;

public sealed class InstructorConfiguration : IEntityTypeConfiguration<Instructor>
{
    public void Configure(EntityTypeBuilder<Instructor> builder)
    {
        builder.ToTable("Instructor");
        builder.HasKey(i => i.Id);

        builder.HasOne<Account>()
            .WithOne()
            .HasForeignKey<Instructor>(i => i.Id)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
