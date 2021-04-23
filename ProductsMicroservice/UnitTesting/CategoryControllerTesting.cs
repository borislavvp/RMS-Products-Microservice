using DatabaseLayer.BaseRepos;
using DatabaseLayer.DB;
using DatabaseLayer.Entities;
using NUnit.Framework;
using PresentationLayer.Controllers.BaseControllers;
using PresentationLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnitTesting
{
    class CategoryControllerTesting
    {
        private CategoryController cc;
        private CategoryRepo cr;
        private ApplicationDbContext _context;

        [SetUp]
        public void Setup(ApplicationDbContext context)
        {
            _context = context;
            cc = new CategoryController(_context);
            cr = new CategoryRepo(_context);
        }

        [Test]
        public void GetAllCategories_Test()
        {
            List<CategoryModel> tempList = new List<CategoryModel>();
            var temp = cr.GetAll();


            foreach (CategoryEntity u in temp)
            {

                CategoryModel temporary = new CategoryModel()
                {
                    Id = u.Id,
                    Name = u.Name,
                    Products = ConvertProjects(u.Products.ToList())

                };
                tempList.Add(temporary);
            }

            var result = cc.GetAll();
            CollectionAssert.AreEquivalent(result.Value, tempList);
        }

        public List<ProductModel> ConvertProjects(List<ProductEntity> prs)
        {
            List<ProductModel> tempList = new List<ProductModel>();
            foreach (ProductEntity u in prs)
            {

                ProductModel temporary = new ProductModel()
                {
                    Id = u.Id,
                    Name = u.Name,
                    Description = u.Description,
                    Ingredients = u.Ingredients,
                    Image = (u.Image == null) ? null : u.Image,
                    Price = (u.Price == 0) ? 0 : u.Price,
                    Availability = u.Availability

                };
                tempList.Add(temporary);
            }
            return tempList;
        }

    }
}
