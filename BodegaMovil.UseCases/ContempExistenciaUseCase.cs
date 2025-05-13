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
            bool ok = false;

            List<PedidoDetalle> list = new List<PedidoDetalle>();
            foreach (var current in pedidoDTO.PedidoDetalle)
            {
                if (current.ExistenciaCedis <= 0f)
                {
                    current.CantidadSurtida = 0;
                    current.Contenedor = 0;
                    current.SurtidoPor = pedidoDTO.SurtidoPor;
                    var item = _mapa.GetEntity<PedidoDetalleDTO, PedidoDetalle>(current);
                    list.Add(item);
                }
            }
            if (list.Count > 0)
            {
                ok = await _pedidoRepository.SurtirVarios(pedidoDTO.Folio,list);
            }
            
            return ok;
        }
    }
}
