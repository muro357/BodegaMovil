using BodegaMovil.CoreBusiness;
using BodegaMovil.UseCases.DTO;
using BodegaMovil.UseCases.Interfaces;
using BodegaMovil.UseCases.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BodegaMovil.UseCases
{
    public class SurtirUseCase
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IMapa _mapa;

        public SurtirUseCase(IPedidoRepository pedidoRepository, IMapa mapa)
        {
            _pedidoRepository = pedidoRepository;
            _mapa = mapa;
        }

        public async Task Surtir(PedidoDTO pedidoDTO, PedidoDetalleDTO pedidoDetalleDTO)
        {
            var pedidoDetalle = _mapa.GetEntity<PedidoDetalleDTO, PedidoDetalle>(pedidoDetalleDTO);
            var ok = await _pedidoRepository.Surtir(pedidoDetalle);
            if (ok)
            {
                var pd = pedidoDTO.PedidoDetalle.Where(x => x.SKU == pedidoDetalleDTO.SKU).FirstOrDefault();
                pd.CantidadSurtida = pedidoDetalleDTO.CantidadSurtida;
                pd.Contenedor = pedidoDetalleDTO.Contenedor;
            }
            else
                throw new InvalidOperationException("No se pudo actualizar");
        }
    }
}
