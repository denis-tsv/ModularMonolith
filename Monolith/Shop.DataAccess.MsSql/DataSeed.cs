using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Shop.Entities;

namespace Shop.DataAccess.MsSql
{
    public static class DataSeed
    {
        public static void Seed(this ModelBuilder builder)
        {
            builder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "Product 1",
                    Price = 10
                },
                new Product
                {
                    Id = 2,
                    Name = "Product 2",
                    Price = 100
                }
                );
            builder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Email = "test@test.test",
                    NormalizedEmail = "TEST@TEST.TEST",
                    EmailConfirmed = true,
                    LockoutEnabled = false,
                    AccessFailedCount = 0,
                    UserName = "test",
                    NormalizedUserName = "TEST",
                    TwoFactorEnabled = false,
                    PasswordHash = "123",
                    PhoneNumber = "123",
                    PhoneNumberConfirmed = true,
                    SecurityStamp = "sstamp",
                    ConcurrencyStamp = "cctamp"
                });
        }
    }
}
