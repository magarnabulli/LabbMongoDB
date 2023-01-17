using LabbMongoDBNeliz.UI;
using MongoDB.Driver;
using MongoDB.Bson;
using LabbMongoDBNeliz.DAO;
using LabbMongoDBNeliz;

internal class Program
{
    private static void Main(string[] args)
    {
        IUI io;
        IProductDAO productDao;
        io = new TextIO();
        productDao = new MongoDAO("Webshop", "mongodb+srv://nel:zimzim4833@cluster0.um5habx.mongodb.net/test");
        ProuductController pController = new ProuductController(io, productDao);
        pController.Start();
    }
}