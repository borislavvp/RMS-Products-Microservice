using AutoMapper;
using DatabaseLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PresentationLayer.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CategoryModel, CategoryEntity>();
            CreateMap<CategoryEntity, CategoryModel>();

            CreateMap<ProductModel, ProductEntity>();
            CreateMap<ProductEntity, ProductModel>();
        }
    }
}
