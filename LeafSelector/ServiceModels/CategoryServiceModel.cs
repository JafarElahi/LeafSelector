using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeafSelector.ServiceModels
{
    public class CategoryServiceModel
    {
        public IList<CategoryServiceModel> Parents { get; set; }
        public int Id { get; set; }
        public string Text { get; set; }
        public bool IsLeaf { get; set; }
        public int ParentId { get; set; }
    }
}
