using BodegaMovil.CoreBusiness;
using BodegaMovil.UseCases.DTO;
using BodegaMovil.UseCases.Interfaces;
using BodegaMovil.CoreBusiness.Enums;


namespace BodegaMovil.Plugins.DataStore.InMemory
{
    public class PedidoRepository : IPedidoRepository
    {
        List<Pedido> pedidos;
        List<PedidoDetalle> pedidosDetalles;
        List<Area> areas;
        List<Tienda> tiendas;
        List<Articulo> articulos;
        List<Existencia> existencia;

        public PedidoRepository()
        {
            LoadTiendas();
            LoadAreas();
            LoadArticulos();
            LoadPedidos();
            LoadPedidosDetalles();
            LoadExistencias();
        }
        public async Task<bool> AgregarArticulo(PedidoDetalle linea)
        {
            //var p = pedidos.FirstOrDefault(x => x.ID_Tienda == pedido.ID_Tienda && x.Consecutivo == pedido.Consecutivo);
            //p.AddDetalle(art,cantidad,formaCalc);
            return true;
        }

        public async Task ContemplarExistencia(IEnumerable<PedidoDetalle> pedidoDetalles)
        {
            
        }

        public async Task Depurar(IEnumerable<PedidoDetalle> pedidoDetalles)
        {
            
        }

        public async Task<PedidoDTO> GetSurtirById(int id, int id_tienda, int id_area)
        {
            var pedido = (from p in pedidos
                          join t in tiendas on p.ID_Tienda equals t.ID_Tienda
                          join ar in areas on p.ID_Area equals ar.ID_Area
                           where p.Consecutivo == id && p.ID_Tienda == id_tienda  
                          select new PedidoDTO
                          {
                              Consecutivo = p.Consecutivo,
                              Folio = p.Folio,
                              ID_Area = p.ID_Area,
                              DescripcionArea = p.ID_Area != 0 ? ar.Descripcion : "Varios",
                              ID_AreaSurtir = id_area,
                              ID_Tienda=p.ID_Tienda,
                              DescripcionTienda=t.Descripcion,
                              FechaSolicitado = p.FechaSolicitado,
                              Tipo = (TipoPedido)Enum.Parse(typeof(TipoPedido), p.Tipo),
                              Estado = (EstadoDePedido)Enum.Parse(typeof(EstadoDePedido), p.Estado),
                          }).FirstOrDefault();

            pedido.PedidoDetalle = (from p in pedidos
                                    join pd in pedidosDetalles on new { p.Consecutivo, p.ID_Tienda } equals new                 { pd.Consecutivo, pd.ID_Tienda }
                                    join t in tiendas on p.ID_Tienda equals t.ID_Tienda
                                    join ar in areas on p.ID_Area equals ar.ID_Area
                                    join arts in articulos on pd.SKU equals arts.SKU
                                    where p.Consecutivo == id && p.ID_Tienda == id_tienda && pd.ID_Area == id_area
                                    select new PedidoDetalleDTO
                                    {
                                        Consecutivo = p.Consecutivo,
                                        Folio = p.Folio,
                                        ID_Area = p.ID_Area,
                                        DescripcionArea = p.ID_Area != 0 ? ar.Descripcion : "Varios",
                                        ID_Tienda = p.ID_Tienda,
                                        SKU = t.Descripcion,
                                        Descripcion = arts.Descripcion,
                                        CantidadSurtida = pd.CantidadSurtida,
                                        Unidad = arts.UnidadVenta,
                                        CantidadPedida = pd.CantidadPedida,
                                        ExistenciaCedis = 10,
                                        UbicacionCedis = "A1",
                                        Tipo = (TipoPedido)Enum.Parse(typeof(TipoPedido), p.Tipo),
                                    }).ToList();
            
            return pedido;
        }

