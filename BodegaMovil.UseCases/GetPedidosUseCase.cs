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
    public class GetPedidosUseCase
    {
        private readonly IPedidoRepository _pedidoRepository;

        public GetPedidosUseCase(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        public async Task<List<PedidoDTO>> ExecuteAsync(IEnumerable<int> lstTiendas, IEnumerable<int> lstAreas)
        {
            var res = await _pedidoRepository.GetPedidos(lstTiendas, lstAreas);
            return res;
        }
    }
}
