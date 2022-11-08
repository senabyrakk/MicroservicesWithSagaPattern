﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Order.Api.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string BuyerId { get; set; }
        public ICollection<OrderItem> Items { get; set; } = new Collection<OrderItem>();
        public OrderStatus Status { get; set; }
        public string FailMessage{ get; set; }
        public Address Address{ get; set; }
    }

    public enum OrderStatus
    {
        Suspend,
        Success,
        Fail
    }
}
