using BodegaMovil.CoreBusiness;
using BodegaMovil.UseCases.DTO;
using MySql.Data.MySqlClient;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Eventing.Reader;

namespace BodegaMovil.WebAPI.EndPoints
{
    public static class UsuarioEndPoints
    {
        public static void MapUsuarioRoutes(this WebApplication app)
        {

            var group = app.MapGroup("/api/usuarios"); // Prefijo común

            group.MapPost("/", async (AccesoDTO acceso, MySqlConnection db) =>
            {
                try
                {
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

                    var result = db.Query<UsuarioDTO, Usuarios_Perfil, Usuarios_Tiendas, Usuarios_Areas, UsuarioDTO>(
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
                    acceso, // Parámetros de la consulta
                    splitOn: "PuedeSurtir, ID_Tienda, ID_Area" // Columnas para dividir los objetos
                    );

                    // Obtener el usuario del diccionario
                    var usuario = userDictionary.Values.FirstOrDefault();

                    if (usuario != null)
                        return Results.Ok(usuario);
                    
                    else
                        return Results.NotFound();

                    
                }
                catch (Exception e)
                {
                    throw e;
                }
            });

            
        }
    }
}
