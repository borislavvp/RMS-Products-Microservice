﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
        private readonly IMapper _mapper;

        public ProductsController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            product = new ProductRepo(_context);
            _mapper = mapper;
        }


        [HttpGet]
        [Route("products/getAll")]
        public ActionResult<List<ProductModel>> GetAll()
        {
            List<ProductModel> tempList = new List<ProductModel>();
            var temp = product.GetAll();


            foreach (ProductEntity u in temp)
            {

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
