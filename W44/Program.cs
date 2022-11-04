using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Linq;
using CE = UtilityLib.ConsoleExtention;

Console.WriteLine("Week: 44");
Console.WriteLine("Weekly mini project - Level 4");
Console.WriteLine("Björn Savander");

Exercises exercises = new Exercises();
exercises.w44_MiniProjectLevel4();

// Excersice container
internal class Exercises
{
    public static ProductList productList = new ProductList();

    public Exercises()
    {
    }

    // Product
    public class Product
    {
        public string Category
        { get; set; }
        public string Name
        { get; set; }
        public int Price
        { get; set; }

        public Product(string category, string name, int price)
        {
            Category = category;
            Name = name;
            Price = price;
        }
    }

    // Product List
    public class ProductList
    {
        public List<Product> products = new List<Product>();

        public ProductList()
        {
        }

        // Add product to list of products
        public void Add(string category, string name, int price)
        {
            products.Add(new Product(category, name, price));
        }

        // Return a list ordered on "Price"
        public void GetOrderedList(ref List<Product> list)
        {
            list = products.OrderBy(o => o.Price).ToList();
        }

        // Check if "name"  exist in list of products
        public bool hasName(string name)
        {
            foreach (var p in products)
            {
                if (p.Name == name)
                {
                    return true;
                }
            }
            return false;
        }
    }

    // Menu
    public class Menu
    {
        // Display user menu
        public void DisplayMenu()
        {
            Console.WriteLine();
            CE.WriteLineColor("------------------------------", ConsoleColor.Yellow);
            CE.WriteLineColor("          Product Menu        ", ConsoleColor.Yellow);
            CE.WriteLineColor("------------------------------", ConsoleColor.Yellow);
            Console.WriteLine("'A'\tAdd Product");
            Console.WriteLine("'D'\tDisplay Products");
            Console.WriteLine("'S'\tSearch Product");
            Console.WriteLine("'Q'\tQuit");
        }

        // Asks for products to add to list of products
        // 'q' = Quit
        public void ProductsAdd()
        {
            while (true)
            {
                Console.WriteLine();
                CE.WriteLineColor("------------------------------", ConsoleColor.Yellow);
                CE.WriteLineColor("Enter a new product. 'Q' = Quit", ConsoleColor.Yellow);

                string productCategory = CE.StringReadNotEmpty("Enter Category: ").Trim();
                if (productCategory.ToLower() == "q")
                {
                    return;
                }

                string productName = CE.StringReadNotEmpty("Enter Product Name: ").Trim();
                int productPriceinsertString = CE.NumberReadPositive("Enter Price: ");

                productList.Add(productCategory, productName, productPriceinsertString);
                CE.WriteLineColor("The product was successfully added!", ConsoleColor.Green);
            }
        }

        // Display list of products on screeen
        // "highlightName" will be != null if there is a "name" to highlight
        public void ProductsDisplay(string? highlightName = null)
        {
            Console.WriteLine();
            Console.WriteLine("------------------------------");
            Console.WriteLine("         Product List         ");
            Console.WriteLine("------------------------------");
            CE.WriteLineColor($"{"Category",-10}{"Name",-10}{"Price",-10}", ConsoleColor.Green);
            Console.WriteLine("------------------------------");

            List<Product> products = new List<Product>();
            productList.GetOrderedList(ref products);

            foreach (var p in products.OrderBy(o => o.Price))
            {
                string str = $"{p.Category,-10}{p.Name,-10}{p.Price,-10}";
                if (highlightName != p.Name)
                {
                    Console.WriteLine(str);
                }
                else
                {
                    // Highlight "name"
                    CE.WriteLineColor(str + "<-", ConsoleColor.Cyan);
                }
            }

            // Calc total sum of "prices"
            int priceSum = products.Sum(o => o.Price);
            Console.WriteLine();
            Console.WriteLine($"{"Sum:",-20}{priceSum,-10}");
            Console.WriteLine("------------------------------");
        }
        
        // Search for product based
        // Search parameter = Name
        public void ProductsSearch()
        {
            Console.WriteLine();
            Console.WriteLine("------------------------------");
            Console.WriteLine("         Product Search       ");
            Console.WriteLine("------------------------------");
            Console.WriteLine("Search for a product. 'Q' = Exit");

            string productName = CE.StringReadNotEmpty("Enter Product Name: ").Trim();

            // Check if "name" list of products
            if (productList.hasName(productName))
            {
                // Found, display list with "name" highlighted
                ProductsDisplay(productName);
            }
            else
            {
                // NOT  found!
                CE.WriteLineColor($"{productName} is Missing!", ConsoleColor.Red);
            }
        }
    }

    // Mini project menu runner
    public void w44_MiniProjectLevel4()
    {
        Menu menu = new Menu();

        bool running = true;
        do
        {
            menu.DisplayMenu();

            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.A:
                    menu.ProductsAdd();
                    break;
                case ConsoleKey.D:
                    menu.ProductsDisplay();
                    break;
                case ConsoleKey.S:
                    menu.ProductsSearch();
                    break;
                case ConsoleKey.Escape:
                case ConsoleKey.Q:
                    running = false;
                    break;
                default:
                    break;
            }
        }
        while (running);

        Console.WriteLine();
        Console.WriteLine("Done! Press any key to Exit!");
        Console.ReadKey(true);
    }
}
