using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BodegaMovil.CoreBusiness
{
    public class Existencia
    {
        public string SKU { get; set; }
        public int ID_Tienda { get; set; }
        public string Ubicacion { get; set; }
        public double ExistenciaActual { get; set; }
        public DateTime FechaUltimaModificacion { get; set; }
    }
}
