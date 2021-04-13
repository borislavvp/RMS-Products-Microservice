using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PresentationLayer.Models
{
    public class CategoryModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ProductModel> Products { get; set; }
    }
}
