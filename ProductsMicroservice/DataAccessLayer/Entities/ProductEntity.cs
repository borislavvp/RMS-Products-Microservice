using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DatabaseLayer.Entities
{
    [Table("Product")]
    public class ProductEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Ingredients are required")]
        public string Ingredients { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        public double Price { get; set; }

        public int Availability { get; set; }

        public string Image { get; set; }

        public Guid? CategoryId { get; set; }

        public CategoryEntity Category { get; set; }
    }
}
