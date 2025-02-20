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
    public class GetArticuloUseCase
    {
        IArticuloRepository _repository;
        

        public GetArticuloUseCase(IArticuloRepository repository)
        {
            _repository = repository;
        }

        public async Task<ArticuloDTO> ExecuteAsync(string sku, int id_tienda)
        {
            var art = await _repository.GetArticulo(sku, id_tienda);
            return art;
        }
    }
}
