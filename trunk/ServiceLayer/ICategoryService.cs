using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public interface ICategoryService
    {
        bool AddCategory(Category category);
        Category GetCategoryByName(String name);
        Category GetCategoryById(int id);
        bool UpdateCategory(int id, String newName);
        bool UpdateCategoryDescription(int id, String description);
        bool DeleteCategory(int id);

        ICollection<Category> getChildren(int idCategory);
        ICollection<Category> getParents(int idCategory);
        ICollection<Category> GetCategorysForAProduct(Product product);
    }
}
