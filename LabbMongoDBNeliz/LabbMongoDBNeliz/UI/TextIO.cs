using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabbMongoDBNeliz.UI
{
    internal class TextIO : IUI
    {
        public void Clear()
        {
            Console.Clear();
        }
        public void Exit()
        {
            System.Environment.Exit(0);
        }
        public string GetInput()
        {
            Console.Write("\n\t<\t");
            return Console.ReadLine();
        }
        public void Print(string output)
        {
            Console.WriteLine(output);
        }
        public void PrintBackToMenu()
        {
            Console.WriteLine("\tpress any key to return to the menu");
            Console.ReadLine();
        }
        public void PrintMenu()
        {
            Console.WriteLine("_____________C A C A D O O D O O_____________\n\t     | i n t e r i o r |" +
                    "\n\n\t[1] Add new product \n" +
                    "\n\t[2] Search for product\n" +
                    "\n\t[3] View products\n" +
                    "\n\t[4] View products by category\n" +
                    "\n\t[5] Update product\n" +
                    "\n\t[6] Update products by category\n" +
                    "\n\t[7] Remove product\n" +
                    "\n\t[8] Exit\n\n_____________________________________________\n");
        }
        public void ProductAdded(bool success)
        {
            if (success == true) { Console.WriteLine("\n\t|'''''''''''''''''''''''''''|\n\t|  Product has been added   |\n\t|___________________________|\n"); }
            if (success == false)
            {
                Console.ForegroundColor= ConsoleColor.Red;
                Console.WriteLine("\n\t|'''''''''''''''''''''''''''|\n\t|  PRODUCT ALREADY EXISTS   |\n\t|___________________________|\n");
                Console.ForegroundColor= ConsoleColor.White;
            }
        }
        public void ProductDeleted(bool success)
        {
            if (success == true) { Console.WriteLine("\n\t|'''''''''''''''''''''''''''|\n\t|  Product has been deleted |\n\t|___________________________|\n"); }
            if (success == false)
            {
                Console.ForegroundColor= ConsoleColor.Red;
                Console.WriteLine("\n\t|'''''''''''''''''''''''''''|\n\t|   PRODUCT NOT DELETED    |\n\t|___________________________|\n");
                Console.ForegroundColor= ConsoleColor.White;
            }
        }
        public void ProductUpdated(bool success)
        {
            if (success == true) { Console.Write("\n\t|'''''''''''''''''''''''''''|\n\t|  Product has been updated |\n\t|___________________________|\n"); }
            if (success == false)
            {
                Console.ForegroundColor= ConsoleColor.Red;
                Console.WriteLine("\n\t|'''''''''''''''''''''''''''|\n\t|   PRODUCT NOT UPDATED    |\n\t|___________________________|\n");
                Console.ForegroundColor= ConsoleColor.White;
            }
        }
        public void ProductsUpdated(bool success)
        {
            if (success == true) { Console.Write("\n\t|'''''''''''''''''''''''''''|\n\t| Products has been updated |\n\t|___________________________|\n"); }
            if (success == false)
            {
                Console.ForegroundColor= ConsoleColor.Red;
                Console.WriteLine("\n\t|'''''''''''''''''''''''''''|\n\t|   PRODUCTS NOT UPDATED   |\n\t|___________________________|\n");
                Console.ForegroundColor= ConsoleColor.White;
            }
        }
    }
}
