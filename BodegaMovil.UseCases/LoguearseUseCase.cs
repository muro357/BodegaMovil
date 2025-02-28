using BodegaMovil.CoreBusiness;
using BodegaMovil.UseCases.DTO;
using BodegaMovil.UseCases.Interfaces;
using BodegaMovil.UseCases.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BodegaMovil.UseCases
{
    public class LoguearseUseCase
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapa _map;

        public LoguearseUseCase(IUsuarioRepository usuarioRepository, IMapa map)
        {
            _usuarioRepository = usuarioRepository;
            _map = map;
        }

        public async Task<UsuarioDTO>ExecuteAsync(AccesoDTO login)
        {
            var log = _map.GetEntity<AccesoDTO,Usuario>(login); 

            return await _usuarioRepository.Autentificarse(log);
        }
    }
}