        public async Task<List<ShowPedidoDTO>> GetPedidosSurtir(IEnumerable<int> ID_Tienda, IEnumerable<int> ID_Area)
        {
            var lista = (from p in pedidos join pd in pedidosDetalles 
                             on new { p.Consecutivo, p.ID_Tienda } equals new { pd.Consecutivo,pd.ID_Tienda }
                             join ar in areas on pd.ID_Area equals ar.ID_Area 
                             join t in tiendas on p.ID_Tienda equals t.ID_Tienda
                         
                    select new PedidoDTO 
                        { Consecutivo = p.Consecutivo, 
                          Folio = p.Folio,
                          ID_Area = pd.ID_Area,
                          DescripcionArea = ar.Descripcion,
                          ID_Tienda = pd.ID_Tienda,
                          DescripcionTienda = t.Descripcion,
                          FechaSolicitado = p.FechaSolicitado,
                          Tipo = (TipoPedido)Enum.Parse(typeof(TipoPedido), p.Tipo),
                          Estado = (EstadoDePedido)Enum.Parse(typeof(EstadoDePedido), p.Estado),
                    }).ToList();

            var listaAgrupada = lista
                                .GroupBy(x => new 
                                    { 
                                        x.Consecutivo, 
                                        x.Folio, 
                                        x.ID_Tienda, 
                                        x.ID_Area, 
                                        x.Tipo, 
                                        x.Estado,
                                        x.DescripcionTienda,
                                        x.DescripcionArea
                                    })
                                .Select(g => new
                                {
                                    g.Key.Consecutivo,
                                    g.Key.Folio,
                                    g.Key.ID_Area,
                                    g.Key.ID_Tienda,
                                    g.Key.Tipo,
                                    g.Key.Estado,
                                    g.Key.DescripcionTienda,
                                    g.Key.DescripcionArea,
                                    FechaSolicitado = g.Select(p => p.FechaSolicitado).ToList()
                                });
            return null;

        }

        public Task<bool> Surtir(PedidoDetalle linea)
        {
            throw new NotImplementedException();
        }

        private void LoadAreas()
        {
            areas = new List<Area>()
                {
                    new Area
                    {
                        ID_Area= 10,
                        Descripcion="Articulos Varios"
                    },
                    new Area
                    {
                        ID_Area = 11,
                        Descripcion = "Plomeria"
                    },
                    new Area
                    {
                        ID_Area = 14,
                        Descripcion = "Diamante"
                    },
                    new Area
                    {
                        ID_Area = 19,
                        Descripcion = "Material electrico"
                    }
                };
        }

        private void LoadTiendas()
        {
            tiendas = new List<Tienda>
            {
                new Tienda
                {
                    ID_Tienda=1, Descripcion="TAcayucan"
                },
                new Tienda
                {
                    ID_Tienda=2, Descripcion="TSan Andres"
                },
                new Tienda
                {
                    ID_Tienda = 3, Descripcion="TMinatitlan"
                },


            };
        }

