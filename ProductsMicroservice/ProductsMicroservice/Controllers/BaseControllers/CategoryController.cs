using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatabaseLayer.BaseRepos;
using DatabaseLayer.DB;
using DatabaseLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Models;

namespace PresentationLayer.Controllers.BaseControllers
{
    [Route("api")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly CategoryRepo category;

        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
            category = new CategoryRepo(_context);

        }


        [HttpGet]
        [Route("categories/getAll")]
        public ActionResult<List<CategoryModel>> GetAll()
        {
            List<CategoryModel> tempList = new List<CategoryModel>();
            var temp = category.GetAll();


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

            return Ok(tempList);
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
