using BodegaMovil.CoreBusiness;
using BodegaMovil.UseCases.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BodegaMovil.UseCases
{
    public class GetTiendasHabilitadasUseCase
    {
        private readonly ITiendaRepository _tiendaRepository;

        public GetTiendasHabilitadasUseCase(ITiendaRepository tiendaRepository)
        {
            _tiendaRepository = tiendaRepository;
        }

        public async Task<List<Tienda>> ExecuteAsync()
        {
            return await _tiendaRepository.GetTiendasHabilitadas();
        }
    }
}
