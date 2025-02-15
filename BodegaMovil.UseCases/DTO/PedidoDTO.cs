using BodegaMovil.CoreBusiness.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BodegaMovil.UseCases.DTO
{
    public class PedidoDTO
    {
        public int Consecutivo { get; set; }
        public int ID_Tienda { get; set; }
        public int ID_Area { get; set; }
        public int ID_AreaSurtir { get; set; }  
        public TipoPedido Tipo { get; set; } //Puede ser Sugerido o Especial
        public string DescripcionTienda { get; set; }
        public string DescripcionArea { get; set; }
        //public string EntregarA { get; set; }
        //public string DomicilioEntregar { get; set; }
        //public string Usuario { get; set; }
        public string Folio { get; set; }
        //public DateTime FechaGenerado { get; set; }
        //public DateTime? FechaInicialFiltro { get; set; }
        //public DateTime? FechaFinalFiltro { get; set; }
        //public float PorcentajeStockMaximo { get; set; }
        //public bool SoloVentas { get; set; }
        public DateTime? FechaSolicitado { get; set; }
        public DateTime? FechaTransferido { get; set; }
        public string SurtidoPor { get; set; }
        public string ChecadoPor { get; set; }
        public string ObservacionesAduana { get; set; }
        public string Placas { get; set; }
        public string NumeroDeCandado { get; set; }
        //public string Chofer { get; set; }
        public EstadoDePedido Estado { get; set; }//(Generado, Solicitado, Surtido, Checado, Embarcado, Finalizado) Generado = Se esta llenando en tienda el pedido, Solicitado = ya se envio pedido a bodega, Surtido = ya se finalizo el surtido, Checado = ya se checo todo, Finalizado = ya se hizo el movimiento de almacen
        //public string RFC_Cliente { get; set; }
        //public EnviarPedidoA EnviarPedidoA { get; set; }

        public string CanceladoPor { get; set; }

        public List<PedidoDetalleDTO> PedidoDetalle 
        { 
            get { return _detalle; }
            set { _detalle = value; } 
        }

        public IEnumerable<PedidoDetalleDTO> ArticulosPorSurtir 
        { 
            get
            {
                var list = (from item in _detalle
                                               where item.CantidadPedida > 0f && !item.CantidadSurtida.HasValue
                                               select item).ToList();
                
                List<PedidoDetalleDTO> result = null;
               switch(_ordenar)
                {
                    case OrdenarPor.SKU:
                        result = (from x in list
                                  orderby x.SKU
                                  select x).ToList();
                        //return result;
                        break;
                    case OrdenarPor.Descripcion:
                        result = (from x in list
                                  orderby x.Descripcion
                                  select x).ToList();
                        break;
                    case OrdenarPor.Ubicacion:
                        result = (from x in list
                                  orderby x.UbicacionCedis
                                  select x).ToList();
                        break;
                    case OrdenarPor.Cantidad_Pedida:
                        result = (from x in list
                                  orderby x.CantidadPedida
                                  select x).ToList();
                        break;
                }
                return result;
            }
        
        }

        public IEnumerable<PedidoDetalleDTO> ArticulosSurtidos
        {
            get
            {
                List<PedidoDetalleDTO> list;
                if (!_mostrarSurtidosEnCeros)
                {
                    list = _detalle.Where(delegate (PedidoDetalleDTO item)
                    {
                        bool arg_4B_0;
                        if ((item.CantidadPedida > 0f || item.FormaDeCalculo == FormaDeCalculo.Articulo_Agregado) && item.CantidadSurtida.HasValue)
                        {
                            float? cantidadSurtida = item.CantidadSurtida;
                            arg_4B_0 = (cantidadSurtida.GetValueOrDefault() > 0f && cantidadSurtida.HasValue);
                        }
                        else
                        {
                            arg_4B_0 = false;
                        }
                        return arg_4B_0;
                    }).ToList();
                }
                else
                {
                    list = (from item in _detalle
                            where (item.CantidadPedida > 0f || item.FormaDeCalculo == FormaDeCalculo.Articulo_Agregado) && item.CantidadSurtida.HasValue
                            select item).ToList();
                }
                
                List<PedidoDetalleDTO> result = null;

                switch(_ordenar)
                {
                    case OrdenarPor.SKU:
                        result = (from x in list
                                  orderby x.SKU
                                  select x).ToList();
                        break;

                    case OrdenarPor.Descripcion:
                        result = (from x in list
                                  orderby x.Descripcion
                                  select x).ToList();
                        break;
                    
                    case OrdenarPor.Ubicacion:
                        result = (from x in list
                                  orderby x.UbicacionCedis
                                  select x).ToList();
                        break;

                    case OrdenarPor.Cantidad_Pedida:
                        result = (from x in list
                                  orderby x.CantidadPedida
                                  select x).ToList();
                        break;
                }

                
             
                return result;
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


        private List<PedidoDetalleDTO> _detalle;
        private OrdenarPor _ordenar;
        private bool _mostrarSurtidosEnCeros;
        private FiltrarPor _filtrarPor;
    }
}
