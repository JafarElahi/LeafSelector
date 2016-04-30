using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeafSelector.Models
{
    public class LeafSelectorItemViewModel
    {
        public IList<LeafSelectorItemViewModel> Parents { get; set; }
        public int Id { get; set; }
        public string Text { get; set; }
        public bool IsLeaf { get; set; }
    }
}
