using BodegaMovil.CoreBusiness;
using BodegaMovil.UseCases.DTO;
using MySql.Data.MySqlClient;
using Dapper;
using Microsoft.AspNetCore.Mvc;

namespace BodegaMovil.WebAPI.EndPoints
{
    public static class TiendaEndPoints
    {
        public static void MapTiendaRoutes(this WebApplication app)
        {

            var group = app.MapGroup("/api/tiendas"); // Prefijo común

            group.MapGet("/{id}", async (int id, MySqlConnection db) =>
            {
                try
                {
                    var query = "SELECT * FROM Tiendas WHERE ID_Tienda = @id";

                    var entidad = db.QueryFirst<Tienda>(query, new { id = id });
                    return Results.Ok(entidad);
                }
                catch (Exception e)
                {
                    throw e;
                }
            });

            group.MapGet("/", async (MySqlConnection db) =>
            {
                List<Tienda> lista;

                var query = "SELECT * FROM Tiendas WHERE Habilitada = 1";
                lista = db.Query<Tienda>(query).ToList();
                return Results.Ok(lista);
            });
        }
    }
}
