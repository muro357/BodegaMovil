using BodegaMovil.CoreBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BodegaMovil.UseCases.DTO;
using BodegaMovil.CoreBusiness.Enums;

namespace BodegaMovil.UseCases.QueriesObjects
{
    public class PedidoQuery
    {
        private IQueryable<PedidoDetalleDTO> _query;

        public PedidoQuery(IQueryable<PedidoDetalleDTO> query)
        {
            _query = query;
        }

        public PedidoQuery MostrarPorSurtir()
        {
            _query = (from item in _query
                    where item.CantidadPedida > 0f && !item.CantidadSurtida.HasValue
                    select item);

            return this;
        }

        public PedidoQuery MostrarSurtidos(bool mostrarSurtidosEnCeros)
        {
            if (mostrarSurtidosEnCeros)
            {
                _query = (from item in _query
                          where item.FormaDeCalculo == FormaDeCalculo.Articulo_Agregado || item.CantidadSurtida.HasValue
                          select item);
            }
            else
            {
                _query = (from item in _query
                          where (item.CantidadSurtida.HasValue && item.CantidadSurtida > 0) || 
                          item.FormaDeCalculo == FormaDeCalculo.Articulo_Agregado
                          select item);
            }

            return this;
        }

        public PedidoQuery OrderBy(OrdenarPor ordenarPor)
        {
            switch (ordenarPor)
            {
                case OrdenarPor.SKU:
                    _query = (from x in _query
                              orderby x.SKU
                              select x);
                    //return result;
                    break;
                case OrdenarPor.Descripcion:
                    _query = (from x in _query
                              orderby x.Descripcion
                              select x);
                    break;
                case OrdenarPor.Ubicacion:
                    _query = (from x in _query
                              orderby x.UbicacionCedis
                              select x);
                    break;
                case OrdenarPor.Cantidad_Pedida:
                    _query = (from x in _query
                              orderby x.CantidadPedida
                              select x);
                    break;
            }

            return this;
        }

        

        public PedidoQuery Filtrar(string filtro, FiltrarPor filtrarPor)
        {
            if (filtrarPor == FiltrarPor.SKU)
            {
                _query = (from item in _query
                          where item.SKU == filtro
                          select item);
            }
            else if (filtrarPor == FiltrarPor.Descripcion)
            {
                _query = (from item in _query
                          where item.Descripcion.Contains(filtro)
                          select item);
            }   

            return this;
        }

        public IQueryable<PedidoDetalleDTO> Build() => _query;
    }
}
