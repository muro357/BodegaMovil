using BodegaMovil.CoreBusiness;
using BodegaMovil.UseCases.DTO;
using MySql.Data.MySqlClient;
using Dapper;
using Microsoft.AspNetCore.Mvc;

namespace BodegaMovil.WebAPI.EndPoints
{
    public static class AreaEndPoints
    {
        public static void MapAreaRoutes(this WebApplication app)
        {

            var group = app.MapGroup("/api/areas"); // Prefijo común

            group.MapGet("/{id}", async (int id, MySqlConnection db) =>
            {
                try
                {
                    var query = "SELECT * FROM Areas WHERE ID_Area = @id";

                    var entidad = db.QueryFirst<Area>(query, new { id = id });
                    return Results.Ok(entidad);
                }
                catch (Exception e)
                {
                    throw e;
                }
            });

            group.MapGet("/", async (MySqlConnection db) =>
            {
                List<Area> lista;

                var query = "SELECT * FROM Areas WHERE Habilitada = 1";
                lista = db.Query<Area>(query).ToList();
                return Results.Ok(lista);
            });
        }
    }
}