        private void LoadArticulos()
        {
            articulos = new List<Articulo>()
            {
                new Articulo()
                {
                    SKU = "10100",
                    ID_Area = 10,
                    UnidadVenta = "PZA",
                    Descripcion = "ACCESORIOS P/COMPRESOR 14 PZAS AMERICAN TOOL"
                },
                new Articulo()
                {
                    SKU = "10200",
                    ID_Area = 10,
                    UnidadVenta = "PZA",
                    Descripcion = "ACCESORIOS P/COMPRESOR 18 PZAS AMERICAN TOOL"
                },
                new Articulo()
                {
                    SKU = "10300",
                    ID_Area = 10,
                    UnidadVenta = "PZA",
                    Descripcion = "MATRACA DE 3/8 AMERICAN T.E.PRO"
                },
                new Articulo()
                {
                    SKU = "10400",
                    ID_Area = 10,
                    UnidadVenta = "PZA",
                    Descripcion = "MATRACA DE 1/2 AMERICAN T.E.PRO"
                },
                new Articulo()
                {
                    SKU = "10500",
                    ID_Area = 10,
                    UnidadVenta = "PZA",
                    Descripcion = "ABRAZADERA P/VARILLA DE TIERRA"
                },
                new Articulo()
                {
                    SKU = "20100",
                    ID_Area = 11,
                    UnidadVenta = "KIT",
                    Descripcion = "KIT DE SOLD. MONARCA 1/2X1/2 Y PASTA GENE. 2000"
                },
                new Articulo()
                {
                    SKU = "20200",
                    ID_Area = 11,
                    UnidadVenta = "PZA",
                    Descripcion = "CARTUCHO DE GAS BUTANO 275G LINMEX"
                },
                new Articulo()
                {
                    SKU = "20300",
                    ID_Area = 11,
                    UnidadVenta = "PZA",
                    Descripcion = "LLAVE DE NARIZ CROMADA C/LLAVE DE SEGURIDAD"
                },
                new Articulo()
                {
                    SKU = "20400",
                    ID_Area = 11,
                    UnidadVenta = "PZA",
                    Descripcion = "REGADERA P/BAÑO T/TELEFONO LYQ-1 EN BOLSA HAIDI"
                },
                new Articulo()
                {
                    SKU = "20500",
                    ID_Area = 11,
                    UnidadVenta = "PZA",
                    Descripcion = "ADAPTADOR CESPOL P/LAVABO 1 1/2 X 1"
                },
                new Articulo()
                {
                    SKU = "30100",
                    ID_Area = 14,
                    UnidadVenta = "PZA",
                    Descripcion = "LAVABO SOLO ROSA GRANDE"
                },
                new Articulo()
                {
                    SKU = "30200",
                    ID_Area = 14,
                    UnidadVenta = "PZA",
                    Descripcion = "CAJA UNIVERSAL DE PVC"
                },
                new Articulo()
                {
                    SKU = "30300",
                    ID_Area = 14,
                    UnidadVenta = "PZA",
                    Descripcion = "COLADERA UNIVERSAL MULTIPLE DE PVC"
                },
                new Articulo()
                {
                    SKU = "30400",
                    ID_Area = 14,
                    UnidadVenta = "ROL",
                    Descripcion = "TELA CICLON FORRADA CAL.12.5-63X63 2.50M ALTOX20"
                },
                new Articulo()
                {
                    SKU = "30500",
                    ID_Area = 14,
                    UnidadVenta = "ROL",
                    Descripcion = "TELA CRIBA 1X10 M 8X8"
                },
                new Articulo()
                {
                    SKU = "40100",
                    ID_Area = 19,
                    UnidadVenta = "MT",
                    Descripcion = "CABLE COAXIAL RG-59 X 305M"
                },
                new Articulo()
                {
                    SKU = "40200",
                    ID_Area = 19,
                    UnidadVenta = "PZA",
                    Descripcion = "LAMPARA DECORATIVA P93341-19 SLI LIGHTING"
                },
                new Articulo()
                {
                    SKU = "40300",
                    ID_Area = 19,
                    UnidadVenta = "PZA",
                    Descripcion = "ACOPLADOR EXTERIOR PARA T.V 01-2010"
                },
                new Articulo()
                {
                    SKU = "40400",
                    ID_Area = 19,
                    UnidadVenta = "PZA",
                    Descripcion = "APAGADOR  TALARI SIEMENS  10003E MARFIL"
                },
                new Articulo()
                {
                    SKU = "40500",
                    ID_Area = 19,
                    UnidadVenta = "PZA",
                    Descripcion = "APAGADOR DE PALANCA 2T SI-1021"
                },
            };
        }
        private void LoadPedidos()
        {
            pedidos = new List<Pedido>
            { 
                new Pedido
                {
                    Consecutivo = 1,
                    ID_Tienda = 1,
                    ID_Area = 10,
                    Folio = "PS01001",
                    Estado = "Solicitado",
                    Tipo = "Sugerido",
                    FechaGenerado = DateTime.Now,
                    FechaSolicitado = DateTime.Now,
                    
                    Detalles= new List<PedidoDetalle>
                    {

                    }
                },
                new Pedido
                {
                    Consecutivo = 2,
                    ID_Tienda = 1,
                    ID_Area = 11,
                    Folio = "PS01002",
                    Estado = "Solicitado",
                    Tipo = "Sugerido",
                    FechaGenerado = DateTime.Now,
                    FechaSolicitado = DateTime.Now,
                    Detalles= new List<PedidoDetalle>
                    {

                    }
                },
                new Pedido
                {
                    Consecutivo = 3,
                    ID_Tienda = 1,
                    ID_Area = 14,
                    Folio = "PS01003",
                    Estado = "Solicitado",
                    Tipo = "Sugerido",
                    Detalles= new List<PedidoDetalle>
                    {

                    }
                },
                new Pedido
                {
                    Consecutivo = 4,
                    ID_Tienda = 1,
                    ID_Area = 19,
                    Folio = "PS01004",
                    Estado = "Solicitado",
                    Tipo = "Sugerido",
                    Detalles= new List<PedidoDetalle>
                    {

                    }
                },
                new Pedido
                {
                    Consecutivo = 5,
                    ID_Tienda = 2,
                    ID_Area = 10,
                    Folio = "PS02005",
                    Estado = "Solicitado",
                    Tipo = "Sugerido",
                    FechaGenerado = DateTime.Now,
                    FechaSolicitado = DateTime.Now,
                    Detalles= new List<PedidoDetalle>
                    {

                    }
                },
                new Pedido
                {
                    Consecutivo = 6,
                    ID_Tienda = 2,
                    ID_Area = 11,
                    Folio = "PS02006",
                    Estado = "Solicitado",
                    Tipo = "Sugerido",
                    FechaGenerado = DateTime.Now,
                    FechaSolicitado = DateTime.Now,
                    Detalles= new List<PedidoDetalle>
                    {

                    }
                },
                new Pedido
                {
                    Consecutivo = 7,
                    ID_Tienda = 2,
                    ID_Area = 14,
                    Folio = "PS02007",
                    Estado = "Solicitado",
                    Tipo = "Sugerido",
                    FechaGenerado = DateTime.Now,
                    FechaSolicitado = DateTime.Now,
                    Detalles= new List<PedidoDetalle>
                    {

                    }
                },
                new Pedido
                {
                    Consecutivo = 8,
                    ID_Tienda = 3,
                    ID_Area = 19,
                    Folio = "PS03008",
                    Estado = "Solicitado",
                    Tipo = "Sugerido",
                    FechaGenerado = DateTime.Now,
                    FechaSolicitado = DateTime.Now,
                    Detalles= new List<PedidoDetalle>
                    {

                    }
                },
                new Pedido
                {
                    Consecutivo = 9,
                    ID_Tienda = 3,
                    ID_Area = 14,
                    Folio = "PS03009",
                    Estado = "Solicitado",
                    Tipo = "Sugerido",
                    FechaGenerado = DateTime.Now,
                    FechaSolicitado = DateTime.Now,
                    Detalles= new List<PedidoDetalle>
                    {

                    }
                },
                new Pedido
                {
                    Consecutivo = 10,
                    ID_Tienda = 3,
                    ID_Area = 0,
                    Folio = "PS03010",
                    Estado = "Solicitado",
                    Tipo = "Especial",
                    FechaGenerado = DateTime.Now,
                    FechaSolicitado = DateTime.Now,
                    Detalles= new List<PedidoDetalle>
                    {

                    }
                },
            };
        }

