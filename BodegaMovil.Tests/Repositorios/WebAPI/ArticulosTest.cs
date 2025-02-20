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
    public class ArticulosTest
    {

        IArticuloRepository rep;
        public ArticulosTest()
        {
            rep = new ArticuloRepository();

        }

        [Fact]
        public void DebeObtenerArticulo()
        {
            GetArticuloUseCase uc = new GetArticuloUseCase(rep);
            var x = uc.ExecuteAsync("#13044", 90);

            //var d = rep.GetArticulo("#13044", 90);

            Assert.NotNull(x);
            Assert.NotNull(x.Result);
            //Assert.NotNull(d.Result);
            Assert.Equal("689958130443", x.Result.CodigoDeBarra);
            //Assert.Equal("689958130443", d.Result.CodigoDeBarra);
        }

        [Fact]
        public void DebeObtenerArticulosFiltrados()
        {
            GetArticulosUseCase uc = new GetArticulosUseCase(rep);
            var x = uc.ExecuteAsync("ACCESORIOS", 90);

            Assert.NotNull(x);
            Assert.True(x.Result.Count() >= 2);

        }
    }
}
