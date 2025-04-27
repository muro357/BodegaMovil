using BodegaMovil.UseCases.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BodegaMovil.CoreBusiness;

namespace BodegaMovil.UseCases
{
    public class DeleteTaraUseCase
    {
        private readonly IContenedorRepository taraRepository;

        public DeleteTaraUseCase(IContenedorRepository taraRepository)
        {
            this.taraRepository = taraRepository;
        }

        public async Task<int> ExecuteAsync(Contenedor tara)
        {
            return await this.taraRepository.Delete(tara);
        }
    }
}
