using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BodegaMovil.UseCases.DTO
{
    public class ShowPedidoDTO
    {
        public int Consecutivo { get; set; }
        public int ID_Tienda { get; set; }
        public int ID_Area { get; set; }
        public string Tipo { get; set; } //Puede ser Sugerido o Especial
        public string DescripcionTienda { get; set; }
        public string DescripcionArea { get; set; }
        public string Folio { get; set; }
        public string FechaSolicitado { get; set; }
        public string Estado { get; set; }//(Generado, Solicitado, Surtido, Checado, Embarcado, Finalizado) Generado = Se esta llenando en tienda el pedido, Solicitado = ya se envio pedido a bodega, Surtido = ya se finalizo el surtido, Checado = ya se checo todo, Finalizado = ya se hizo el movimiento de almacen
       

        
        }
    }
