using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_6
{
    internal class PizzaDemo
    {
        public static void TestProblematicOrder()
        {
            try
            {
                var order = new PizzaOrder("Order-001", "John", "Sverdlova 13a");
                order.AddItem("Margherita", 12);
                order.AddItem("Hawaiian with pineapple", 15);
            }
            catch (InvalidOrderException exception)
            {
                Console.WriteLine($" Order error: {exception.Message}");
                throw;
            }
        }

        public static void TestPaymentIssues()
        {
            var order = new PizzaOrder("ORD-002", "Alice", "456 Oak St");
            order.AddItem("Pepperoni", 14);

            var payment = new PaymentSystem();
            payment.ProccessPayment(order, "99991234123412341", 14);
        }
        public static void TestDeliveryProblems()
        {
            var order = new PizzaOrder("ORD-003", "Bob", "");
            order.AddItem("Quattro Formaggi", 12);

            var delivery = new DeliveryService();
            delivery.DeliverOrder(order);
        }

        public static void TestKitchenDisaster()
        {
            var order = new PizzaOrder("ORD-004", "Charlie", "789 Pine St");

            var kitchen = new PizzaKitchen();
            kitchen.PrepareOrder(order);
        }

        public static void RunAllTests()
        { 
            Action[] tests = {
                () => {
                    var order = new PizzaOrder("TEST-1", "Test", "Address");
                    order.AddItem("Pizza with pineapple", 10);
                },
                () => {
                    var order = new PizzaOrder("TEST-2", "Test", "Address");
                    order.AddItem("Margherita", -5);
                },
                () => TestPaymentIssues(),
                () => TestDeliveryProblems(),
                () => TestKitchenDisaster(),
                () => {
                    var order = new PizzaOrder("TEST-3", "Test", "Address");
                    for (int i = 0; i < 10; i++)
                        order.AddItem($"Pizza {i}", 10);
                    new PizzaKitchen().PrepareOrder(order);
                }
            };

            string[] testNames = {
                "Pineapple pizza (invalid)",
                "Negative price",
                "Payment failure",
                "Delivery area issue",
                "Empty order",
                "Too many pizzas"
            };

            for (int i = 0; i < tests.Length; i++)
            {
                try
                {
                    Console.WriteLine($"\n Test {i + 1}: {testNames[i]}");
                    tests[i]();
                    Console.WriteLine(" SUCCESS");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($" {ex.GetType().Name}: {ex.Message}");
                }
            }
        }
    }
}
