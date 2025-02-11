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
    public class AddNewArtSurtirUseCase
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IMapa _mapa;

        public AddNewArtSurtirUseCase(IPedidoRepository pedidoRepository, IMapa mapa)
        {
            _pedidoRepository = pedidoRepository;
            _mapa = mapa;
        }

        public async Task<PedidoDetalleDTO> ExecuteAsync(PedidoDTO pedidoDTO, Articulo art, float? cantidad, FormaDeCalculo formaDeCalculo)
        {
            var pedido = _mapa.GetEntity<PedidoDTO, Pedido>(pedidoDTO);

            if (pedidoDTO.ID_AreaSurtir != art.ID_Area)
                throw new InvalidOperationException("El articulo no es de la misma area del pedido");

            pedido.AddDetalle(art, cantidad, formaDeCalculo.ToString());
            var ok = await _pedidoRepository.AgregarArticulo(pedido, art, cantidad, formaDeCalculo.ToString());

            if (ok)
            {
                var pd = new PedidoDetalleDTO
                {
                    ID_Tienda = pedido.ID_Tienda,
                    Consecutivo = pedido.Consecutivo,
                    ID_Area = pedidoDTO.ID_AreaSurtir,
                    SKU = art.SKU,
                    Descripcion = art.Descripcion,

                };
                return pd;
            }
            else
                return null;
            
        }
    }
}
