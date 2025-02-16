using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BodegaMovil.CoreBusiness;
using BodegaMovil.UseCases;
using BodegaMovil.Plugins.DataStore.WebApi;
using BodegaMovil.UseCases.Interfaces;

namespace BodegaMovil.Tests.Repositorios.WebAPI
{
    public class TiendaTest
    {
        public TiendaTest()
        {

        }

        [Fact]
        public void DebeObtenerTiendas()
        {
            ITiendaRepository rep = new TiendaRepository();
            var uc = new GetTiendasHabilitadasUseCase(rep);
            var x = uc.ExecuteAsync();

            Assert.NotNull(x);
            Assert.True(x.Result.Count() >= 2);
        }

    }
}
