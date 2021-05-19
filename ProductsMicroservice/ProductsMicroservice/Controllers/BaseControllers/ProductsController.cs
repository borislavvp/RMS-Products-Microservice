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

namespace PresentationLayer.Controllers
{
    [Route("api")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ProductRepo product;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
            product = new ProductRepo(_context);

        }


        [HttpGet]
        [Route("products/getAll")]
        public ActionResult<List<ProductModel>> GetAll()
        {
            List<ProductModel> tempList = new List<ProductModel>();
            var temp = product.GetAll();


            foreach (ProductEntity u in temp)
            {

                ProductModel temporary = new ProductModel()
                {
                    Id = u.Id,
                    Name = u.Name,
                    Description =  u.Description,
                    Ingredients =  u.Ingredients,
                    Image = (u.Image == null) ? null : u.Image,
                    Price = (u.Price == 0) ? 0 : u.Price,
                    Availability = u.Availability

                };
                tempList.Add(temporary);
            }

            return Ok(tempList);
        }

        [HttpGet]
        [Route("products/getbyid/{id}")]
        public ActionResult<ProductModel> GetById(Guid id)
        {
            var u = product.GetById(id);
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
            return Ok(temporary);
        }


        [HttpPost]
        [Route("products/insert")]
        public IActionResult Insert([Microsoft.AspNetCore.Mvc.FromForm] ProductModel u)
        {
            ProductEntity temporary = new ProductEntity()
            {
                Id = u.Id,
                Name = u.Name,
                Description = u.Description,
                Ingredients = u.Ingredients,
                Image = (u.Image == null) ? null : u.Image,
                Price = (u.Price == 0) ? 0 : u.Price,
                Availability = u.Availability

            };
            return Ok(product.Insert(temporary));

        }


        [HttpDelete]
        [Route("products/delete/{id}")]
        public IActionResult Delete(Guid id)
        {
            return Ok(product.Delete(id));
        }

        [HttpPut]
        [Route("products/update/{id}")]
        public IActionResult Update
        (
            Guid id,
            [FromForm] string name = "default",
            [FromForm] string description = "default",
            [FromForm] string ingredients = "default",
            [FromForm] double price = -1,
            [FromForm] string img = "default",
            [FromForm] int availability = -999
        )
        {
            return Ok(product.Update(id,name,description,ingredients,price,img,availability));
        }

    }
}
