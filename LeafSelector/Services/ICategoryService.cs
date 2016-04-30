using LeafSelector.ServiceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeafSelector.Services
{
    public interface ICategoryService
    {
        IList<CategoryServiceModel> GetWithParents(int id);
        IList<CategoryServiceModel> SubCategories(int categoryId);
        CategoryServiceModel Get(int id);
        IList<CategoryServiceModel> Get();
    }
}
