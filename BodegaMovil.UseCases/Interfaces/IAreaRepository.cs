using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BodegaMovil.UseCases.Interfaces;
using BodegaMovil.CoreBusiness;

namespace BodegaMovil.UseCases.Interfaces
{
    public interface IAreaRepository
    {
        Task<Area> GetById(int id);

        Task<List<Area>> GetAreasHabilitadas();

    }
}
