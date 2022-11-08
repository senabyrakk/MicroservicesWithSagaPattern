using Microsoft.EntityFrameworkCore;
using Order.Api.Models;
using System;

namespace Order.Api.Data
{
    public class OrderDbContext : DbContext
    {
        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
        {

        }

        public DbSet<Order.Api.Models.Order> Order { get; set; }
        public DbSet<OrderItem> OrderItem { get; set; }

    }
}
