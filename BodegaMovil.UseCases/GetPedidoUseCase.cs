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
    public class GetPedidoUseCase
    {
        private readonly IPedidoRepository _pedidoRepository;

        public GetPedidoUseCase(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        public async Task<PedidoDTO> ExecuteAsync(int consecutivo, int id_tienda)
        {
            return await _pedidoRepository.GetById(consecutivo, id_tienda);
        }
    }
}
