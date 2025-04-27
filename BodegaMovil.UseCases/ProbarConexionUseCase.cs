using BodegaMovil.UseCases.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BodegaMovil.UseCases
{
    public class ProbarConexionUseCase
    {
        private readonly IConexion _conexion;
        private string _datos;
        public ProbarConexionUseCase(IConexion conexion)
        {
            _conexion = conexion;
        }
        


        public async Task<bool> ExecuteAsync(string datos)
        {
            var res = await _conexion.ProbarConexion(datos);
            return res;
        }

       
    }
}
