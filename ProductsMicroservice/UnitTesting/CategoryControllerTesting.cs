using AutoMapper;
using DatabaseLayer.BaseRepos;
using DatabaseLayer.DB;
using DatabaseLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private IMapper _mapper;

        [SetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            this._mapper = config.CreateMapper();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlServer(@"Server=DESKTOP-GJVD2M7;Database=proep-products;Trusted_Connection=True").Options;
            _context = new ApplicationDbContext(options);
            cc = new CategoryController(_context,_mapper);
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
            var okResult = result.Result as OkObjectResult;
            Assert.AreEqual(okResult.StatusCode, 200);
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
