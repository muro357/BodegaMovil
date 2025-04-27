using BodegaMovil.CoreBusiness;
using BodegaMovil.UseCases.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BodegaMovil.UseCases
{
    public class IsBusyTaraUseCase
    {
        private readonly IContenedorRepository taraRepository;

        public IsBusyTaraUseCase(IContenedorRepository taraRepository)
        {
            this.taraRepository = taraRepository;
        }

        public async Task<bool> ExecuteAsync(Contenedor tara)
        {
            return await taraRepository.IsBusy(tara);
        }
    }
}
