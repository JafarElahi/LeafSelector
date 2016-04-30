using LeafSelector.ServiceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeafSelector.Services
{
    public class CategoryService : ICategoryService
    {
        public IList<CategoryServiceModel> Get()
        {
            var categories = new List<CategoryServiceModel>()
                    {
                        new CategoryServiceModel() { Id = 0, Text = "Base", IsLeaf = false , ParentId = -1 },
                        new CategoryServiceModel() { Id = 1, Text = "Sample 1", IsLeaf = false , ParentId = 0 },
                        new CategoryServiceModel() { Id = 2, Text = "Sample 2", IsLeaf = false , ParentId = 0 },
                        new CategoryServiceModel() { Id = 3, Text = "Sample 3", IsLeaf = false , ParentId = 0 },
                        new CategoryServiceModel() { Id = 11, Text = "Sample 11", IsLeaf = false , ParentId = 1 },
                        new CategoryServiceModel() { Id = 12, Text = "Sample 12", IsLeaf = true , ParentId = 1 },
                        new CategoryServiceModel() { Id = 13, Text = "Sample 13", IsLeaf = true , ParentId = 1 },
                        new CategoryServiceModel() { Id = 14, Text = "Sample 14", IsLeaf = true , ParentId = 1 },
                        new CategoryServiceModel() { Id = 21, Text = "Sample 21", IsLeaf = true , ParentId = 2 },
                        new CategoryServiceModel() { Id = 22, Text = "Sample 22", IsLeaf = true , ParentId = 2 },
                        new CategoryServiceModel() { Id = 23, Text = "Sample 23", IsLeaf = true , ParentId = 2 },
                        new CategoryServiceModel() { Id = 111, Text = "Sample 111", IsLeaf = true , ParentId = 11 },
                        new CategoryServiceModel() { Id = 112, Text = "Sample 112", IsLeaf = true , ParentId = 11 },
                        new CategoryServiceModel() { Id = 113, Text = "Sample 113", IsLeaf = true , ParentId = 11 },
                        new CategoryServiceModel() { Id = 114, Text = "Sample 114", IsLeaf = true , ParentId = 11 },
                        new CategoryServiceModel() { Id = 115, Text = "Sample 115", IsLeaf = true , ParentId = 11 },
                    };
            foreach (var category in categories)
            {
                if (categories.Any(x => x.Id == category.ParentId))
                {
                    category.Parents = new List<CategoryServiceModel>() { categories.Where(x => x.Id == category.ParentId).FirstOrDefault() };
                }
            }
            return categories;
        }

        public CategoryServiceModel Get(int id)
        {
            return Get().FirstOrDefault(x => x.Id == id);
        }

        public IList<CategoryServiceModel> SubCategories(int categoryId)
        {
            return Get().Where(x => x.ParentId == categoryId).ToList();
        }

        public IList<CategoryServiceModel> GetWithParents(int id)
        {
            var list = new List<CategoryServiceModel>();
            var category = Get(id);
            while (category != null)
            {
                list.Add(category);
                category = Get(category.ParentId);
            }
            list.Reverse();
            return list;
        }

    }
}
