using BodegaMovil.CoreBusiness;
using BodegaMovil.UseCases.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BodegaMovil.UseCases
{
    public class GetArticulosUseCase
    {
        IArticuloRepository _repository;
        public GetArticulosUseCase(IArticuloRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Articulo>> ExecuteAsync(string filter)
        {
            var list = await _repository.GetArticulos(filter);
            return list;
        }
    }
}
