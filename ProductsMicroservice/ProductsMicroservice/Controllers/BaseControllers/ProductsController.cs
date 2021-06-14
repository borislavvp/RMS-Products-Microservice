using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DatabaseLayer;
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
        private IBlobService _blobService;
        private readonly ProductRepo product;
        private readonly IMapper _mapper;

        public ProductsController(ApplicationDbContext context, IBlobService blobService, IMapper mapper)
        {
            _context = context;
            product = new ProductRepo(_context);
            _mapper = mapper;
            this._blobService = blobService;
        }


        [HttpGet]
        [Route("products/getAll")]
        public ActionResult<List<ProductModel>> GetAll()
        {
            List<ProductModel> tempList = new List<ProductModel>();
            var temp = product.GetAll();
            

            foreach (ProductEntity u in temp)
            {
                u.Image = this._blobService.GetBlobAsync(u.Image);
                var temporary = _mapper.Map<ProductModel>(u);
                tempList.Add(temporary);
            }

            return Ok(tempList);
        }

        [HttpGet]
        [Route("products/getbyid/{id}")]
        public ActionResult<ProductModel> GetById(Guid id)
        {
            var u = product.GetById(id);
            var temporary = _mapper.Map<ProductModel>(u);
            return Ok(temporary);
        }


        [HttpPost]
        [Route("products/insert")]
        public IActionResult Insert([Microsoft.AspNetCore.Mvc.FromForm] ProductModel u)
        {
            ProductEntity temporary =_mapper.Map<ProductEntity>(u);
            return Ok(product.Insert(temporary));
        }


        [HttpDelete]
        [Route("products/delete/{id}")]
        public IActionResult Delete(Guid id)
        {
            return Ok(product.Delete(id));
        }

        [HttpPost]
        [Route("products/update")]
        public IActionResult Update([FromForm] ProductModel p)
        {
            return Ok(product.Update(p.Id,p.Name,p.Description,p.Ingredients,p.Price,p.Image,p.Availability));
        }

    }
}
