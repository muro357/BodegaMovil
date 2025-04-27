using BodegaMovil.CoreBusiness;
using BodegaMovil.UseCases.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BodegaMovil.UseCases
{
    public class ExistTaraUseCase
    {
        private readonly IContenedorRepository taraRepository;

        public ExistTaraUseCase(IContenedorRepository taraRepository)
        {
            this.taraRepository = taraRepository;
        }

        public async Task<bool> ExecuteAsync(Contenedor tara)
        {
            return await this.taraRepository.Existe(tara);
        }
    }
}
