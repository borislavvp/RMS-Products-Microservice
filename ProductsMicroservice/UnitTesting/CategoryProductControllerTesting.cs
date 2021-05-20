using DatabaseLayer.DB;
using DatabaseLayer.Entities;
using DatabaseLayer.RelationshipRepos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlServer(@"Server=DESKTOP-GJVD2M7;Database=proep-products;Trusted_Connection=True").Options;
            _context = new ApplicationDbContext(options);
            cpc = new CategoryProductController(_context);
            cpr = new CategoryProductRepo(_context);
        }

        [Test]
        public void AttachProductToCategory_Test()
        {
            Guid categoryId = new Guid("312d245c-cb9f-4dad-b7e3-848baecc43fd");
            Guid productId = new Guid("c72b31c4-5392-4056-a54f-2dcb4b290aac");
            var result = cpc.Attach(productId, categoryId);
            var okResult = result as OkObjectResult;
            Assert.AreEqual(okResult.StatusCode, 200);
        }

        [Test]
        public void DetachProductFromCategory_Test()
        {
            Guid categoryId = new Guid("73289780-af63-4222-9f5d-551cfed67f48");
            Guid productId = new Guid("c72b31c4-5392-4056-a54f-2dcb4b290aac");
            var result = cpc.Detach(categoryId, productId);
            var okResult = result as OkObjectResult;
            Assert.AreEqual(okResult.StatusCode, 200);
        }

        [Test]
        public void GetAllProjectsBy_Test()
        {
            Guid categoryId = new Guid("73289780-af63-4222-9f5d-551cfed67f48");
            var result = cpc.GetAllByParent(categoryId);
            var okResult = result.Result as OkObjectResult;
            Assert.AreEqual(okResult.StatusCode, 200);
        }
    }
}
