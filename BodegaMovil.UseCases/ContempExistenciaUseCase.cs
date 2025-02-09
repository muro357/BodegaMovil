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
    public class ContempExistenciaUseCase
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IMapa _mapa;

        public ContempExistenciaUseCase(IPedidoRepository pedidoRepository, IMapa mapa)
        {
            _pedidoRepository = pedidoRepository;
            _mapa = mapa;
        }

        public async Task ExecuteAsync(PedidoDTO pedidoDTO)
        {
            //var pedido = _mapa.GetEntity<PedidoDTO, Pedido>(pedidoDTO);

            List<PedidoDetalle> list = new List<PedidoDetalle>();
            foreach (var current in pedidoDTO.PedidoDetalle)
            {
                if (current.ExistenciaCedis <= 0f)
                {
                    var item = _mapa.GetEntity<PedidoDetalleDTO, PedidoDetalle>(current);
                    list.Add(item);
                }
            }
            if (list.Count > 0)
            {
                await _pedidoRepository.ContemplarExistencia(list);
            }

            foreach (var item in list)
            {
                item.CantidadSurtida = new float?(0f);
            }

            //Task.CompletedTask;
        }
    }
}
