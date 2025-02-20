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
    public class DepurarUseCase
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IMapa _mapa;

        public DepurarUseCase(IPedidoRepository pedidoRepository, IMapa map)
        {
            _pedidoRepository = pedidoRepository;
            _mapa = map;
        }   

        public async Task<bool> ExecuteAsync(PedidoDTO pedidoDTO)
        {
            //var pedido = _map.GetEntity<PedidoDTO,Pedido>(pedidoDTO);
            bool ok = false;

            List<PedidoDetalle> list = new List<PedidoDetalle>();
            foreach (var current in pedidoDTO.ArticulosPorSurtir)
            {
                if (current.Elegido)
                {
                   var linea = _mapa.GetEntity<PedidoDetalleDTO,PedidoDetalle>(current);
                    linea.CantidadSurtida = 0;
                    linea.Contenedor = 0;
                    list.Add(linea);
                }
            }
            if (list.Count > 0)
            {
                ok = await _pedidoRepository.SurtirVarios(pedidoDTO.Folio, list);
            }

            foreach(var item in list)
            {
                item.CantidadSurtida = new float?(0f);
            }

            return ok;
        }
    }
}
