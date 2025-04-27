using BodegaMovil.UseCases.Interfaces;
using BodegaMovil.CoreBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BodegaMovil.UseCases
{
    public class GetTarasUseCase
    {
        private readonly IContenedorRepository taraRepository;

        public GetTarasUseCase(IContenedorRepository taraRepository)
        {
            this.taraRepository = taraRepository;
        }

        public async Task<List<Contenedor>> ExecuteAsync(string folio)
        {
            return await taraRepository.GetTaras(folio);
        }
    }
}
