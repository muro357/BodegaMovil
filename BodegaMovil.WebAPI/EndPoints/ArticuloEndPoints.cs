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

            group.MapGet("/{id}/{sku}", async (int id, string sku, MySqlConnection db) =>
            {
                try
                {
                    var query = @"SELECT a.*, e.* FROM Articulos a JOIN Existencias e ON a.SKU = e.SKU
                    WHERE a.SKU = @sku AND e.ID_Tienda = @ID_Tienda";
                    
                    var art = db.QueryFirst<ArticuloDTO>(query, new { ID_Tienda = id, SKU = sku });

                    return Results.Ok(art);
                }
                catch (Exception e)
                {
                    throw e;
                }
            });

            
        }
    }
}
