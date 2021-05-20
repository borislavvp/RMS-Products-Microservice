using System;
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

namespace PresentationLayer.Controllers.BaseControllers
{
    [Route("api")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly CategoryRepo category;
        private readonly IMapper _mapper;

        public CategoryController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            category = new CategoryRepo(_context);
            _mapper = mapper;
        }


        [HttpGet]
        [Route("categories/getAll")]
        public ActionResult<List<CategoryModel>> GetAll()
        {
            List<CategoryModel> tempList = new List<CategoryModel>();
            var temp = category.GetAll();


            foreach (CategoryEntity u in temp)
            {

                var temporary = _mapper.Map<CategoryModel>(u);
                tempList.Add(temporary);
            }

            return Ok(tempList);
        }

    }
}
