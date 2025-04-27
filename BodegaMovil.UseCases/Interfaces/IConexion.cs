using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BodegaMovil.UseCases.Interfaces
{
    public  interface IConexion
    {
        Task<bool> ProbarConexion(string datos);
       
    }
}
