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

        public async Task<PedidoDTO> ExecuteAsync(int consecutivo, int id_tienda, int id_area)
        {
            return await _pedidoRepository.GetSurtirById(consecutivo, id_tienda, id_area);
        }
    }
}
