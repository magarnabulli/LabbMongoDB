using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;


namespace LabbMongoDBNeliz.UI
{
    internal interface IUI
    {
        public void Clear();
        public void Exit();
        public string GetInput();
        public void Print(string output);
        public void PrintBackToMenu();
        public void PrintMenu();
        public void ProductAdded(bool success);
        public void ProductUpdated(bool success);
        public void ProductsUpdated(bool success);
        public void ProductDeleted(bool success);
        
    }
}
