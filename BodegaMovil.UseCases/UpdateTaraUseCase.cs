using BodegaMovil.UseCases.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BodegaMovil.CoreBusiness;

namespace BodegaMovil.UseCases
{
    public class UpdateTaraUseCase
    {
        private readonly IContenedorRepository taraRepository;

        public UpdateTaraUseCase(IContenedorRepository taraRepository)
        {
            this.taraRepository = taraRepository;
        }

        public async Task<int> ExecuteAsync(Contenedor tara)
        {
            return await this.taraRepository.Update(tara);
        }
    }
}
