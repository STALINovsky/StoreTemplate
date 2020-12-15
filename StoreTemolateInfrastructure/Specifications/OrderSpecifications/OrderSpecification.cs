using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Specifications.Base;
using StoreTemplateCore.Entities;

namespace Infrastructure.Specifications.OrderSpecifications
{
    public class OrderSpecification : Specification<Order>
    {
        public OrderSpecification()
        {
        }

        public OrderSpecification(int id) : base(order => order.Id == id)
        {

        }

        public OrderSpecification IncludeOrderItemsWithProducts()
        {
            IncludeStrings.Add($"{nameof(Order.OrderItems)}.{nameof(OrderItem.Product)}");
            return this;
        }

        public OrderSpecification IncludeOrderDetails()
        {
            Includes.Add(order => order.OrderDetails);
            return this;
        }

        public OrderSpecification OrderByDate()
        {
            OrderByDescendingExpressions.Add(order => order.CreateTime);
            return this;
        }

        public OrderSpecification IncludeUser()
        {
            Includes.Add(order=> order.User);
            return this;
        }
    }
}
