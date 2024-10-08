﻿using AutoMapper;
using LinkDev.Talabat.Core.Application.Abstraction.Products.Models;
using LinkDev.Talabat.Core.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Application.Mapping
{
	internal class MappingProfile : Profile
	{
        public MappingProfile()
        {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(d => d.Brand, O => O.MapFrom(s => s.Brand!.Name))
                .ForMember(d => d.Category, O => O.MapFrom(s => s.Category!.Name));
				

			CreateMap<ProductBrand, BrandDto>();
            CreateMap<ProductCategory, CategoryDto>();
		}  
    }
}
