using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BodegaMovil.CoreBusiness;
using BodegaMovil.UseCases.DTO;

namespace BodegaMovil.Services.Maps.AutoMapper
{
    public class ArticuloProfile : Profile
    {
        public ArticuloProfile()
        {
            CreateMap<Articulo, ArticuloDTO>().ReverseMap();
        }
    }
}
