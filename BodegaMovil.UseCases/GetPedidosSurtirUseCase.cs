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

        public async Task<List<ShowPedidoDTO>> ExecuteAsync(UsuarioDTO usuarioDTO, int id_tienda, int id_area)
        {
            IEnumerable<int> lstTiendas = new List<int>();
            IEnumerable<int> lstAreas = new List<int>();

            if (id_tienda == 0){
                lstTiendas = (from tiendas in usuarioDTO.ListaTiendasAsignadas select tiendas.ID_Tienda).ToList();
            }
            else{
                lstTiendas = new List<int>() { id_tienda };
            }

            if(id_area == 0){
                lstAreas = (from areas in usuarioDTO.ListaAreasAsignadas select areas.ID_Area).ToList();
            }
            else{
                lstAreas = new List<int> { id_area };
            }


            var res = await _pedidoRepository.GetPedidosSurtir(lstTiendas, lstAreas);
            return res;
        }
    }
}
