using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabbMongoDBNeliz.DAO
{
    internal interface IProductDAO
    {
        bool Create(ProductModel product);
        List<ProductModel> ReadAll();
        List<ProductModel> ReadAllWithFilter(string category);
        ProductModel ReadOne(string name);
        ProductModel ReadOneWithFilter();
        bool Update(ProductModel oldProduct, string[] values);
        bool UpdateMany(string element, string value, string input);
        bool DeleteOne(ProductModel product);
        bool DeleteManyWithFilter(string element);

    }
}
