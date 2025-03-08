using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CloneBE.Application.DTO;
using CloneBE.Application.DTO.Request;
using CloneBE.Domain.EF;

namespace CloneBE.Application.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {
          CreateMap<Product,ProductRequest>().ReverseMap();
          CreateMap<Product,ProductDTO>().ReverseMap();
            CreateMap<CategoryDTO, Category>().ForMember(x => x.CategoryId, y => y.Ignore()).ReverseMap();
         CreateMap<Order,OrderDTO>().ReverseMap();
            CreateMap<OrderDetail, OrdetailSTO>().ReverseMap();
        
        }
    }
}
