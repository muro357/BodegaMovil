using BodegaMovil.CoreBusiness;
using BodegaMovil.CoreBusiness.Enums;
using BodegaMovil.UseCases.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BodegaMovil.UseCases.Interfaces
{
    public interface IPedidoRepository
    {
        public Task<PedidoDTO> GetSurtirById(int id_tienda, string folio, int id_area_surtir);

        public Task<List<ShowPedidoDTO>> GetPedidosSurtir(IEnumerable<int> ID_Tienda, IEnumerable<int> ID_Area);

        public Task<bool> Surtir(PedidoDetalle linea);

        public Task<bool> SurtirVarios(string folio, List<PedidoDetalle> lineas);

        public Task<bool> AgregarArticulo(PedidoDetalle linea);

        public Task<bool> Finalizar(Pedido pedido);

        //public Task ContemplarExistencia(IEnumerable<PedidoDetalle> pedidoDetalles);

        //public Task Depurar(string folio, IEnumerable<PedidoDetalle> pedidoDetalles);
    }


}
