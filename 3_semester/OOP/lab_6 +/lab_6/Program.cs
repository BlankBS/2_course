using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(" PIZZA ORDERING SYSTEM\n");

            try
            {
                ProcessCustomerOrder();
            }
            catch (InvalidOrderException ex)
            {
                Console.WriteLine($"\n ORDER ERROR: {ex.Message}");
                Console.WriteLine($"Order ID: {ex.OrderID}, Time: {ex.ErrorTime:T}");
            }
            catch (PaymentException ex)
            {
                Console.WriteLine($"\n PAYMENT ERROR: {ex.Message}");
                Console.WriteLine($"Amount: ${ex.Amount}, Method: {ex.PaymentMethod}");
                Console.WriteLine("Please try another payment method");
            }
            catch (KitchenException ex)
            {
                Console.WriteLine($"\n KITCHEN ERROR: {ex.Message}");
                Console.WriteLine($"Chef: {ex.CookName}");
                Console.WriteLine("Your order will be refunded");
            }
            catch (DeliveryException ex)
            {
                Console.WriteLine($"\n DELIVERY ERROR: {ex.Message}");
                Console.WriteLine($"Address: {ex.Adress}");
                Console.WriteLine("Please pick up your order from the restaurant");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nUNEXPECTED ERROR: {ex.Message}");
                Console.WriteLine($"Type: {ex.GetType().Name}");
            }
            finally
            {
                Console.WriteLine("\n=================================");
                Console.WriteLine("Thank you for choosing our pizza!");
                Console.WriteLine("=================================\n");
            }

            Console.WriteLine("RUNNING ALL ERROR TESTS");
            PizzaDemo.RunAllTests();
        }

        static void ProcessCustomerOrder()
        {
            try
            {
                PizzaDemo.TestProblematicOrder();
            }
            catch (InvalidOrderException ex)
            {
                Console.WriteLine($"First level handling: {ex.InvalidItem} is not allowed");
                throw;
            }
        }
    }
}
