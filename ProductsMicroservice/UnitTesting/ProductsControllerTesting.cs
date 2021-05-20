using DatabaseLayer.BaseRepos;
using DatabaseLayer.DB;
using DatabaseLayer.Entities;
using NUnit.Framework;
using PresentationLayer.Controllers;
using PresentationLayer.Models;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace UnitTesting
{
    public class ProductsControllerTesting
    {
        private ProductsController pc;
        private ProductRepo pr;
        private ApplicationDbContext _context;
        private IMapper _mapper;
        [SetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            this._mapper = config.CreateMapper();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlServer(@"Server=DESKTOP-GJVD2M7;Database=proep-products;Trusted_Connection=True").Options;
            _context = new ApplicationDbContext(options);
            pc = new ProductsController(_context,_mapper);
            pr = new ProductRepo(_context);
        }

        [Test]
        public void GetAllProducts_Test()
        {
            var result = pc.GetAll();
            var okResult = result.Result as OkObjectResult;
            Assert.AreEqual(okResult.StatusCode, 200);
        }

        [Test]
        public void GetProductById_Test()
        {
            Guid i = new Guid("c72b31c4-5392-4056-a54f-2dcb4b290aac");
            var result = pc.GetById(i);
            var okResult = result.Result as OkObjectResult;
            Assert.AreEqual(okResult.StatusCode,200);

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
            var result = pc.Insert(temporary);
            var okResult = result as OkObjectResult;
            Assert.AreEqual(okResult.StatusCode, 200);
        }

        [Test]
        public void DeleteProduct_Test()
        {
            Guid i = new Guid("c72b31c4-5392-4056-a54f-2dcb4b290aac");
            var result = pc.Delete(i);
            var okResult = result as OkObjectResult;
            Assert.AreEqual(okResult.StatusCode, 200);
        }

        [Test]
        public void UpdateProduct_Test()
        {
            Guid i = new Guid("c72b31c4-5392-4056-a54f-2dcb4b290aac");
            string name = "testNameChanged";
            
            var result = pc.Update(i, name);
            var okResult = result as OkObjectResult;
            Assert.AreEqual(okResult.StatusCode, 200);
        }
    }
}