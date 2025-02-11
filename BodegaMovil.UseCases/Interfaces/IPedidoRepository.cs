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
        public Task<PedidoDTO> GetSurtirById(int id, int id_tienda, int id_area);

        public Task<List<PedidoDTO>> GetPedidosSurtir(IEnumerable<int> ID_Tienda, IEnumerable<int> ID_Area);

        public Task<bool> Surtir(PedidoDetalle linea);

        public Task<bool> AgregarArticulo(Pedido pedido, Articulo art, float? cantidad, string formaCalc);

        public Task ContemplarExistencia(IEnumerable<PedidoDetalle> pedidoDetalles);

        public Task Depurar(IEnumerable<PedidoDetalle> pedidoDetalles);
    }


}
