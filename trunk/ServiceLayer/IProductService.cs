using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public interface IProductService
    {
        bool AddProduct(Product product);
        ICollection<Product> GetProductsByNameAndDescription(String name, String description);
        Product GetProductById(int id);
        bool UpdateProduct(int id, String newName);
        bool UpdateProductDescription(int id, String description);
        bool DeleteProduct(int id);
        ICollection<Product> GetAllProductsOfACategory(Category category);
        Auction GetAuctionOfAProduct(Product product);
    }
}
