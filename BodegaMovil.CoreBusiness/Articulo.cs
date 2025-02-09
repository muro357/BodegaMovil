using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BodegaMovil.CoreBusiness
{
    public class Articulo
    {
        public string SKU { get; set; }

        public string Descripcion { get; set; }

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

        //public float Peso { get; set; }

        //public float Largo { get; set; }

        //public float Ancho { get; set; }

        //public float Alto { get; set; }

        //public float ComisionVenta { get; set; }

        public DateTime? FechaAlta { get; set; }

        public DateTime? FechaUltimaModificacion { get; set; }

        public bool Descontinuado { get; set; }
    }
}
