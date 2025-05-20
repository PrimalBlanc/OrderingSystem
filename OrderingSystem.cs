using System;
using System.Collections.Generic;

namespace AJennyCafe
{
    class MenuItem
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public MenuItem(string name, double price)
        {
            Name = name;
            Price = price;
        }
    }

    class OrderItem
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public OrderItem(string name, int quantity, double price)
        {
            Name = name;
            Quantity = quantity;
            Price = price;
        }
    }

    class Program
    {
        static void PrintMenu(List<MenuItem> breadMenu, List<MenuItem> coffeeMenu)
        {
            Console.WriteLine("AJenny Cafe");
            Console.WriteLine("Menu:");

            Console.WriteLine("\nBread:");
            for (int i = 0; i < breadMenu.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {breadMenu[i].Name} - ${breadMenu[i].Price:F2}");
            }

            Console.WriteLine("\nCoffee:");
            for (int i = 0; i < coffeeMenu.Count; i++)
            {
                Console.WriteLine($"{i + 1 + breadMenu.Count}. {coffeeMenu[i].Name} - ${coffeeMenu[i].Price:F2}");
            }
        }

        static void Main(string[] args)
        {
            var breadMenu = new List<MenuItem>
            {
                new MenuItem("Baguette", 2.50),
                new MenuItem("Croissant", 3.00),
                new MenuItem("Sourdough", 4.00),
                new MenuItem("Focaccia", 3.50),
                new MenuItem("Ciabatta", 3.75)
            };

            var coffeeMenu = new List<MenuItem>
            {
                new MenuItem("Espresso", 2.00),
                new MenuItem("Americano", 2.50),
                new MenuItem("Latte", 3.50),
                new MenuItem("Cappuccino", 3.75),
                new MenuItem("Mocha", 4.00)
            };

            var orders = new List<OrderItem>();
            string orderMore = "Y";

            while (orderMore.ToUpper() == "Y")
            {
                PrintMenu(breadMenu, coffeeMenu);
                Console.Write("\nPlease select an item by number: ");
                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    continue;
                }

                int totalItems = breadMenu.Count + coffeeMenu.Count;
                if (choice < 1 || choice > totalItems)
                {
                    Console.WriteLine("Invalid choice. Please try again.");
                    continue;
                }

                Console.Write("Enter quantity: ");
                if (!int.TryParse(Console.ReadLine(), out int quantity) || quantity < 1)
                {
                    Console.WriteLine("Invalid quantity. Please try again.");
                    continue;
                }

                string itemName;
                double itemPrice;
                if (choice <= breadMenu.Count)
                {
                    itemName = breadMenu[choice - 1].Name;
                    itemPrice = breadMenu[choice - 1].Price;
                }
                else
                {
                    int coffeeIndex = choice - breadMenu.Count - 1;
                    itemName = coffeeMenu[coffeeIndex].Name;
                    itemPrice = coffeeMenu[coffeeIndex].Price;
                }

                orders.Add(new OrderItem(itemName, quantity, itemPrice * quantity));

                Console.Write("Do you want to order again? (Y/N): ");
                orderMore = Console.ReadLine();
            }

            double totalPrice = 0;
            Console.WriteLine("\n--- Receipt ---");
            Console.WriteLine("AJenny Cafe");
            Console.WriteLine("{0,-15}{1,-10}{2,-10}", "Item", "Quantity", "Price");
            foreach (var order in orders)
            {
                Console.WriteLine("{0,-15}{1,-10}{2,-10:C2}", order.Name, order.Quantity, order.Price);
                totalPrice += order.Price;
            }
            Console.WriteLine("-----------------------------");
            Console.WriteLine("Total Price: {0:C2}", totalPrice);

            Console.Write("Enter amount of money given by customer: $");
            if (!double.TryParse(Console.ReadLine(), out double amountGiven))
            {
                Console.WriteLine("Invalid amount entered.");
                return;
            }

            double change = amountGiven - totalPrice;
            Console.WriteLine("Total Amount: {0:C2}", amountGiven);
            Console.WriteLine("Total Payment (Change): {0:C2}", change);

            if (change < 0)
            {
                Console.WriteLine("Warning: Insufficient amount given by customer.");
            }

            Console.WriteLine("Thank you for your order!");
        }
    }
}
