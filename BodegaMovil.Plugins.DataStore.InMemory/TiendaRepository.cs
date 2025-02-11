using BodegaMovil.CoreBusiness;
using BodegaMovil.UseCases.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BodegaMovil.Plugins.DataStore.InMemory
{
    public class TiendaRepository : ITiendaRepository
    {
        List<Tienda> _list;
        public TiendaRepository()
        {
            _list = new List<Tienda>
            {
                new Tienda
                {
                    ID_Tienda=1, Descripcion="TAcayucan"
                },
                new Tienda
                {
                    ID_Tienda=2, Descripcion="TSan Andres"
                },
                new Tienda
                {
                    ID_Tienda = 3, Descripcion="TMinatitlan"
                },
                
               
            };
        }
        public async Task<Tienda> GetById(int id)
        {
            return _list.FirstOrDefault(x => x.ID_Tienda == id);
        }

        public async Task<List<Tienda>> GetTiendasHabilitadas()
        {
            return _list;
        }
    }
}
