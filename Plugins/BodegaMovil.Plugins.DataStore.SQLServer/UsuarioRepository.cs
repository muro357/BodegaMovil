using Dapper;
using BodegaMovil.CoreBusiness;
using BodegaMovil.UseCases.DTO;
using BodegaMovil.UseCases.Interfaces;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BodegaMovil.UseCases.Interfaces.Services;

namespace BodegaMovil.Plugins.DataStore.MySQL
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private MySqlConnection db;
        private readonly ISetting _settings;

        public UsuarioRepository(ISetting settings)
        {
            _settings = settings;
            
        }

        private async Task<MySqlConnection> GetConnection()
        {
            var cadena = await _settings.GetConnectionAsync();
            return new MySqlConnection(cadena);
        }
        public async Task<UsuarioDTO> Autentificarse(Usuario login)
        {
            try
            {
                db = await GetConnection();
                await db.OpenAsync();
                var userDictionary = new Dictionary<string, UsuarioDTO>();

                var query = @"SELECT u.usuario, 
                                         u.password, 
                                         u.Nombre,
                                         u.ID_Perfil,
                                         p.PuedeSurtir,
                                         p.PuedeChecar,
                                         p.PuedeTransferir,
                                         p.PuedeCancelar,
                                         ut.ID_Tienda,
                                          t.Descripcion as DescripcionTienda,
                                         ut.usuario,
                                         ut.FechaUltimaModificacion,
                                         ua.ID_Area,
                                          a.Descripcion as DescripcionArea,
                                         ua.usuario,
                                         ua.FechaUltimaModificacion
                                FROM usuarios u JOIN usuarios_perfil p ON u.ID_Perfil = p.ID_Perfil 
                                LEFT JOIN usuarios_tiendas ut ON u.usuario = ut.usuario 
                                LEFT JOIN usuarios_areas ua ON u.usuario = ua.usuario 
                                LEFT JOIN tiendas t ON t.ID_Tienda = ut.ID_Tienda 
                                LEFT JOIN areas a ON a.ID_Area = ua.ID_Area
                                WHERE u.usuario = @usuario AND u.password = @password";

                var result = await db.QueryAsync<UsuarioDTO, Usuarios_Perfil, Usuarios_Tiendas, Usuarios_Areas, UsuarioDTO>(
                query,
                (usuario, perfil, tienda, area) =>
                {
                    // Verificar si el usuario ya está en el diccionario
                    if (!userDictionary.TryGetValue(usuario.usuario, out var usuarioEntry))
                    {
                        // Si no está, crear un nuevo usuario y agregarlo al diccionario
                        usuarioEntry = usuario;
                        //usuarioEntry.ID_Perfil = perfil.ID_Perfil;
                        usuario.PuedeSurtir = perfil.PuedeSurtir;
                        usuario.PuedeChecar = perfil.PuedeChecar;
                        usuario.PuedeTransferir = perfil.PuedeTransferir;
                        usuario.PuedeCancelar = perfil.PuedeCancelar;
                        usuarioEntry.ListaTiendasAsignadas = new List<Usuarios_Tiendas>();
                        usuarioEntry.ListaAreasAsignadas = new List<Usuarios_Areas>();
                        userDictionary.Add(usuario.usuario, usuarioEntry);
                    }

                    // Agregar la tienda si no existe
                    if (tienda != null && !usuarioEntry.ListaTiendasAsignadas.Any(t => t.ID_Tienda == tienda.ID_Tienda))
                    {
                        usuarioEntry.ListaTiendasAsignadas.Add(tienda);
                    }

                    // Agregar el área si no existe
                    if (area != null && !usuarioEntry.ListaAreasAsignadas.Any(a => a.ID_Area == area.ID_Area))
                    {
                        usuarioEntry.ListaAreasAsignadas.Add(area);
                    }

                    return usuarioEntry;
                },
                login, // Parámetros de la consulta
                splitOn: "PuedeSurtir, ID_Tienda, ID_Area" // Columnas para dividir los objetos
                );

                // Obtener el usuario del diccionario
                var usuario = userDictionary.Values.FirstOrDefault();

                if(usuario != null)
                {
                    
                    return usuario;
                }
                else
                {
                    return null;
                }

            }
            catch (MySqlException e)
            {
                throw e;
            }
            finally
            {
                if (db != null && db.State == System.Data.ConnectionState.Open)
                {
                    await db.CloseAsync();
                }
            }
        }
    }
}
