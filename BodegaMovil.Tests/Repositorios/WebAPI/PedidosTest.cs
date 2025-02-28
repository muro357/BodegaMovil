using BodegaMovil.Plugins.DataStore.WebApi;
using BodegaMovil.UseCases;
using BodegaMovil.UseCases.Interfaces;
using BodegaMovil.CoreBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BodegaMovil.Services.Maps;
using BodegaMovil.Services.Maps.AutoMapper;
using BodegaMovil.UseCases.DTO;

namespace BodegaMovil.Tests.Repositorios.WebAPI
{
    public class PedidosTest
    {
        IPedidoRepository rep;
        public PedidosTest()
        {
            rep = new PedidoRepository();
        }

        [Fact]
        public void DebeAgregarArt()
        {
            //ObtenerPedido
            var pedidouc = new GetPedidoSurtirUseCase(rep);
            var ped = pedidouc.ExecuteAsync(1, "PS0100001545");
            ped.Result.ID_AreaSurtir = 10;
            //Iniciar un mapeador
            var map = new AutoMapperConfig();

            //Obtener Articulo
            IArticuloRepository repArt = new ArticuloRepository();
            GetArticuloUseCase artsuc = new GetArticuloUseCase(repArt);
            var artDTO = artsuc.ExecuteAsync("10101", 90);
            var art = map.GetEntity<ArticuloDTO, Articulo>(artDTO.Result);
            

            //Agregar un Articulo
            var uc = new AddNewArtSurtirUseCase(rep,map);
            var x = uc.ExecuteAsync(ped.Result, art, 10, CoreBusiness.Enums.FormaDeCalculo.Articulo_Agregado);

            Assert.NotNull(ped.Result);
            Assert.NotNull(x.Result);
            Assert.Equal("06610101", x.Result.CodigoDeBarra);

            //var obj = new ArticuloDTO
            //{
            //    SKU = "10101",
            //    Descripcion = "ABRAZADERA P/VARILLA DE TIERRA",
            //    ExistenciaCedis = 744,
            //    UbicacionCedis = "",
            //    CodigoDeBarra = "610530601533",
            //    FactorCompra = 1,
            //    UnidadCompra = "PZA",
            //    IVACompra = 1,
            //    IEPSCompra = 0,
            //    UnidadVenta = "PZA",
            //    IVAVenta = 1,
            //    IEPSVenta = 0,
            //    ID_SubFamilia = 0,
            //    ID_Marca = 0,
            //    ID_Area = 10,
            //    EsCompuesto = false,
            //    NoInventariable = false
            //};

            //var art2 = map.GetEntity<ArticuloDTO, Articulo>(obj);
            //var ped2 = map.GetEntity<PedidoDTO, Pedido>(ped.Result);
            //var item = ped2.AddDetalle(art2, 10, "Articulo_Agregado");
            //var res = rep.AgregarArticulo(item);

            //Assert.NotNull(item);
            //Assert.Equal("Sugerido", item.Tipo);
            //Assert.True(res.Result);



        }

        [Fact]
        public void DebeContemplarExistencias()
        {
            var map = new AutoMapperConfig();
            var uc = new ContemplarExistenciaUseCase(rep, map);
            var getpedido = new GetPedidoSurtirUseCase(rep);

            var p = getpedido.ExecuteAsync(1, "PS0100001545");

            var pd = p.Result.PedidoDetalle.Where(x => x.SKU == "00061" || x.SKU == "00062").ToList();

            foreach (var item in pd)
            {
                item.ExistenciaCedis = 0;
                item.CantidadSurtida = null;
            }

            var x = uc.ExecuteAsync(p.Result);

            Assert.NotNull(p);
            Assert.NotNull(pd);
            Assert.True(x.Result);
        }

        [Fact]
        public void DebeDepurar()
        {
            var map = new AutoMapperConfig();
            var uc = new DepurarUseCase(rep, map);
            var getpedido = new GetPedidoSurtirUseCase(rep);

            var p = getpedido.ExecuteAsync(1, "PS0100001545");

            var pd = p.Result.PedidoDetalle.Where(x => x.SKU == "#13044" || x.SKU == "#13045").ToList();

            foreach(var item in pd)
            {
                //if(item.SKU == "#13044" || item.SKU == "#13045")
                    item.Elegido = true;
                item.CantidadSurtida = null;
            }

            var x = uc.ExecuteAsync(p.Result);

            Assert.NotNull(p);
            Assert.NotNull(pd);
            Assert.True(x.Result);
        }

        [Fact]
        public void DebeObtenerPedidosParaSurtir()
        {
            var uc = new GetPedidosSurtirUseCase(rep);

            var user = new UsuarioDTO()
            {
                usuario = "admin",
                password= "control",
                ListaTiendasAsignadas = new List<Usuarios_Tiendas>()
                {
                    new Usuarios_Tiendas() {ID_Tienda =1, usuario = "admin", DescripcionTienda = "Acayucan"},
                    new Usuarios_Tiendas() {ID_Tienda =2, usuario = "admin", DescripcionTienda = "Mina"},
                    new Usuarios_Tiendas() {ID_Tienda =3, usuario = "admin", DescripcionTienda = "Coatza"},
                    new Usuarios_Tiendas() {ID_Tienda =4, usuario = "admin", DescripcionTienda = "Isla"},
                    new Usuarios_Tiendas() {ID_Tienda =5, usuario = "admin", DescripcionTienda = "San Andres"}
                },
                ListaAreasAsignadas = new List<Usuarios_Areas>()
                {
                    new Usuarios_Areas(){ID_Area = 10, DescripcionArea="Articulos Varios"},
                    new Usuarios_Areas(){ID_Area = 11, DescripcionArea="Herramientas"},
                    new Usuarios_Areas(){ID_Area = 14, DescripcionArea="Diamante"},
                    new Usuarios_Areas(){ID_Area = 19, DescripcionArea="Plomeria"},
                }
                
            };

            var tiendas = new List<int>
            {
                1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14
            };
            var areas = new List<int>
            {
                10,11,14,19
            };
            var x = uc.ExecuteAsync(user,0,0);

            Assert.NotNull(x.Result);
            Assert.True(x.Result.Count > 3);
        }

        [Fact]
        public void DebeObtenerUnPedidoParaSurtir()
        {
            var uc = new GetPedidoSurtirUseCase(rep);
            
            var x = uc.ExecuteAsync(1, "PS0100001550");

            Assert.NotNull(x.Result);
            Assert.Equal("Plomeria",x.Result.DescripcionArea);
        }

        [Fact]
        public void DebeSurtir()
        {
            var map = new AutoMapperConfig();
            var uc = new SurtirUseCase(rep, map);
            var getpedido = new GetPedidoSurtirUseCase(rep);

            var p = getpedido.ExecuteAsync(1, "PS0100001545");

            //var pd = p.Result.PedidoDetalle.FirstOrDefault(x => x.SKU == "#13044");

            var pd = new PedidoDetalleDTO()
            {
                Folio="PS0100001545", 
                SKU="#13044", 
                CantidadSurtida=50, 
                Contenedor=30
            };

            var x = uc.ExecuteAsync(p.Result,pd);

            //var ts = 

            //var xd = rep.Surtir(pd);

            Assert.NotNull(p);
            Assert.NotNull(pd);
            Assert.True(x.Result);
        }


    }
}
