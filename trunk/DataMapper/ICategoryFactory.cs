using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMapper
{
    public interface ICategoryFactory
    {
        void AddCategory(Category category);
        Category GetCategoryByName(String name);
        Category GetCategoryById(int id);
        void UpdateCategory(int id, String newName);
        void UpdateCategoryDescription(int id, String description);
        void DeleteCategory(int id);

        ICollection<Category> getChildren(int idCategory);
        ICollection<Category> getParents(int idCategory);
        ICollection<Category> GetAllCategoryForAProduct(Product product);
    }
}
