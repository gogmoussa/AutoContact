using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoContact.Models
{
    public partial class Category
    {
        public Category()
        {
            Parts = new HashSet<Part>();
        }

        public long CategoryId { get; set; }
        public string CategoryName { get; set; }

        public virtual ICollection<Part> Parts { get; set; }
    }
}
