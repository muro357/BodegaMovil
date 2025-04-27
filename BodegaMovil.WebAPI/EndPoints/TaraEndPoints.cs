using MySql.Data.MySqlClient;
using BodegaMovil.CoreBusiness;
using Dapper;
using Microsoft.AspNetCore.Mvc;

namespace BodegaMovil.WebAPI.EndPoints
{
    public static class ContenedorEndPoints
    {
        public static void MapTaraRoutes(this WebApplication app)
        {

            var group = app.MapGroup("/api/taras"); // Prefijo común

            group.MapPost("/", async (Contenedor tara, MySqlConnection db) =>
            {
                var query = "INSERT INTO taras VALUES(@Folio, @NumTara, @Estado)";
                var rows = db.ExecuteAsync(query,tara);
                return Results.Ok(rows);
            });

            group.MapPut("/", async (Contenedor tara, MySqlConnection db) =>
            {
                var query = "UPDATE taras SET Estado = @Estado WHERE Folio = @Folio AND Tara = @Tara";
                var rows = db.ExecuteAsync(query,tara);
                return Results.Ok(rows);
            });

            group.MapDelete("/", async ([FromBody]Contenedor tara, MySqlConnection db) =>
            {
                var query = "DELETE FROM taras WHERE Folio = @Folio AND Tara = @Tara";
                var rows = db.ExecuteAsync(query, tara);
                return Results.Ok(rows);
            });

            group.MapGet("/exist/", async ([FromQuery] string? s, [FromQuery] string? id, MySqlConnection db) =>
            {
                try
                {
                    var query = "SELECT count(*) FROM taras WHERE Folio = @folio AND Tara = @tara limit 1";

                    var rows = db.ExecuteScalar<int>(query, new { folio = s, tara = id });
                    return Results.Ok(rows);
                }
                catch (Exception e)
                {
                    throw e;
                }
            });

            group.MapGet("/isbusy/", async ([FromQuery] string ? s, [FromQuery] string ? id, MySqlConnection db) =>
            {
                var query = "SELECT COUNT(*) FROM pedidos_detalle WHERE Folio = @folio AND Contenedor = @tara limit 1";
                var res = db.ExecuteScalar<int>(query, new { folio = s, tara = id });
                return Results.Ok(res);
            });

            group.MapGet("/lista/{folio}", async (string folio, MySqlConnection db) =>
            {
                List<Contenedor> lista;

                var query = "SELECT * FROM taras WHERE Folio = @Folio";
                lista = db.Query<Contenedor>(query, new { Folio = folio }).ToList();
                return Results.Ok(lista);
            });


        }
    }
}