        private void LoadPedidosDetalles()
        {
            pedidosDetalles = new List<PedidoDetalle>()
            {
                new PedidoDetalle
                {
                    Consecutivo = 1,
                    Folio = "PS01001",
                    ID_Tienda=1,
                    ID_Area=10,
                    SKU="10100",
                    CantidadPedida = 10,
                    FormaDeCalculo="Venta",
                    Tipo = "Sugerido",
                    CantidadSurtida=null
                    
                },
                new PedidoDetalle
                {
                    Consecutivo = 1,
                    Folio = "PS01001",
                    ID_Tienda=1,
                    ID_Area=10,
                    SKU="10200",
                    CantidadPedida = 10,
                    FormaDeCalculo="Venta",
                    Tipo = "Sugerido",
                    CantidadSurtida=null

                },
                new PedidoDetalle
                {
                    Consecutivo = 1,
                    Folio = "PS01001",
                    ID_Tienda=1,
                    ID_Area=10,
                    SKU="10300",
                    CantidadPedida = 10,
                    FormaDeCalculo="Venta",
                    Tipo = "Sugerido",
                    CantidadSurtida=null

                },
                new PedidoDetalle
                {
                    Consecutivo = 1,
                    Folio = "PS01001",
                    ID_Tienda=1,
                    ID_Area=10,
                    SKU="10400",
                    CantidadPedida = 10,
                    FormaDeCalculo="Venta",
                    Tipo = "Sugerido",
                    CantidadSurtida=null

                },
                new PedidoDetalle
                {
                    Consecutivo = 2,
                    Folio = "PS01002",
                    ID_Tienda=1,
                    ID_Area=11,
                    SKU="20100",
                    CantidadPedida = 10,
                    FormaDeCalculo="Venta",
                    Tipo = "Sugerido",
                    CantidadSurtida=null

                },
                new PedidoDetalle
                {
                    Consecutivo = 2,
                    Folio = "PS01002",
                    ID_Tienda=1,
                    ID_Area=11,
                    SKU="20200",
                    CantidadPedida = 10,
                    FormaDeCalculo="Venta",
                    Tipo = "Sugerido",
                    CantidadSurtida=null

                },
                new PedidoDetalle
                {
                    Consecutivo = 2,
                    Folio = "PS01002",
                    ID_Tienda=1,
                    ID_Area=10,
                    SKU="20300",
                    CantidadPedida = 24,
                    FormaDeCalculo="Venta",
                    Tipo = "Sugerido",
                    CantidadSurtida=null

                },
                new PedidoDetalle
                {
                    Consecutivo = 2,
                    Folio = "PS01002",
                    ID_Tienda=1,
                    ID_Area=11,
                    SKU="20400",
                    CantidadPedida = 17,
                    FormaDeCalculo="Venta",
                    Tipo = "Sugerido",
                    CantidadSurtida=null

                },
                new PedidoDetalle
                {
                    Consecutivo = 3,
                    Folio = "PS01003",
                    ID_Tienda=1,
                    ID_Area=14,
                    SKU="30100",
                    CantidadPedida = 10,
                    FormaDeCalculo="Venta",
                    Tipo = "Sugerido",
                    CantidadSurtida=null

                },
                new PedidoDetalle
                {
                    Consecutivo = 3,
                    Folio = "PS01003",
                    ID_Tienda=1,
                    ID_Area=14,
                    SKU="30200",
                    CantidadPedida = 10,
                    FormaDeCalculo="Venta",
                    Tipo = "Sugerido",
                    CantidadSurtida=null

                },
                new PedidoDetalle
                {
                    Consecutivo = 4,
                    Folio = "PS01004",
                    ID_Tienda=1,
                    ID_Area=19,
                    SKU="40100",
                    CantidadPedida = 10,
                    FormaDeCalculo="Venta",
                    Tipo = "Sugerido",
                    CantidadSurtida=null

                },
                new PedidoDetalle
                {
                    Consecutivo = 4,
                    Folio = "PS01004",
                    ID_Tienda=1,
                    ID_Area=19,
                    SKU="40200",
                    CantidadPedida = 31,
                    FormaDeCalculo="Venta",
                    Tipo = "Sugerido",
                    CantidadSurtida=null

                },
                new PedidoDetalle
                {
                    Consecutivo = 4,
                    Folio = "PS01004",
                    ID_Tienda=1,
                    ID_Area=19,
                    SKU="40300",
                    CantidadPedida = 40,
                    FormaDeCalculo="Venta",
                    Tipo = "Sugerido",
                    CantidadSurtida=null

                },
                new PedidoDetalle
                {
                    Consecutivo = 5,
                    Folio = "PS02005",
                    ID_Tienda=2,
                    ID_Area=10,
                    SKU="10100",
                    CantidadPedida = 10,
                    FormaDeCalculo="Venta",
                    Tipo = "Sugerido",
                    CantidadSurtida=null

                },
                new PedidoDetalle
                {
                    Consecutivo = 5,
                    Folio = "PS02005",
                    ID_Tienda=2,
                    ID_Area=10,
                    SKU="10200",
                    CantidadPedida = 10,
                    FormaDeCalculo="Venta",
                    Tipo = "Sugerido",
                    CantidadSurtida=null

                },
                new PedidoDetalle
                {
                    Consecutivo = 5,
                    Folio = "PS02005",
                    ID_Tienda=1,
                    ID_Area=10,
                    SKU="10300",
                    CantidadPedida = 10,
                    FormaDeCalculo="Venta",
                    Tipo = "Sugerido",
                    CantidadSurtida=null

                },
                new PedidoDetalle
                {
                    Consecutivo = 6,
                    Folio = "PS02006",
                    ID_Tienda=2,
                    ID_Area=11,
                    SKU="20200",
                    CantidadPedida = 25,
                    FormaDeCalculo="Venta",
                    Tipo = "Sugerido",
                    CantidadSurtida=null

                },
                new PedidoDetalle
                {
                    Consecutivo = 6,
                    Folio = "PS02006",
                    ID_Tienda=2,
                    ID_Area=11,
                    SKU="20300",
                    CantidadPedida = 25,
                    FormaDeCalculo="Venta",
                    Tipo = "Sugerido",
                    CantidadSurtida=null

                },
                new PedidoDetalle
                {
                    Consecutivo = 6,
                    Folio = "PS02006",
                    ID_Tienda=2,
                    ID_Area=11,
                    SKU="20400",
                    CantidadPedida = 25,
                    FormaDeCalculo="Venta",
                    Tipo = "Sugerido",
                    CantidadSurtida=null

                },
                new PedidoDetalle
                {
                    Consecutivo = 7,
                    Folio = "PS02007",
                    ID_Tienda=2,
                    ID_Area=14,
                    SKU="30300",
                    CantidadPedida = 25,
                    FormaDeCalculo="Venta",
                    Tipo = "Sugerido",
                    CantidadSurtida=null

                },
                new PedidoDetalle
                {
                    Consecutivo = 7,
                    Folio = "PS02007",
                    ID_Tienda=2,
                    ID_Area=14,
                    SKU="30400",
                    CantidadPedida = 25,
                    FormaDeCalculo="Venta",
                    Tipo = "Sugerido",
                    CantidadSurtida=null

                },
                new PedidoDetalle
                {
                    Consecutivo = 7,
                    Folio = "PS02007",
                    ID_Tienda=2,
                    ID_Area=14,
                    SKU="30500",
                    CantidadPedida = 25,
                    FormaDeCalculo="Venta",
                    Tipo = "Sugerido",
                    CantidadSurtida=null

                },
                new PedidoDetalle
                {
                    Consecutivo = 8,
                    Folio = "PS03008",
                    ID_Tienda=3,
                    ID_Area=19,
                    SKU="40100",
                    CantidadPedida = 25,
                    FormaDeCalculo="Venta",
                    Tipo = "Sugerido",
                    CantidadSurtida=null

                },
                new PedidoDetalle
                {
                    Consecutivo = 8,
                    Folio = "PS03008",
                    ID_Tienda=3,
                    ID_Area=19,
                    SKU="40400",
                    CantidadPedida = 25,
                    FormaDeCalculo="Venta",
                    Tipo = "Sugerido",
                    CantidadSurtida=null

                },
                new PedidoDetalle
                {
                    Consecutivo = 9,
                    Folio = "PS03009",
                    ID_Tienda=3,
                    ID_Area=14,
                    SKU="30200",
                    CantidadPedida = 25,
                    FormaDeCalculo="Venta",
                    Tipo = "Sugerido",
                    CantidadSurtida=null

                },
                new PedidoDetalle
                {
                    Consecutivo = 9,
                    Folio = "PS03009",
                    ID_Tienda=3,
                    ID_Area=14,
                    SKU="30300",
                    CantidadPedida = 25,
                    FormaDeCalculo="Venta",
                    Tipo = "Sugerido",
                    CantidadSurtida=null

                },
                new PedidoDetalle
                {
                    Consecutivo = 10,
                    Folio = "PE03010",
                    ID_Tienda=3,
                    ID_Area=10,
                    SKU="10200",
                    CantidadPedida = 30,
                    FormaDeCalculo="Pedido_Especial",
                    Tipo = "Especial",
                    CantidadSurtida=null

                },
                new PedidoDetalle
                {
                    Consecutivo = 10,
                    Folio = "PE03010",
                    ID_Tienda=3,
                    ID_Area=11,
                    SKU="20200",
                    CantidadPedida = 35,
                    FormaDeCalculo="Pedido_Especial",
                    Tipo = "Especial",
                    CantidadSurtida=null

                },
                new PedidoDetalle
                {
                    Consecutivo = 10,
                    Folio = "PE03010",
                    ID_Tienda=3,
                    ID_Area=14,
                    SKU="30200",
                    CantidadPedida = 38,
                    FormaDeCalculo="Pedido_Especial",
                    Tipo = "Especial",
                    CantidadSurtida=null

                },
                new PedidoDetalle
                {
                    Consecutivo = 10,
                    Folio = "PE03010",
                    ID_Tienda=3,
                    ID_Area=19,
                    SKU="40200",
                    CantidadPedida = 40,
                    FormaDeCalculo="Pedido_Especial",
                    Tipo = "Especial",
                    CantidadSurtida=null

                },
            };
        }

