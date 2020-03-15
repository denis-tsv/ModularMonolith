﻿using Microsoft.EntityFrameworkCore;
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
        }
    }
}
