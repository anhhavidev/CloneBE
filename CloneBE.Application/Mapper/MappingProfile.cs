using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CloneBE.Application.DTO;
using CloneBE.Domain.EF;

namespace CloneBE.Application.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {
          CreateMap<Product,ProductDetail>().ReverseMap();
          CreateMap<Product,ProductDTO>().ReverseMap();
        
        
        }
    }
}
