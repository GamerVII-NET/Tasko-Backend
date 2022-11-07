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
            .HasColumnOrder(0)
            .HasDefaultValueSql("(newid())");
            entity.Property(e => e.About).HasColumnOrder(9);
            entity.Property(e => e.EmailGuid).HasColumnOrder(2);
            entity.Property(e => e.FirstName).HasColumnOrder(4);
            entity.Property(e => e.IsDeleted).HasColumnOrder(11);
            entity.Property(e => e.LastName).HasColumnOrder(6);
            entity.Property(e => e.LastOnline).HasColumnOrder(10);
            entity.Property(e => e.PasswordHash).HasColumnOrder(5);
            entity.Property(e => e.Patronymic).HasColumnOrder(7);
            entity.Property(e => e.PhoneGuid).HasColumnOrder(1);
            entity.Property(e => e.Photo).HasColumnOrder(8);
            entity.Property(e => e.Role).HasColumnOrder(3);

            entity.HasOne(d => d.RoleNavigation).WithMany(p => p.Users)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Users_Roles");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<User> entity);
    }
}