        private void LoadExistencias()
        {
            existencia = new List<Existencia>()
            {
                new Existencia()
                {
                    SKU = "10100",
                    ID_Tienda = 1,
                    Ubicacion = "A1",
                    ExistenciaActual=10
                },
                new Existencia()
                {
                    SKU = "10200",
                    ID_Tienda = 1,
                    Ubicacion = "A1",
                    ExistenciaActual=10
                },
                new Existencia()
                {
                    SKU = "10300",
                    ID_Tienda = 1,
                    Ubicacion = "A1",
                    ExistenciaActual=10
                },
                new Existencia()
                {
                    SKU = "10400",
                    ID_Tienda = 1,
                    Ubicacion = "A1",
                    ExistenciaActual=10
                },
                new Existencia()
                {
                    SKU = "10500",
                    ID_Tienda = 1,
                    Ubicacion = "A1",
                    ExistenciaActual=10
                },
                new Existencia()
                {
                    SKU = "20100",
                    ID_Tienda = 1,
                    Ubicacion = "A1",
                    ExistenciaActual=10
                },
                new Existencia()
                {
                    SKU = "20200",
                    ID_Tienda = 1,
                    Ubicacion = "A1",
                    ExistenciaActual=10
                },
                new Existencia()
                {
                    SKU = "20300",
                    ID_Tienda = 1,
                    Ubicacion = "A1",
                    ExistenciaActual=10
                },
                new Existencia()
                {
                    SKU = "20400",
                    ID_Tienda = 1,
                    Ubicacion = "A1",
                    ExistenciaActual=10
                },
                new Existencia()
                {
                    SKU = "20500",
                    ID_Tienda = 1,
                    Ubicacion = "A1",
                    ExistenciaActual=10
                },
                new Existencia()
                {
                    SKU = "30100",
                    ID_Tienda = 1,
                    Ubicacion = "A1",
                    ExistenciaActual=10
                },
                new Existencia()
                {
                    SKU = "30200",
                    ID_Tienda = 1,
                    Ubicacion = "A1",
                    ExistenciaActual=10
                },
                new Existencia()
                {
                    SKU = "30300",
                    ID_Tienda = 1,
                    Ubicacion = "A1",
                    ExistenciaActual=10
                },
                new Existencia()
                {
                    SKU = "30400",
                    ID_Tienda = 1,
                    Ubicacion = "A1",
                    ExistenciaActual=10
                },
                new Existencia()
                {
                    SKU = "30500",
                    ID_Tienda = 1,
                    Ubicacion = "A1",
                    ExistenciaActual=10
                },
                new Existencia()
                {
                    SKU = "40100",
                    ID_Tienda = 1,
                    Ubicacion = "A1",
                    ExistenciaActual=10
                },
                new Existencia()
                {
                    SKU = "40200",
                    ID_Tienda = 1,
                    Ubicacion = "A1",
                    ExistenciaActual=10
                },
                new Existencia()
                {
                    SKU = "40300",
                    ID_Tienda = 1,
                    Ubicacion = "A1",
                    ExistenciaActual=10
                },
                new Existencia()
                {
                    SKU = "40400",
                    ID_Tienda = 1,
                    Ubicacion = "A1",
                    ExistenciaActual=10
                },
                new Existencia()
                {
                    SKU = "40500",
                    ID_Tienda = 1,
                    Ubicacion = "A1",
                    ExistenciaActual=10
                },
            };
        }



    }
}
