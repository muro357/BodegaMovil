﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BodegaMovil.CoreBusiness
{
    public class Usuarios_Perfil
    {
        public int ID_Perfil { get; set; }
        public string Descripcion { get; set; }
        public bool PuedeSurtir { get; set; }
        public bool PuedeChecar { get; set; }
        public bool PuedeTransferir { get; set; }
        public bool PuedeCancelar { get; set; }
        public DateTime? FechaUltimaModificacion { get; set; }
    }
}
