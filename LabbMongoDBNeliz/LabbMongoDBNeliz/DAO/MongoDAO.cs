using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;

namespace LabbMongoDBNeliz.DAO
{
    internal class MongoDAO : IProductDAO
    {
        MongoClient client;
        IMongoDatabase db;
        public MongoDAO(string db, string MongoURI)
        {
            this.client = new MongoClient(MongoURI);
            this.db = this.client.GetDatabase(db);
        }
        IMongoCollection<BsonDocument> GetCollection()
        {
            var collection = db.GetCollection<BsonDocument>("Products");
            return collection;
        }
        bool IProductDAO.Create(ProductModel product)
        {
            var collection = GetCollection();
            var checkNameFilter = Builders<BsonDocument>.Filter.Eq("name", product.name);
            var document = collection.Find(checkNameFilter).FirstOrDefault();
            if (document != null)
            {
                return false;
            }
            else
            {
                var insertProduct = new BsonDocument { { "name", product.name.ToString() },
                    { "description", product.description.ToString() },
                    { "price", product.price },
                    { "quantity", product.quantity },
                    { "category", product.category.ToString() }
                    };
                collection.InsertOne(insertProduct);
                return true;
            }
        }
        void IProductDAO.CreateMany(List<ProductModel> products)
        {

        }
        List<ProductModel> IProductDAO.ReadAll()
        {
            List<ProductModel> productList = new List<ProductModel>();
            var collection = GetCollection();
            var collectionDocs = collection.Find(new BsonDocument()).ToList();
            var collectionList = collectionDocs.Select(v => BsonSerializer.Deserialize<ProductModel>(v)).ToList();
            foreach (var product in collectionList)
            {
                productList.Add(product);
            }
            return productList;
        }
        List<ProductModel> IProductDAO.ReadAllWithFilter(string category)
        {
            List<ProductModel> categories = new List<ProductModel>();
            var collection = GetCollection();
            var collectionDocsFilter = Builders<BsonDocument>.Filter.Eq("category", category);
            var cursor = collection.Find(collectionDocsFilter).ToList();
            foreach (var item in cursor)
            {
                ProductModel prd = BsonSerializer.Deserialize<ProductModel>(item);
                categories.Add(prd);
            }
            return categories;
        }
        ProductModel IProductDAO.ReadOne(string name)
        {
            ProductModel tmp = new ProductModel();
            var collection = GetCollection();
            var collectionFilter = Builders<BsonDocument>.Filter.Eq("name", name);
            var prd = collection.Find(collectionFilter).FirstOrDefault();
            ProductModel prduct = BsonSerializer.Deserialize<ProductModel>(prd);
            return prduct;

        }
        ProductModel IProductDAO.ReadOneWithFilter()
        {
            throw new NotImplementedException();
        }
        bool IProductDAO.Update(ProductModel product, string[] values)
        {
            try
            {
                var collection = GetCollection();
                var filter = Builders<BsonDocument>.Filter.Eq("_id", product.id);

                if (values[0] != null)
                {
                    var updateName = Builders<BsonDocument>.Update.Set("name", values[0]);
                    collection.UpdateOne(filter, updateName);

                }
                if (values[1] != null)
                {
                    var updatePrice = Builders<BsonDocument>.Update.Set("price", decimal.Parse(values[1]));
                    collection.UpdateOne(filter, updatePrice);
                }
                if (values[2] != null)
                {
                    var updateDescription = Builders<BsonDocument>.Update.Set("description", values[2]);
                    collection.UpdateOne(filter, updateDescription);
                }
                if (values[3] != null)
                {
                    var updateQuantity = Builders<BsonDocument>.Update.Set("quantity", int.Parse(values[0]));
                    collection.UpdateOne(filter, updateQuantity);
                }
                if (values[4] != null)
                {
                    var updateCategory = Builders<BsonDocument>.Update.Set("category", values[4]);
                    collection.UpdateOne(filter, updateCategory);
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
        bool IProductDAO.UpdateMany(string element, string value, string update)
        {
            try
            {
                var collection = GetCollection();
                var filter = Builders<BsonDocument>.Filter.Eq(element, value);
                var updateFilter = Builders<BsonDocument>.Update.Set(element, update);
                collection.UpdateMany(filter, updateFilter);
                return true;
            }
            catch
            {
                return false;

            }
        }
        bool IProductDAO.DeleteOne(ProductModel product)
        {
            try
            {
                var collection = GetCollection();
                var deleteFilter = Builders<BsonDocument>.Filter.Eq("_id", product.id);
                collection.DeleteOne(deleteFilter);
                return true;
            }
            catch
            {
                return false;
            }
        }
        bool IProductDAO.DeleteManyWithFilter(string element)
        {
            try
            {
                var collection = GetCollection();
                var deleteFilter = Builders<BsonDocument>.Filter.ElemMatch<BsonValue>("category", element);
                collection.DeleteMany(deleteFilter);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
