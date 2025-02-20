using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BodegaMovil.CoreBusiness
{
    public class PedidoDetalle
    {
        public int Consecutivo { get; set; }
        public int ID_Tienda { get; set; }
        public int ID_Area { get; set; }
        public string Tipo { get; set; } //Enumeracion TipoPedido
        public string Folio { get; set; }
        public string SKU { get; set; }
        public float CantidadVendida { get; set; }
        public float PedidoEspecial { get; set; }
        public float StockMinimo { get; set; }
        public float StockMaximo { get; set; }
        public float ExistenciaFiltro { get; set; }
        public string FormaDeCalculo { get; set; } //Enumeracion FormaDeCalculo
        public bool Modificado { get; set; }
        public string Observaciones { get; set; }
        public float CantidadPedida { get; set; }
        public float? CantidadSurtida { get; set; }
        public int Contenedor { get; set; }
        public bool Checado { get; set; }
        public DateTime? FechaSurtido { get; set; }
        public DateTime? FechaChecado { get; set; }
        public string Referencia { get; set; }//Folio de Ticket o Factura
        public float PedidoInferido { get; set; }

        public PedidoDetalle()
        {
            
        }

        public PedidoDetalle(
            Articulo art, 
            float? cantidad, 
            string formaCalc, 
            int consec, 
            int id_tienda, 
            string folio,
            string tipo)
        {
            this.Consecutivo = consec;
            this.ID_Area = art.ID_Area;
            this.ID_Tienda = id_tienda;
            this.Folio = folio;
            this.SKU = art.SKU;
            this.CantidadVendida = 0;
            this.StockMinimo = 0;
            this.StockMaximo = 0;
            this.ExistenciaFiltro = 0;
            this.FormaDeCalculo = formaCalc;
            this.Contenedor = 0;//int.Parse(txtCaja.Text),
            this.CantidadPedida = 0;
            this.CantidadSurtida = cantidad;
            this.Tipo = tipo;
        }
    }

    
}
