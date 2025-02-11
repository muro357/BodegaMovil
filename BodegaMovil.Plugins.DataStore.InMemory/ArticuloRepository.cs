using BodegaMovil.CoreBusiness;
using BodegaMovil.UseCases.DTO;
using BodegaMovil.UseCases.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BodegaMovil.Plugins.DataStore.InMemory
{
    public class ArticuloRepository : IArticuloRepository
    {
        List<Articulo> list;
        List<Existencia> existencia;

        public ArticuloRepository()
        {
            LoadArticulos();
            LoadExistencias();
        }
        public async Task<ArticuloDTO> GetArticulo(string sku, int id_tienda)
        {
            var l = (from art in list join ex in existencia 
                    on art.SKU equals ex.SKU
                    where ex.ID_Tienda == 1 
                    select new ArticuloDTO 
                    { 
                        SKU = art.SKU,
                        Descripcion = art.Descripcion,
                        UnidadVenta=art.UnidadVenta,
                        ExistenciaCedis = ex.ExistenciaActual,
                        UbicacionCedis = ex.Ubicacion,
                        ID_Area=art.ID_Area,
                    }).FirstOrDefault();

            if(l == null)
                l=new ArticuloDTO();

            return l;

        }

        public async Task<List<Articulo>> GetArticulos(string filtro)
        {
            return list.Where(x => x.SKU.StartsWith(filtro) || x.Descripcion.StartsWith(filtro)).ToList();
        }

        private void LoadArticulos()
        {
            list = new List<Articulo>()
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
