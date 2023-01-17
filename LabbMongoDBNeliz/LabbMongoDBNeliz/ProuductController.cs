using LabbMongoDBNeliz.DAO;
using LabbMongoDBNeliz.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace LabbMongoDBNeliz
{
    internal class ProuductController
    {
        IUI io;
        IProductDAO pDAO;
        public ProuductController(IUI io, IProductDAO pDAO)
        {
            this.io= io;
            this.pDAO= pDAO;
        }
        public void Start() //MENY FÖR HELA SKITEN
        {
            int i = 1;
            bool menu = true;
            while (menu)
            {
                io.Clear();
                io.PrintMenu();
                switch (ReadMenuChoice())
                {
                    case 1:
                        AddNewProduct(); //GOOD
                        break;
                    case 2:
                        SearchByName(); //Good but better with to.upper or sumt
                        break;
                    case 3:
                        ShowAllProducts(); //GOOD
                        break;
                    case 4:
                        ShowByCategory(); //GOOD
                        break;
                    case 5:
                        UpdateProduct(); //Works but ugly af 
                        break;
                    case 6:
                        UpdateProductsByCategory(); //very nice
                        break;
                    case 7:
                        DeleteProduct(); //works fine
                        break;
                    case 8:
                        io.Exit();
                        break;
                }
            }
        }
        public int ReadMenuChoice()
        {
            bool rightFormat = true;
            while (rightFormat)
            {
                try
                {
                    int choice = Int32.Parse(io.GetInput());
                    if (choice > 8 || choice < 1)
                    {

                        Console.ForegroundColor= ConsoleColor.Red;
                        Console.WriteLine("\t|'''''''''''''''''''''''''''|\n\t|     CHOOSE FROM MENU      |\n\t|___________________________|\n");
                        Console.ForegroundColor= ConsoleColor.White;
                    }
                    else
                    {
                        rightFormat= false;
                        return choice;
                    }
                }
                catch
                {
                    Console.ForegroundColor= ConsoleColor.Red;
                    Console.WriteLine("\t|'''''''''''''''''''''''''''|\n\t|        DIGITS ONLY        |\n\t|___________________________|\n");
                    Console.ForegroundColor= ConsoleColor.White;
                }
            }
            return 1;
        }
        public decimal RightFormatCheckPrice()
        {
            bool rightFormat = true;
            while (rightFormat)
            {
                io.Print("\n\tPrice: ");
                try
                {
                    decimal price = decimal.Parse(io.GetInput());
                    rightFormat = false;
                    return price;
                }
                catch
                {
                    io.Clear();
                    Console.ForegroundColor= ConsoleColor.Red;
                    Console.WriteLine("\n\t|'''''''''''''''''''''''''''|\n\t|   WRITE DECIMALS WITH [,] |\n\t|___________________________|\n");
                    Console.ForegroundColor= ConsoleColor.White;
                    rightFormat= true;
                }
            }
            return 1;
        }
        public int RightFormatCheckQty()
        {
            bool rightFormat = true;
            while (rightFormat)
            {
                io.Print("\n\tQuantity: ");
                try
                {
                    int price = Int32.Parse(io.GetInput());
                    rightFormat = false;
                    return price;
                }
                catch
                {
                    io.Clear();
                    Console.ForegroundColor= ConsoleColor.Red;
                    Console.WriteLine("\n\t|'''''''''''''''''''''''''''|\n\t|       DIGITS   ONLY       |\n\t|___________________________|\n");
                    Console.ForegroundColor= ConsoleColor.White;
                    rightFormat= true;
                }
            }
            return 1;
        }
        public void AddNewProduct()
        {
            io.Clear();
            ProductModel product = new ProductModel();
            io.Print("_____________N E W  P R O D U C T_____________\n\n");
            io.Print("\n\tName:");
            product.name = io.GetInput();
            io.Print("\n\tDescription: ");
            product.description = io.GetInput();
            product.price = RightFormatCheckPrice();
            product.quantity = RightFormatCheckQty();
            io.Print("\n\tCategory: ");
            product.category= ChooseOneFromListCategory();
            bool success = pDAO.Create(product);
            io.ProductAdded(success);
            io.PrintBackToMenu();
        } //case 1
        public void SearchByName()
        {
            io.Clear();
            io.Print("Name of product: ");
            string prdname = io.GetInput();
            ProductModel prd = pDAO.ReadOne(prdname);
            PrintProduct(prd);
            io.PrintBackToMenu();
        } //case2
        public void ShowAllProducts()
        {
            foreach (var prod in pDAO.ReadAll())
            {
                PrintProduct(prod);
            }
            io.PrintBackToMenu();
        }//case3
        public void ShowByCategory()
        {
            int i = 1;
            io.Print("\n\tWich category would you like to see?");
            List<ProductModel> catProducts = pDAO.ReadAllWithFilter(ChooseOneFromListCategory());
            foreach (var prod in catProducts)
            {
                PrintProduct(prod);
                i++;
            }
            io.PrintBackToMenu();
        }//case4
        public void UpdateProduct()
        {
            io.Print("n\tWich product would you like to update?");
            ProductModel prdToUpdate = ChooseOneFromList();
            var values = UpdateOptions();
            io.ProductUpdated(pDAO.Update(prdToUpdate, values));
            io.PrintBackToMenu();
        }//case5
        public void UpdateProductsByCategory() //case6
        {
            io.Clear();
            io.Print("\n\tChoose wich category you would like to update: ");
            string chosenValue = ChooseOneFromListCategory();
            io.Print($"\n\tWich category would you like to change the products in the category {chosenValue} to?");
            string chosenUpdatedValue = ChooseOneFromListCategory();
            io.ProductsUpdated(pDAO.UpdateMany("category", chosenValue, chosenUpdatedValue));
        }
        public void DeleteProduct()
        {
            io.Print("\n\tWich product would you like to delete?");
            ProductModel prdToDelete = ChooseOneFromList();
            io.ProductDeleted(pDAO.DeleteOne(prdToDelete));
            io.PrintBackToMenu();
        }//case7
        public void PrintProduct(ProductModel prod)
        {
            io.Print($"\n\tName: {prod.name}\n\t| Description: {prod.description}" +
            $"\n\t| Price: {prod.price.ToString()}\n\t| Quantity: {prod.quantity.ToString()}\n\t\t* * * *");
        }
        public string[] UpdateOptions()
        {
            string[] values = new string[5];

            io.Print("\n\tWhat do you want to update: \n\t[1] Name\n\t[2]Price\n\t[3] Description\n\t[4] Quantity\n\t[5]Category");
            switch (ReadMenuChoice())
            {
                case 1:
                    io.Print("\n\t New name: ");
                    values[0] = io.GetInput();
                    return values;
                case 2:
                    io.Print("\n\t New price: ");
                    values[1] = RightFormatCheckPrice().ToString();
                    return values;
                case 3:
                    io.Print("\n\t New description: ");
                    values[2] = io.GetInput();
                    return values;
                case 4:
                    io.Print("\n\t New quantity value: ");
                    values[3] = io.GetInput().ToString().ToString();
                    return values;
                case 5:
                    io.Print("\n\t New category: ");
                    values[4] = io.GetInput();
                    return values;
            }
            return values;
        }
        public string ChooseOneFromListCategory()
        {
            bool rightChoice = true;
            List<string> catNames = new List<string> { "Bath", "Lighting", "Deco", "Plants", "Dining", "Office", "Bedroom", "Cushions", "TEST" };
            while (rightChoice)
            {
                int i = 1;
                try
                {
                    foreach (var item in catNames)
                    {
                        io.Print($"\n\t{i}. {item}");
                        i++;
                    }
                    int v = int.Parse(io.GetInput());
                    string val = catNames[v-1].ToString();
                    return val;
                }
                catch
                {
                    Console.ForegroundColor= ConsoleColor.Red;
                    io.Print("\n\t|'''''''''''''''''''''''''''|\n\t|       DIGITS   ONLY       |\n\t|___________________________|\n");
                    Console.ForegroundColor= ConsoleColor.White;
                    Console.ReadLine();
                }
            }
            return "";
        }
        public ProductModel ChooseOneFromList()
        {
            bool rightChoice = true;
            int i = 1;
            List<ProductModel> prods = pDAO.ReadAll();
            ProductModel prd = new ProductModel();
            while (rightChoice)
            {
                i =1;
                foreach (var prod in prods)
                {
                    io.Print($"\t{i}.Name: {prod.name}\n\t| Description: {prod.description}" +
                    $"\n\t| Price: {prod.price.ToString()}\n\t| Quantity: {prod.quantity.ToString()}\n\t\t* * * *");
                    i++;
                }
                try
                {
                    int prodVal = int.Parse(io.GetInput());
                    if (prodVal < 1 || prodVal > prods.Count)
                    {
                        io.Print("\n\t|'''''''''''''''''''''''''''|\n\t|      Choose from list     |\n\t|___________________________|\n\t press ENTER to try again");
                        Console.ReadLine();
                    }
                    else
                    {
                        ProductModel chosenOne = prods[prodVal-1];
                        return chosenOne;

                    }
                }
                catch
                {
                    Console.ForegroundColor= ConsoleColor.Red;
                    io.Print("\n\t|'''''''''''''''''''''''''''|\n\t|       DIGITS   ONLY       |\n\t|___________________________|\n");
                    Console.ForegroundColor= ConsoleColor.White;
                    Console.ReadLine();
                }
            }
            return prd;
        }
    }
}
