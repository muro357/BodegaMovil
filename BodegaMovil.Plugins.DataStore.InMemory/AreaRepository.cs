
using BodegaMovil.CoreBusiness;
using BodegaMovil.UseCases.Interfaces;

namespace BodegaMovil.Plugins.DataStore.InMemory
{
    // All the code in this file is included in all platforms.
    public class AreaRepository : IAreaRepository
    {
        List<Area> _areas;

        public AreaRepository()
        {
            _areas = new List<Area>()
                {
                    new Area
                    {
                        ID_Area= 10,
                        Descripcion="Articulos Varios"
                    },
                    new Area
                    {
                        ID_Area = 11,
                        Descripcion = "Plomeria"
                    },
                    new Area
                    {
                        ID_Area = 14,
                        Descripcion = "Diamante"
                    },
                    new Area
                    {
                        ID_Area = 19,
                        Descripcion = "Material electrico"
                    }
                }; 
        }
        public async Task<List<Area>> GetAreasHabilitadas()
        {
            return _areas;
        }

        public async Task<Area> GetById(int id)
        {
            return _areas.FirstOrDefault(x => x.ID_Area == id);
        }

        private void prueba()
        {
            _areas.FirstOrDefault(x => x.ID_Area == 1);
        }
    }
}
