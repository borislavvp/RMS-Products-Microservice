using DatabaseLayer.DB;
using DatabaseLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatabaseLayer.RelationshipRepos
{
    public class CategoryProductRepo : IRelationshipRepo<ProductEntity>
    {


        private readonly ApplicationDbContext _context;
        private readonly DbSet<ProductEntity> _dbSetProduct;
        private readonly DbSet<CategoryEntity> _dbSetCategory;
        public CategoryProductRepo(ApplicationDbContext context)
        {
            this._context = context;
            this._dbSetProduct = _context.Set<ProductEntity>();
            this._dbSetCategory = _context.Set<CategoryEntity>();
        }

        public int Attach(Guid idOfParent, Guid idOfentityToBeAdded)
        {
            if (idOfentityToBeAdded != null && idOfParent != null)
            {
                var tempCategory = _dbSetCategory.Include(u => u.Products).First(u => u.Id == idOfParent);
                var tempProduct = _dbSetProduct.First(u => u.Id == idOfentityToBeAdded);

                tempCategory.Products.Add(tempProduct);
                tempProduct.Category = tempCategory;
                tempProduct.CategoryId = null;
                return _context.SaveChanges();
            }
            return 0;
        }

        public int Detach(Guid idOfParent, Guid idOfentityToBeRemoved)
        {
            if (idOfentityToBeRemoved != null && idOfParent != null)
            {
                var tempCategory = _dbSetCategory.Include(u => u.Products).First(u => u.Id == idOfParent);
                var tempProduct = _dbSetProduct.First(u => u.Id == idOfentityToBeRemoved);

                tempCategory.Products.Remove(tempProduct);
                tempProduct.Category = null;
                tempProduct.CategoryId = tempCategory.Id;
                return _context.SaveChanges();
            }
            return 0;
        }

        public ICollection<ProductEntity> GetAllbyParent(Guid idOfParent)
        {
            ICollection<ProductEntity> products = new List<ProductEntity>();
            var tempCategory = _dbSetCategory.Include(u => u.Products).First(u => u.Id == idOfParent);

            foreach (ProductEntity c in tempCategory.Products)
            {
                products.Add(c);
            }
            return products;
        }
    }
}
