using BodegaMovil.CoreBusiness.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BodegaMovil.UseCases.DTO
{
    public class PedidoDetalleDTO
    {
        public bool Elegido { get; set; }
        public int Consecutivo { get; set; }
        public string CodigoDeBarra { get; set; }
        public string Descripcion { get; set; }
        public string Unidad { get; set; }
        public string DescripcionArea { get; set; }
        public float ExistenciaCedis { get; set; }
        public string UbicacionCedis { get; set; }
        public string SurtidoPor { get; set; }
        public string ChecadoPor { get; set; }
        public int Multiplo { get; set; }
        public string SugerenciaSurtido { get; set; }
        public float? Diferencia { get => (this.CantidadPedida - this.CantidadSurtida); }

        public int ID_Tienda { get; set; }
        public int ID_Area { get; set; }
        public TipoPedido Tipo { get; set; }
        public string Folio { get; set; }
        public string SKU { get; set; }
        public float CantidadVendida { get; set; }
        public float PedidoEspecial { get; set; }
        public float StockMinimo { get; set; }
        public float StockMaximo { get; set; }
        public float ExistenciaFiltro { get; set; }
        public FormaDeCalculo FormaDeCalculo { get; set; }
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
    }
}
