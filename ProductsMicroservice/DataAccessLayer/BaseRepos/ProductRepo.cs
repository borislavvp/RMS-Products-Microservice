using DatabaseLayer.DB;
using DatabaseLayer.Entities;
using DatabaseLayer.RelationshipRepos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatabaseLayer.BaseRepos
{
    public class ProductRepo : IBaseRepo<ProductEntity>
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<ProductEntity> _dbSet;
        private readonly CategoryProductRepo cp;

        public ProductRepo(ApplicationDbContext context)
        {
            this._context = context;
            this._dbSet = _context.Set<ProductEntity>();
            this.cp = new CategoryProductRepo(_context);
        }

        public int Delete(Guid id)
        {
            if (id != null)
            {
                var temp = _dbSet.First(p => p.Id == (Guid)id);
                _dbSet.Remove(temp);
                return _context.SaveChanges();
            }
            return 0;
        }

        public ICollection<ProductEntity> GetAll()
        {
            ICollection<ProductEntity> temp = _context.Products.Include(x => x.Category).ToList();
            return temp;
        }

        public ProductEntity GetById(Guid id)
        {
            if (id != null)
            {
                return _dbSet.Find(id);
            }
            return null;
        }

        public int Insert(ProductEntity entity)
        {
            if (entity != null)
            {
                if (_dbSet.FirstOrDefault(p =>p.Id == entity.Id) != null)
                {
                    return 0;
                }
                else
                {
                    _dbSet.Add(entity);
                    return _context.SaveChanges();
                }

            }
            return 0;
        }

        public int Update(
            Guid idOfEntityToUpdate,
            string name = "default",
            string description = "default",
            string ingredients = "default",
            double price = -1,
            string img = "default",
            int availability = 999,
            CategoryEntity cat = null
            )
        {
            if (idOfEntityToUpdate != null)
            {
                var temp = _dbSet.First(u => u.Id == idOfEntityToUpdate);
                if (temp != null)
                {
                    if (name != "default")
                    {
                        temp.Name = name;
                        _context.Entry(temp).Property(x => x.Name).IsModified = true;

                    }
                    if (description != "default")
                    {
                        temp.Description = description;
                        _context.Entry(temp).Property(x => x.Description).IsModified = true;

                    }
                    if (img != "default")
                    {
                        temp.Image = img;
                        _context.Entry(temp).Property(x => x.Image).IsModified = true;

                    }
                    if (ingredients != "default")
                    {
                        temp.Ingredients = ingredients;
                        _context.Entry(temp).Property(x => x.Ingredients).IsModified = true;

                    }
                    if (price != -1)
                    {
                        temp.Price = price;
                        _context.Entry(temp).Property(x => x.Price).IsModified = true;

                    }
                    if (availability != 999)
                    {
                        temp.Availability = availability;
                        _context.Entry(temp).Property(x => x.Availability).IsModified = true;

                    }
                    if(cat != null)
                    {
                        temp.Category = cat;
                        temp.CategoryId = cat.Id;
                        _context.SaveChanges();
                        this.cp.Attach(cat.Id, temp.Id);
                    }
                    return _context.SaveChanges();

                }
                return 0;

            }
            return 0;
        }
    }
}
