using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_6
{
    public class PizzaException : Exception
    {
        public string OrderID { get; }
        public DateTime ErrorTime { get; }

        public PizzaException(string orderID, string message) : base(message)
        {
            OrderID = orderID;
            ErrorTime = DateTime.Now;
        }
    }

    public class InvalidOrderException : PizzaException
    {
        public string InvalidItem { get; }

        public InvalidOrderException(string orderId, string item, string problem)
           : base(orderId, $"Invalid item '{item}': {problem}")
        {
            InvalidItem = item;
        }
    }

    public class PaymentException : PizzaException
    {
        public decimal Amount { get; }
        public string PaymentMethod { get; }

        public PaymentException(string orderId, decimal amount, string method, string problem)
            : base(orderId, $"Payment failed: {problem}")
        {
            Amount = amount;
            PaymentMethod = method;
        }
    }

    public class KitchenException : PizzaException
    {
        public string CookName { get; }
        public KitchenException(string orderId, string cook, string problem)
            : base(orderId, $"Kitchen error ({cook}): {problem}")
        {
            CookName = cook;
        }
    }

    public class DeliveryException : PizzaException
    {
        public string Adress { get; }

        public DeliveryException(string orderId, string adress, string problem)
        : base(orderId, $"Delivery to {adress} failed: {problem}")
        {
            Adress = adress;
        }
    }
}
