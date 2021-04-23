using DatabaseLayer.BaseRepos;
using DatabaseLayer.DB;
using DatabaseLayer.Entities;
using NUnit.Framework;
using PresentationLayer.Controllers;
using PresentationLayer.Models;
using System;
using System.Collections.Generic;

namespace UnitTesting
{
    public class ProductsControllerTesting
    {
        private ProductsController pc;
        private ProductRepo pr;
        private ApplicationDbContext _context;
        [SetUp]
        public void Setup(ApplicationDbContext context)
        {
            _context = context;
            pc = new ProductsController(_context);
            pr = new ProductRepo(_context);
        }

        [Test]
        public void GetAllProducts_Test()
        {
            List<ProductModel> tempList = new List<ProductModel>();
            var temp = pr.GetAll();


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

            var result = pc.GetAll();
            CollectionAssert.AreEquivalent(result.Value,tempList);
        }

        [Test]
        public void GetProductById_Test()
        {
            Guid i = new Guid("b0a403b4-2347-414a-9b05-1ff0e5d0406f");
            var u = pr.GetById(i);
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
            var result = pc.GetById(i);
            Assert.AreEqual(result.Value.Id,temporary.Id);
        }


        [Test]
        public void InsertProduct_Test()
        {
            Guid i = new Guid("c72b31c4-5392-4056-a54f-2dcb4b290aac");
            ProductModel temporary = new ProductModel()
            {
                Id = i,
                Name = "testName",
                Description = "testDesc",
                Ingredients = "testIngredients",
                Image = "testImageAddr",
                Price = 2.2,
                Availability = 1

            };
            Assert.AreEqual(1, pc.Insert(temporary));
        }

        [Test]
        public void DeleteProduct_Test()
        {
            Guid i = new Guid("c72b31c4-5392-4056-a54f-2dcb4b290aac");
            Assert.AreEqual(1, pc.Delete(i));
        }

        [Test]
        public void UpdateProduct_Test()
        {
            Guid i = new Guid("c72b31c4-5392-4056-a54f-2dcb4b290aac");
            string name = "testNameChanged";
            Assert.AreEqual(1, pc.Update(i,name));
        }
    }
}