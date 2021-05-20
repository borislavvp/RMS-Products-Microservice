using DatabaseLayer.DB;
using DatabaseLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatabaseLayer.BaseRepos
{
    public class CategoryRepo : IBaseRepo<CategoryEntity>
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<CategoryEntity> _dbSet;

        public CategoryRepo(ApplicationDbContext context)
        {
            this._context = context;
            this._dbSet = _context.Set<CategoryEntity>();
        }


        public int Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public ICollection<CategoryEntity> GetAll()
        {
            ICollection<CategoryEntity> temp = _context.Categories.Include(u => u.Products).ToList();
            return temp;
        }

        public CategoryEntity GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public int Insert(CategoryEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
