using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
namespace LabbMongoDBNeliz
{
    public class ProductModel
    {
        [BsonId]
        public Object? id { get; set; }
        public string? name { get; set; }
        public string? description { get; set; }
        public decimal? price { get; set; }
        public int? quantity { get; set; }
        public string? category { get; set; }
    }
}
