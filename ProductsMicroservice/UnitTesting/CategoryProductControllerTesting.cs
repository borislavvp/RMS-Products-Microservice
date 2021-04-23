using DatabaseLayer.DB;
using DatabaseLayer.Entities;
using DatabaseLayer.RelationshipRepos;
using NUnit.Framework;
using PresentationLayer.Controllers.RelationshipControllers;
using PresentationLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTesting
{
    class CategoryProductControllerTesting
    {
        private CategoryProductController cpc;
        private CategoryProductRepo cpr;
        private ApplicationDbContext _context;

        [SetUp]
        public void Setup(ApplicationDbContext context)
        {
            _context = context;
            cpc = new CategoryProductController(_context);
            cpr = new CategoryProductRepo(_context);
        }

        [Test]
        public void AttachProductToCategory_Test()
        {
            Guid categoryId = new Guid("5a41f438-9f5b-413f-b96b-875f3d9edd1e");
            Guid productId = new Guid("c72b31c4-5392-4056-a54f-2dcb4b290aac");
            Assert.AreEqual(1, cpc.Attach(productId,categoryId));
        }

        [Test]
        public void DetachProductFromCategory_Test()
        {
            Guid categoryId = new Guid("5a41f438-9f5b-413f-b96b-875f3d9edd1e");
            Guid productId = new Guid("c72b31c4-5392-4056-a54f-2dcb4b290aac");
            Assert.AreEqual(1, cpc.Detach(categoryId , productId ));
        }

        [Test]
        public void GetAllProjectsBy_Test()
        {
            Guid categoryId = new Guid("5a41f438-9f5b-413f-b96b-875f3d9edd1e");
            List<ProductModel> tempList = new List<ProductModel>();
            var temp = cpr.GetAllbyParent(categoryId);


            foreach (ProductEntity u in temp)
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
            var result = cpc.GetAllByParent(categoryId);
            CollectionAssert.AreEquivalent(result.Value, tempList);
        }
    }
}
