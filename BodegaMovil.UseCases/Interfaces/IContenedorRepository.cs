using BodegaMovil.CoreBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BodegaMovil.UseCases.Interfaces
{
    public interface IContenedorRepository
    {
        Task<int> Insert(Contenedor tara);
        Task<int> Update(Contenedor tara);
        Task<int> Delete(Contenedor tara);
        Task<bool> Existe(Contenedor tara);
        Task<bool> IsBusy(Contenedor tara);
        Task<List<Contenedor>> GetTaras(string folio);
    }
}
