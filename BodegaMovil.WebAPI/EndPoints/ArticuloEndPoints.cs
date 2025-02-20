using BodegaMovil.CoreBusiness;
using BodegaMovil.UseCases.DTO;
using MySql.Data.MySqlClient;
using Dapper;
using Microsoft.AspNetCore.Mvc;

namespace BodegaMovil.WebAPI.EndPoints
{
    public static class ArticuloEndPoints
    {
        public static void MapArticuloRoutes(this WebApplication app)
        {

            var group = app.MapGroup("/api/articulos"); // Prefijo común

            group.MapPost("/", async (GetArticuloDTO search, MySqlConnection db) =>
            {
                try
                {
                    var query = @"SELECT 
                                    *, ExistenciaActual AS ExistenciaCedis, Ubicacion AS UbicacionCedis
                                FROM vexistencias 
                    WHERE (SKU = @sku OR CodigoDeBarra = @SKU) AND ID_Tienda = @ID_Tienda";
                    
                    var art = db.QueryFirst<ArticuloDTO>(query, search);

                    return Results.Ok(art);
                }
                catch (Exception e)
                {
                    throw e;
                }
            });

            group.MapGet("/", async ([FromQuery] string? s, [FromQuery] string? id, MySqlConnection db) =>
            {
                List<ArticuloDTO> lista;

                if (!string.IsNullOrWhiteSpace(s))
                {
                    
                    var query = @"SELECT 
                        a.*, e.* 
                        FROM Articulos a JOIN Existencias e 
                        ON a.SKU = e.SKU
                        WHERE e.ID_Tienda = @ID_Tienda AND Descripcion LIKE CONCAT('%',@Descripcion,'%')";

                    lista = db.Query<ArticuloDTO>(query, 
                        new { Descripcion = s, ID_Tienda = id}).ToList();

                    return Results.Ok(lista);
                }
                else
                    lista = new List<ArticuloDTO>();

                return Results.Ok(lista);
            });

        }
    }
}
