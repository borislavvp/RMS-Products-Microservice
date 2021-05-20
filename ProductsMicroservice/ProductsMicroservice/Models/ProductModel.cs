using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PresentationLayer.Models
{
    public class ProductModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Ingredients { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public int Availability { get; set; }

        public string Image { get; set; }

        public Guid? CategoryId { get; set; }

        public CategoryModel Category { get; set; }
    }
}
