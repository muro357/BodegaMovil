using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BodegaMovil.CoreBusiness;
using BodegaMovil.UseCases.DTO;
using AutoMapper;
using Microsoft.Maui.Controls;

namespace BodegaMovil.Services.Maps.AutoMapper
{
    public class PedidoProfile : Profile
    {
        public PedidoProfile()
        {
            CreateMap<Pedido, PedidoDTO>();
               /* .ForMember(dest => dest.Consecutivo, opt => opt.MapFrom(src => src.Consecutivo))
                .ForMember(dest => dest., opt => opt.MapFrom(src => src.Fecha))
                .ForMember(dest => dest.Descripcion, opt => opt.MapFrom(src => src.Descripcion))
                .ForPath(dest => dest.CompradorDTO.ID_Comprador, opt => opt.MapFrom(src => src.Comprador.ID_Comprador))
                .ForPath(dest => dest.CompradorDTO.Nombre, opt => opt.MapFrom(src => src.Comprador.Nombre))
            .ForPath(dest => dest.ListaDetalle, opt => opt.MapFrom(src => src.Lista));*/
        }

    }

    public class PedidoDetalleProfile : Profile
    {
        public PedidoDetalleProfile()
        {
            CreateMap<PedidoDetalle,PedidoDetalleDTO>();
        }
    }
}
