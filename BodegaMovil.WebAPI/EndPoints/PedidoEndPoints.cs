using BodegaMovil.CoreBusiness;
using BodegaMovil.UseCases.DTO;
using MySql.Data.MySqlClient;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto.Utilities;
namespace BodegaMovil.WebAPI.EndPoints
{
    public static class PedidoEndPoints
    {
        public static void MapPedidoRoutes(this WebApplication app)
        {

            var group = app.MapGroup("/api/pedido"); // Prefijo común

            group.MapPost("/addnew", async (PedidoDetalle p, MySqlConnection db) =>
            {
                try
                {
                    var query = @"INSERT INTO pedidos_detalle (Consecutivo, ID_Tienda, ID_Area, Tipo, Folio, SKU, FormaDeCalculo, CantidadPedida, CantidadSurtida, Contenedor, FechaSurtido) VALUES(@Consecutivo, @ID_Tienda, @ID_Area, @Tipo, @Folio, @SKU, @FormaDeCalculo, @CantidadPedida, @CantidadSurtida, @Contenedor, now())";

                    var entidad = db.Execute(query, p);
                    return Results.Ok(entidad);
                }
                catch (Exception e)
                {
                    throw e;
                }
            });

            group.MapPut("/surtir", async (PedidoDetalle p, MySqlConnection db) =>
            {
                try
                {
                    var query = @"UPDATE pedidos_detalle 
                    SET CantidadSurtida = @CantidadSurtida, Contenedor = @Contenedor, FechaSurtido = NOW() 
                    WHERE Folio = @Folio AND SKU = @SKU"; ;

                    var entidad = db.Query<Area>(query, p);
                    return Results.Ok(entidad);
                }
                catch (Exception e)
                {
                    throw e;
                }
            });

            group.MapPut("/surtir/{folio}", async (string folio, List<PedidoDetalle> p, MySqlConnection db) =>
            {
                await db.OpenAsync();
                using (var transaction = await db.BeginTransactionAsync())
                {
                    try
                    {
                        // Construir la consulta SQL dinámica
                        var updateQuery = @"
                    UPDATE Pedidos_Detalle
                    SET 
                    CantidadSurtida = CASE 
                        " + string.Join(" ", p.Select((d, index) => $"WHEN SKU = @SKU{index} THEN @CantidadSurtida{index}")) + @"
                        ELSE CantidadSurtida 
                    END,
                    Contenedor = CASE 
                        " + string.Join(" ", p.Select((d, index) => $"WHEN SKU = @SKU{index} THEN @Contenedor{index}")) + @"
                        ELSE Contenedor 
                    END
                    WHERE Folio = @Folio AND SKU IN (" + string.Join(",", p.Select((d, index) => $"@SKU{index}")) + @");
            ";

                        // Crear un objeto anónimo con los parámetros
                        var parameters = new DynamicParameters();
                        parameters.Add("Folio", folio);

                        for (int i = 0; i < p.Count; i++)
                        {
                            parameters.Add($"SKU{i}", p[i].SKU);
                            parameters.Add($"CantidadSurtida{i}", p[i].CantidadSurtida);
                            parameters.Add($"Contenedor{i}", p[i].Contenedor);
                        }

                        // Ejecutar la actualización dentro de la transacción
                        await db.ExecuteAsync(updateQuery, parameters, transaction);

                        // Confirmar la transacción si todo está bien
                        await transaction.CommitAsync();

                        return Results.Ok(updateQuery);
                    }
                    catch (Exception ex)
                    {
                        // Revertir la transacción en caso de error
                        await transaction.RollbackAsync();
                        return Results.Problem($"Error al actualizar los detalles del pedido: {ex.Message}");
                    }
                    finally
                    {
                        await db.CloseAsync();
                    }
                }
            });

            group.MapPut("/setcero", async (string folio, string listaSkus, MySqlConnection db) =>
            {
                try
                {
                    var query = @"UPDATE pedidos_detalle 
                    SET CantidadSurtida = 0, Contenedor = 0, FechaSurtido = NOW() 
                    WHERE Folio = @Folio AND SKU IN (@SKU)";

                    var entidad = db.Execute(query, new { SKU = listaSkus, Folio = folio });
                    return Results.Ok(entidad);
                }
                catch (Exception e)
                {
                    throw e;
                }
            });


            group.MapPut("/finalizar", async (string folio, string estado, MySqlConnection db) =>
            {
                try
                {
                    var query = @"UPDATE pedidos SET Estado = @Estado WHERE Folio = @Folio";

                    var entidad = db.Execute(query, new { Folio = folio, Estado = estado });
                    return Results.Ok(entidad);
                }
                catch (Exception e)
                {
                    throw e;
                }
            });


        }
    }
}
