﻿using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public interface IProductService
    {
        void AddProduct(Product product);
        ICollection<Product> GetProductsByNameAndDescription(String name, String description);
        Product GetProductById(int id);
        void UpdateProduct(int id, String newName);
        void UpdateProductDescription(int id, String description);
        void DeleteProduct(int id);
    }
}