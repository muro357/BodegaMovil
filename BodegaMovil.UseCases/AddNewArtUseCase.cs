using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BodegaMovil.CoreBusiness;
using BodegaMovil.CoreBusiness.Enums;
using BodegaMovil.UseCases.DTO;
using BodegaMovil.UseCases.Interfaces;
using BodegaMovil.UseCases.Interfaces.Services;

namespace BodegaMovil.UseCases
{
    public class AddNewArtUseCase
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IMapa _mapa;

        public AddNewArtUseCase(IPedidoRepository pedidoRepository, IMapa mapa)
        {
            _pedidoRepository = pedidoRepository;
            _mapa = mapa;
        }

        public async Task<PedidoDetalleDTO> ExecutionAsync(PedidoDTO pedidoDTO, Articulo art, float? cantidad, FormaDeCalculo formaDeCalculo)
        {
            var pedido = _mapa.GetEntity<PedidoDTO, Pedido>(pedidoDTO);

            pedido.AddDetalle(art, cantidad, formaDeCalculo.ToString());
            return await _pedidoRepository.AgregarArticulo(art, cantidad, formaDeCalculo.ToString());
            
        }
    }
}
