using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BodegaMovil.CoreBusiness;
using BodegaMovil.UseCases.DTO;
using AutoMapper;

namespace BodegaMovil.Services.Maps.AutoMapper
{
    public class AccesoProfile : Profile
    {
        public AccesoProfile()
        {
            CreateMap<AccesoDTO, Usuario>().ReverseMap();
        }
    }
}
