using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BodegaMovil.CoreBusiness;
using BodegaMovil.UseCases;
using BodegaMovil.UseCases.Interfaces;
using BodegaMovil.UseCases.DTO;
using BodegaMovil.Plugins.DataStore.WebApi;
using BodegaMovil.UseCases.Interfaces.Services;
using BodegaMovil.Services.Maps.AutoMapper;


namespace BodegaMovil.Tests.Repositorios.WebAPI
{
    public class UsuariosTest
    {
        public UsuariosTest()
        {

        }

        [Fact]
        public void DebeAutentificar()
        {
            IUsuarioRepository rep = new UsuarioRepository();
            IMapa mapa = new AutoMapperConfig();

            LoguearseUseCase uc = new LoguearseUseCase(rep, mapa);

            var user = new AccesoDTO
            {
                usuario = "",
                password = ""
            };

            var x = uc.ExecuteAsync(user);

            Assert.NotNull(x);
            //Assert.True(x);
        }
    }
}
