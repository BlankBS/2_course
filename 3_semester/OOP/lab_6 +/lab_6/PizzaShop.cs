using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_6
{
    public class PizzaOrder
    {
        public string OrderId { get; }
        public List<string> Items { get; }
        public decimal TotalPrice { get; set; }
        public string CustomerName { get; }
        public string DeliveryAddress { get; }

        public PizzaOrder(string orderId, string customer, string adress)
        {
            OrderId = orderId;
            CustomerName = customer;
            DeliveryAddress = adress;
            Items = new List<string>();
        }

        public void AddItem(string item, decimal price)
        {
            Debug.Assert(price > 0, "Цена должна быть положительной");

            if (price <= 0)
                throw new InvalidOrderException(OrderId, item, "Price must be positive");

            if (item.ToLower().Contains("pineapple"))
                throw new InvalidOrderException(OrderId, item, "We don't serve pineapple on pizza!");

            Items.Add(item);
            TotalPrice += price;
        }
    }

    public class PizzaKitchen
    {
        public void PrepareOrder(PizzaOrder order)
        {
            if (order.Items.Count == 0)
                throw new KitchenException(order.OrderId, "Chef Artyom", "No items in order");

            if (order.Items.Count > 5)
                throw new KitchenException(order.OrderId, "Chef Artyom", "Too many items in order");

            Console.WriteLine($" Preparing order {order.OrderId}: {string.Join(", ", order.Items)}");
        }
    }

    public class PaymentSystem
    {
        public void ProccessPayment(PizzaOrder order, string cardNumber, decimal amount)
        {
            if (cardNumber == null || cardNumber.Length > 16)
                throw new PaymentException(order.OrderId, amount, "Card", "Invalid card number");

            if (amount != order.TotalPrice)
                throw new PaymentException(order.OrderId, amount, "Card", $"Amount {amount} doesn't match order total {order.TotalPrice}");

            Console.WriteLine($" Payment processed: ${amount}");
        }
    }

    public class DeliveryService
    {
        public void DeliverOrder(PizzaOrder order)
        {
            if (string.IsNullOrEmpty(order.DeliveryAddress))
                throw new DeliveryException(order.OrderId, "Unknown", "No delivery adress");

            if (order.TotalPrice < 15)
                throw new DeliveryException(order.OrderId, order.DeliveryAddress, "Minimum order for delivery is 15$");

            Console.WriteLine($"🚚 Delivering to: {order.DeliveryAddress}");
        }
    }
}
