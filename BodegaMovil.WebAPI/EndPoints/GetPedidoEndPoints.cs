using BodegaMovil.CoreBusiness;
using BodegaMovil.UseCases.DTO;
using MySql.Data.MySqlClient;
using Dapper;
using Microsoft.AspNetCore.Mvc;

namespace BodegaMovil.WebAPI.EndPoints
{
    public static class GetPedidoEndPoints
    {
        public static void MapGetPedidoRoutes(this WebApplication app)
        {

            var group = app.MapGroup("/api/getpedidos"); // Prefijo común
            //Obtiene una lista de pedidos
            group.MapPost("/surtir", async (GetPedidoDTO get, MySqlConnection db) =>
            {
                try
                {
                    var query = @"SELECT Consecutivo, Folio, ID_Area, ID_Tienda, Tipo, Estado, FechaSolicitado, DescripcionTienda, DescripcionArea FROM vsurtido 
                      WHERE Estado = 'Solicitado' AND ID_Area IN (@ListaAreas) AND ID_Tienda IN (@ListaTiendas) AND CantidadSurtida is null
                      group by Consecutivo, Folio, ID_Area, ID_Tienda, Tipo, Estado, FechaSolicitado, DescripcionTienda, DescripcionArea";

                    var entidad = db.Query<ShowPedidoDTO>(query, get);
                    return Results.Ok(entidad);
                }
                catch (Exception e)
                {
                    throw e;
                }
            });

            //Obtiene un pedido
            group.MapGet("/{tienda}/{folio}", async (string folio, string tienda, MySqlConnection db) =>
            {
                try
                {
                    var pedidoDictionary = new Dictionary<string, PedidoDTO>();

                    var query = @"SELECT 
                        s.Consecutivo,
                        s.ID_Tienda,
                        s.ID_Area,
                        s.Tipo,
                        s.DescripcionTienda,
                        s.DescripcionArea,
                        s.Folio,
                        s.FechaSolicitado,
                        s.FechaTransferido,
                        s.SurtidoPor,
                        s.ChecadoPor,
                        s.ObservacionesAduana,
                        s.Estado,
                        s.SKU,
                        s.CodigoDeBarra,
                        s.Descripcion,
                        s.Unidad,
                        IFNULL(e.ExistenciaActual,0) AS ExistenciaCedis,
                        IFNULL(e.Ubicacion, 0) AS UbicacionCedis,
                        s.CantidadPedida,
                        s.CantidadSurtida,
                        s.Contenedor,
                        s.SurtidoPor,
                        s.ChecadoPor,
                        s.Checado,
                        s.FechaSurtido,
                        s.FechaChecado,
                        s.Consecutivo,
                        s.ID_Tienda,
                        s.ID_Area,
                        s.Folio
                       FROM vsurtido s LEFT JOIN vexistencias e ON s.SKU = e.SKU 
                         AND e.ID_Tienda = @ID_Tienda WHERE s.Folio = @Folio";

                    var entidad = db.Query<PedidoDTO, PedidoDetalleDTO, PedidoDTO>(query, (pedido, pedidoDetalle) =>
                    {
                        // Verificar si el usuario ya está en el diccionario
                        if (!pedidoDictionary.TryGetValue(pedido.Folio, out var pedidoEntry))
                        {
                            // Si no está, crear un nuevo usuario y agregarlo al diccionario
                            pedidoEntry = pedido;
                            //usuarioEntry.ID_Perfil = perfil.ID_Perfil;
                            pedidoEntry.PedidoDetalle = new List<PedidoDetalleDTO>();
                            pedidoDictionary.Add(pedido.Folio, pedidoEntry);
                        }

                        pedidoEntry.PedidoDetalle.Add(pedidoDetalle);

                        return pedidoEntry;
                    },
                    new { Folio = folio, ID_Tienda = tienda}, // Parámetros de la consulta
                    splitOn: "SKU" // Columnas para dividir los objetos
                    );


                    var pedido = pedidoDictionary.Values.FirstOrDefault();

                    if (pedido != null)
                        return Results.Ok(pedido);

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
