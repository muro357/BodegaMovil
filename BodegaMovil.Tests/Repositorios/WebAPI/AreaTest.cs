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
    public class AreaTest
    {
        [Fact]
        public void DebeObtenerAreas()
        {
            IAreaRepository rep = new AreaRepository();
            var uc = new GetAreasHabilitadasUseCase(rep);
            var x = uc.ExecuteAsync();

            Assert.NotNull(x);
            Assert.True(x.Result.Count() >= 2);
        }
    }
}
