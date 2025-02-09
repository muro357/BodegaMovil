using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BodegaMovil.CoreBusiness;

namespace BodegaMovil.UseCases.DTO
{
    public class UsuarioDTO
    {
        #region Datos Grales
        public string usuario { get; set; }
        public int ID_Perfil { get; set; }
        public string password { get; set; }
        public string Nombre { get; set; }
        public int ID_Empleado { get; set; }
        #endregion

        #region Permisos
        public bool PuedeSurtir { get; set; }
        public bool PuedeChecar { get; set; }
        public bool PuedeTransferir { get; set; }
        public bool PuedeCancelar { get; set; }
        #endregion

        #region Ambitos de Influencia
        public List<Tienda> ListaTiendasAsignadas { get; set; }
        public List<Usuarios_Areas> ListaAreasAsignadas { get; set; }
        #endregion
    }
}
