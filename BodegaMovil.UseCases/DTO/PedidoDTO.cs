using BodegaMovil.CoreBusiness.Enums;
using BodegaMovil.UseCases.QueriesObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BodegaMovil.UseCases.DTO
{
    public class PedidoDTO
    {
        private List<PedidoDetalleDTO> _detalle;
        private OrdenarPor _ordenar;
        private bool _mostrarSurtidosEnCeros;
        private FiltrarPor _filtrarPor;
        private PedidoQuery _pedidoQuery=null;

        public PedidoDTO()
        {
            _ordenar = OrdenarPor.Ubicacion;
            _mostrarSurtidosEnCeros = false;
            _filtrarPor = FiltrarPor.SKU;

            _detalle = new List<PedidoDetalleDTO>();
        }

        public int Consecutivo { get; set; }
        public int ID_Tienda { get; set; }
        public int ID_Area { get; set; }
        public int ID_AreaSurtir { get; set; }  
        public TipoPedido Tipo { get; set; } //Puede ser Sugerido o Especial
        public string DescripcionTienda { get; set; }
        public string DescripcionArea { get; set; }
        public string Folio { get; set; }
        public DateTime? FechaSolicitado { get; set; }
        public DateTime? FechaTransferido { get; set; }
        public string SurtidoPor { get; set; }
        public string ChecadoPor { get; set; }
        public string ObservacionesAduana { get; set; }
        public string Placas { get; set; }
        public string NumeroDeCandado { get; set; }
        //public string Chofer { get; set; }
        public EstadoDePedido Estado { get; set; }//(Generado, Solicitado, Surtido, Checado, Embarcado, Finalizado) Generado = Se esta llenando en tienda el pedido, Solicitado = ya se envio pedido a bodega, Surtido = ya se finalizo el surtido, Checado = ya se checo todo, Finalizado = ya se hizo el movimiento de almacen

        public string CanceladoPor { get; set; }

        public List<PedidoDetalleDTO> PedidoDetalle 
        { 
            get { return _detalle; }
            set 
            { 
                _detalle = value; 
            } 
        }

        public IEnumerable<PedidoDetalleDTO> ArticulosPorSurtir 
        { 
            get
            {
                _pedidoQuery = new PedidoQuery(_detalle.AsQueryable());

                _pedidoQuery.MostrarPorSurtir()
                            .OrderBy(_ordenar);

                return _pedidoQuery.Build().ToList();
            }
        
        }

        public IEnumerable<PedidoDetalleDTO> ArticulosSurtidos
        {
            get
            {
                _pedidoQuery = new PedidoQuery(_detalle.AsQueryable());

                _pedidoQuery.MostrarSurtidos(_mostrarSurtidosEnCeros)
                            .OrderBy(_ordenar);

                return _pedidoQuery.Build().ToList();
                
            }
        }

        #region Filtros
        public OrdenarPor Ordenar
        {
            set
            {
                _ordenar = value;
            }
        }

        public bool MostrarSurtidosEnCeros
        {
            set
            {
                _mostrarSurtidosEnCeros = value;
            }
        }

        public FiltrarPor Filtrar
        {
            set 
            { 
                _filtrarPor = value;
            }
        }

        #endregion


        
    }
}
