using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BodegaMovil.UseCases.DTO
{
    public class ArticuloDTO
    {
        public string SKU { get; set; }

        public string Descripcion { get; set; }
        public double ExistenciaCedis { get; set; }
        public string UbicacionCedis { get; set; }
        public string CodigoDeBarra { get; set; }

        public float FactorCompra { get; set; }

        public string UnidadCompra { get; set; }

        public float IVACompra { get; set; }

        public float IEPSCompra { get; set; }

        public string UnidadVenta { get; set; }

        public float IVAVenta { get; set; }

        public float IEPSVenta { get; set; }

        public int ID_SubFamilia { get; set; }

        public int ID_Marca { get; set; }

        public int ID_Area { get; set; }

        public bool EsCompuesto { get; set; }

        public bool NoInventariable { get; set; }
    }
}
