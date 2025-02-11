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
    public class GetPedidosSurtirUseCase
    {
        private readonly IPedidoRepository _pedidoRepository;

        public GetPedidosSurtirUseCase(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        public async Task<List<PedidoDTO>> ExecuteAsync(IEnumerable<int> lstTiendas, IEnumerable<int> lstAreas)
        {
            var res = await _pedidoRepository.GetPedidosSurtir(lstTiendas, lstAreas);
            return res;
        }
    }
}
