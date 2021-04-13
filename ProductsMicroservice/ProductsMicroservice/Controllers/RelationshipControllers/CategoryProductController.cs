using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatabaseLayer.DB;
using DatabaseLayer.Entities;
using DatabaseLayer.RelationshipRepos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Models;

namespace PresentationLayer.Controllers.RelationshipControllers
{
    [Route("api")]
    [ApiController]
    public class CategoryProductController : ControllerBase
    {
        CategoryProductRepo catProds;
        

        public CategoryProductController(ApplicationDbContext context)
        {
            this.catProds = new CategoryProductRepo(context);
        }

        [HttpPost]
        [Route("categoryProduct/attach/{childId}/{parentId}")]
        public IActionResult Attach( Guid childId, Guid parentId)
        {
            return Ok(catProds.Attach(parentId, childId));
        }


        [HttpDelete]
        [Route("categoryProduct/detach/{parentId}/{childId}")]
        public IActionResult Detach(Guid parentId, Guid childId)
        {
            return Ok(catProds.Detach(parentId, childId));
        }


        [HttpGet]
        [Route("categoryProduct/getByParent/{id}")]
        public ActionResult<List<ProductModel>> GetAllByParent(Guid id)
        {
            List<ProductModel> tempList = new List<ProductModel>();
            var temp = catProds.GetAllbyParent(id);
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

            return Ok(tempList);
        }
    }
}
