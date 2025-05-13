using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BodegaMovil.CoreBusiness
{
    public class Pedido
    {
        public int Consecutivo { get; set; }
        public int ID_Tienda { get; set; }
        public int ID_Area { get; set; }
        public string Tipo { get; set; } //Puede ser Sugerido o Especial
        public int ID_Empleado { get; set; }
        public string Usuario { get; set; }
        public string Folio { get; set; }
        public DateTime FechaGenerado { get; set; }
        public DateTime? FechaInicialFiltro { get; set; }
        public DateTime? FechaFinalFiltro { get; set; }
        public float PorcentajeStockMaximo { get; set; }
        public bool SoloVentas { get; set; }
        public DateTime? FechaSolicitado { get; set; }
        public DateTime? FechaTransferido { get; set; }
        public string SurtidoPor { get; set; }
        public string ChecadoPor { get; set; }
        public string ObservacionesAduana { get; set; }
        public string Placas { get; set; }
        public string NumeroDeCandado { get; set; }
        public string Chofer { get; set; }
        public string Estado { get; set; }//(Generado, Solicitado, Surtido, Checado, Embarcado, Finalizado) Generado = Se esta llenando en tienda el pedido, Solicitado = ya se envio pedido a bodega, Surtido = ya se finalizo el surtido, Checado = ya se checo todo, Finalizado = ya se hizo el movimiento de almacen
        public string RFC_Cliente { get; set; }
        public string EnviarPedidoA { get; set; }

        public string CanceladoPor { get; set; }

        public List<PedidoDetalle> Detalles { get; set; }

        public Pedido()
        {
            this.Detalles = new List<PedidoDetalle>();
        }

        public PedidoDetalle AddDetalle(Articulo art, float? cantidad, string formaCalc)
        {
            var item = Detalles.FirstOrDefault(x => x.SKU == art.SKU);

            if (item != null)
            {
                throw new InvalidOperationException("Ya esta agregado");
            }

            //if(Tipo == "Sugerido" && (ID_Area != art.ID_Area))
            //{
            //    throw new InvalidOperationException("En un pedido sugerido solo puede agregar articulos de la misma area");
            //}

            var newItem = new PedidoDetalle(art, cantidad, formaCalc, this.Consecutivo,this.ID_Tienda,this.Folio, this.Tipo);

            Detalles.Add(newItem);
            return newItem;
            
        }
    }



}
