using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BodegaMovil.CoreBusiness;
using BodegaMovil.UseCases;
using BodegaMovil.Plugins.DataStore.InMemory;
using BodegaMovil.UseCases.Interfaces;

namespace BodegaMovil.Tests
{
    public class ArticulosTest
    {
        
        IArticuloRepository rep;
        public ArticulosTest()
        {
            IArticuloRepository rep = new ArticuloRepository();
            
        }

        [Fact]
        public void DebeObtenerArticulo()
        {
            GetArticuloUseCase uc = new GetArticuloUseCase(rep);
            var x = uc.ExecuteAsync("10100",1);

            Assert.NotNull(x);
            Assert.Equal("ACCESORIOS P/COMPRESOR 14 PZAS AMERICAN TOOL",x.Result.Descripcion);
            Assert.Equal(100, x.Result.ExistenciaCedis);
        }

        [Fact]
        public void DebeObtenerArticulosFiltrados()
        {
            GetArticulosUseCase uc = new GetArticulosUseCase(rep);
            var x = uc.ExecuteAsync("ACCESORIOS");

            Assert.NotNull(x);
            Assert.True(x.Result.Count() >= 2);
            
        }
    }
}
