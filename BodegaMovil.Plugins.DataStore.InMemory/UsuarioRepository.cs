using BodegaMovil.CoreBusiness;
using BodegaMovil.UseCases.DTO;
using BodegaMovil.UseCases.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BodegaMovil.Plugins.DataStore.InMemory
{
    public class UsuarioRepository : IUsuarioRepository
    {

        List<Usuario> users;
        List<Usuarios_Areas> areas;
        List<Usuarios_Tiendas> tiendas;
        List<Usuarios_Perfil> perfils;
        public UsuarioRepository()
        {
            LoadAreas();
            LoadTiendas();
            LoadPerfils();
            LoadUsers();
        }

        public Task<UsuarioDTO> Autentificarse(Usuario login)
        {
            throw new NotImplementedException();
        }

        private void LoadUsers()
        {
            users = new List<Usuario>()
            {
                new Usuario()
                {
                    usuario = "s1",
                    password = "s1",
                    Nombre= "Surtidor Varios",

                },
                new Usuario()
                {
                    usuario = "s2",
                    password = "s2",
                    Nombre="Surtidor Plomeria"
                },
                new Usuario()
                {
                    usuario = "s3",
                    password = "s3",
                    Nombre="Surtidor Diamante"
                },
                new Usuario()
                {
                    usuario = "s4",
                    password = "s4",
                    Nombre = "Surtidor electrico"
                },
                new Usuario()
                {
                    usuario = "aduana",
                    password = "aduana",
                    Nombre = "Aduanero"
                },
                new Usuario()
                {
                    usuario = "admin",
                    password = "aduana",
                    Nombre = "Aduanero"
                },
            };
        }
        private void LoadAreas() 
        {
            areas = new List<Usuarios_Areas>()
                {
                    new Usuarios_Areas { ID_Area = 10, usuario = "s1" },
                    new Usuarios_Areas { ID_Area = 11, usuario = "s2" },
                    new Usuarios_Areas { ID_Area = 14, usuario = "s3" },
                    new Usuarios_Areas { ID_Area = 19, usuario = "s4" },

                    new Usuarios_Areas { ID_Area = 10, usuario = "aduana" },
                    new Usuarios_Areas { ID_Area = 11, usuario = "aduana" },
                    new Usuarios_Areas { ID_Area = 14, usuario = "aduana" },
                    new Usuarios_Areas { ID_Area = 19, usuario = "aduana" },

                    new Usuarios_Areas { ID_Area = 10, usuario = "admin" },
                    new Usuarios_Areas { ID_Area = 11, usuario = "admin" },
                    new Usuarios_Areas { ID_Area = 14, usuario = "admin" },
                    new Usuarios_Areas { ID_Area = 19, usuario = "admin" }
                };
        }

        private void LoadTiendas() 
        {
            tiendas = new List<Usuarios_Tiendas>
            {
                new Usuarios_Tiendas { ID_Tienda= 1, usuario="s1"},
                new Usuarios_Tiendas { ID_Tienda= 2, usuario="s1"},
                new Usuarios_Tiendas { ID_Tienda= 3, usuario="s1"},

                new Usuarios_Tiendas { ID_Tienda= 1, usuario="s2"},
                new Usuarios_Tiendas { ID_Tienda= 2, usuario="s2"},
                new Usuarios_Tiendas { ID_Tienda= 3, usuario="s2"},

                new Usuarios_Tiendas { ID_Tienda= 1, usuario="s3"},
                new Usuarios_Tiendas { ID_Tienda= 2, usuario="s3"},
                new Usuarios_Tiendas { ID_Tienda= 3, usuario="s3"},

                new Usuarios_Tiendas { ID_Tienda= 1, usuario="s4"},
                new Usuarios_Tiendas { ID_Tienda= 2, usuario="s4"},
                new Usuarios_Tiendas { ID_Tienda= 3, usuario="s4"},

                new Usuarios_Tiendas { ID_Tienda= 1, usuario="aduana"},
                new Usuarios_Tiendas { ID_Tienda= 2, usuario="aduana"},
                new Usuarios_Tiendas { ID_Tienda= 3, usuario="aduana"},

                new Usuarios_Tiendas { ID_Tienda= 1, usuario="admin"},
                new Usuarios_Tiendas { ID_Tienda= 2, usuario="admin"},
                new Usuarios_Tiendas { ID_Tienda= 3, usuario="admin"}
            };
        }
        private void LoadPerfils() 
        {
            perfils = new List<Usuarios_Perfil>
            { 
                new Usuarios_Perfil 
                { 
                    ID_Perfil= 1, 
                    Descripcion="Surtidor",
                    PuedeSurtir=true,
                    PuedeChecar=false,
                    PuedeTransferir=false,
                    PuedeCancelar=false,
                },
                new Usuarios_Perfil
                {
                    ID_Perfil= 2,
                    Descripcion="Checador",
                    PuedeSurtir=false,
                    PuedeChecar=true,
                    PuedeTransferir=false,
                    PuedeCancelar=false,
                },
                new Usuarios_Perfil
                {
                    ID_Perfil= 3,
                    Descripcion="Administrador",
                    PuedeSurtir=true,
                    PuedeChecar=true,
                    PuedeTransferir=true,
                    PuedeCancelar=true,
                }
            };
        }

    }
}
