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
    public class ContemplarExistenciaUseCase
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IMapa _mapa;

        public ContemplarExistenciaUseCase(IPedidoRepository pedidoRepository, IMapa mapa)
        {
            _pedidoRepository = pedidoRepository;
            _mapa = mapa;
        }

        public async Task<bool> ExecuteAsync(PedidoDTO pedidoDTO)
        {
            //var pedido = _mapa.GetEntity<PedidoDTO, Pedido>(pedidoDTO);
            bool ok = false;

            List<PedidoDetalle> list = new List<PedidoDetalle>();
            foreach (var current in pedidoDTO.PedidoDetalle)
            {
                if (current.ExistenciaCedis <= 0f)
                {
                    var item = _mapa.GetEntity<PedidoDetalleDTO, PedidoDetalle>(current);
                    item.CantidadSurtida = 0;
                    item.Contenedor = 0;
                    list.Add(item);
                }
            }
            if (list.Count > 0)
            {
                ok = await _pedidoRepository.SurtirVarios(pedidoDTO.Folio,list);
            }

            foreach (var item in list)
            {
                item.CantidadSurtida = new float?(0f);
            }

            return ok;
            //Task.CompletedTask;
        }
    }
}
