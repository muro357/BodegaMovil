using BodegaMovil.UseCases.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BodegaMovil.CoreBusiness;
using BodegaMovil.UseCases.DTO;

namespace BodegaMovil.UseCases
{
    public class GetAreasHabilitadasUseCase
    {
        private readonly IAreaRepository _areaRepository;

        public GetAreasHabilitadasUseCase(IAreaRepository areaRepository)
        {
            _areaRepository = areaRepository;
        }

        public async Task<List<Area>> ExecuteAsync()
        {
            var res = await _areaRepository.GetAreasHabilitadas();
            
            return res;
        }
    }
}
