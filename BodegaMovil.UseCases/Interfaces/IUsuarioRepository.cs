using BodegaMovil.CoreBusiness;
using BodegaMovil.UseCases.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BodegaMovil.UseCases.Interfaces
{
    public interface IUsuarioRepository
    {
        public Task<UsuarioDTO> Autentificarse(Usuario login);
    }
}
