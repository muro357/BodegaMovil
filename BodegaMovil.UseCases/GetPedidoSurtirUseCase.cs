using BodegaMovil.CoreBusiness;
using BodegaMovil.UseCases.DTO;
using BodegaMovil.UseCases.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BodegaMovil.UseCases
{
    public class GetPedidoSurtirUseCase
    {
        private readonly IPedidoRepository _pedidoRepository;

        public GetPedidoSurtirUseCase(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        public async Task<PedidoDTO> ExecuteAsync(int id_tienda, string folio)
        {
            return await _pedidoRepository.GetSurtirById(id_tienda, folio);
        }
    }
}
