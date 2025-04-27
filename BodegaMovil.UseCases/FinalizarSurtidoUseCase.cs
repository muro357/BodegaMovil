using BodegaMovil.CoreBusiness;
using BodegaMovil.UseCases.DTO;
using BodegaMovil.UseCases.Interfaces;
using BodegaMovil.UseCases.Interfaces.Services;

namespace BodegaMovil.UseCases
{
   
    public class FinalizarSurtidoUseCase
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IMapa _mapa;

        public FinalizarSurtidoUseCase(IPedidoRepository pedidoRepository, IMapa map)
        {
            _pedidoRepository = pedidoRepository;
            _mapa = map;
        }

        public async Task<bool> ExecuteAsync(PedidoDTO pedidoDTO)
        {
            var pedido = _mapa.GetEntity<PedidoDTO,Pedido>(pedidoDTO);
            
            var ok = await _pedidoRepository.Finalizar(pedido);
            
            return ok;
        }
    }
}
