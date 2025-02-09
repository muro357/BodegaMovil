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
        public Task<PedidoDTO> GetById(int id, int id_tienda);

        public Task<List<PedidoDTO>> GetPedidos(IEnumerable<int> ID_Tienda, IEnumerable<int> ID_Area);

        public Task<bool> Surtir(PedidoDetalle linea);

        public Task<PedidoDetalleDTO> AgregarArticulo(Articulo art, float? cantidad, string formaCalc);

        public Task ContemplarExistencia(IEnumerable<PedidoDetalle> pedidoDetalles);

        public Task Depurar(IEnumerable<PedidoDetalle> pedidoDetalles);
    }


}
