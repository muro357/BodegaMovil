using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using AutoMapper;
using BodegaMovil.UseCases.Interfaces.Services;

namespace BodegaMovil.Services.Maps.AutoMapper
{
    public class AutoMapperConfig : IMapa
    {

        //MapperConfiguration _config;
        private IMapper _mapper;
        public AutoMapperConfig()
        {
            Configurar();
        }

        public Tout GetEntity<Tin, Tout>(Tin entity)
            where Tin : class
            where Tout : class
        {
            return _mapper.Map<Tout>(entity);
        }

        private void Configurar()
        {
             var config = new MapperConfiguration(cfg => {
                
                cfg.AddProfile<UsuarioProfile>();
                cfg.AddProfile<AccesoProfile>();
                cfg.AddProfile<PedidoProfile>();
                cfg.AddProfile<PedidoDetalleProfile>();
                cfg.AddProfile<ArticuloProfile>();
             });

            _mapper = config.CreateMapper();
        }
    }
}
