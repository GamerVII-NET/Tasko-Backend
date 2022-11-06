﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using Tasko.Server.Context.Data.Context;
using Tasko.Server.Context.Data.Models;

#nullable disable

namespace Tasko.Server.Context.Data.Context.Configurations
{
    public partial class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> entity)
        {
            entity.Property(e => e.GlobalGuid)
            .ValueGeneratedNever()
            .HasColumnOrder(0);
            entity.Property(e => e.About).HasColumnOrder(8);
            entity.Property(e => e.EmailGuid).HasColumnOrder(2);
            entity.Property(e => e.FirstName).HasColumnOrder(3);
            entity.Property(e => e.IsDeleted).HasColumnOrder(10);
            entity.Property(e => e.LastName).HasColumnOrder(5);
            entity.Property(e => e.LastOnline).HasColumnOrder(9);
            entity.Property(e => e.PasswordHash).HasColumnOrder(4);
            entity.Property(e => e.Patronymic).HasColumnOrder(6);
            entity.Property(e => e.PhoneGuid).HasColumnOrder(1);
            entity.Property(e => e.Photo).HasColumnOrder(7);

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<User> entity);
    }
}
