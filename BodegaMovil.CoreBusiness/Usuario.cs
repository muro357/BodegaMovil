using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BodegaMovil.CoreBusiness
{
    public class Usuario
    {
        public string usuario { get; set; } = string.Empty;
        public int ID_Perfil { get; set; } 
        public string password { get; set; } = string.Empty;    
        public string Nombre { get; set; } = string.Empty;

        
    }
}
