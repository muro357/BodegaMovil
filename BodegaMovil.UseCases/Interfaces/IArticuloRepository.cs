using BodegaMovil.CoreBusiness;
using BodegaMovil.UseCases.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BodegaMovil.UseCases.Interfaces
{
    public interface IArticuloRepository
    {

        Task<ArticuloDTO> GetArticulo(string sku, int id_tienda);

        Task<List<Articulo>> GetArticulos(string filtro);

    }
}
