using BodegaMovil.CoreBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BodegaMovil.UseCases.DTO
{
    public class GetPedidoDTO
    {
       

        public string TipoPedido { get; set; }

        public string ListaTiendas
        {
            get; set;
        }
        public string ListaAreas
        {
            get; set;
        }

        
    }
}
