using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BodegaMovil.UseCases.DTO
{
    public class GetArticuloDTO
    {
        public string SKU { get; set; }
        public int ID_Tienda { get; set; }
    }
}
