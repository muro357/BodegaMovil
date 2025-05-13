using BodegaMovil.CoreBusiness;
using BodegaMovil.UseCases.DTO;
using BodegaMovil.UseCases.Interfaces;
using BodegaMovil.UseCases.Interfaces.Services;
using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BodegaMovil.Plugins.DataStore.MySQL
{
    public class PedidoRepository : IPedidoRepository
    {
        private MySqlConnection db;
        private readonly ISetting _settings;

        public PedidoRepository(ISetting settings)
        {
            _settings = settings;
            
        }
        private async Task<MySqlConnection> GetConnection()
        {
            var cadena = await _settings.GetConnectionAsync();
            return new MySqlConnection(cadena);
        }
        public async Task<bool> AgregarArticulo(PedidoDetalle linea)
        {
            try
            {
                db = await GetConnection();
                await db.OpenAsync();
                var query = @"INSERT INTO pedidos_detalle (Consecutivo, ID_Tienda, ID_Area, Tipo, Folio, SKU, FormaDeCalculo, CantidadPedida, CantidadSurtida, Contenedor, FechaSurtido, SurtidoPor) VALUES(@Consecutivo, @ID_Tienda, @ID_Area, @Tipo, @Folio, @SKU, @FormaDeCalculo, @CantidadPedida, @CantidadSurtida, @Contenedor, now(), @SurtidoPor)";

                int res = await db.ExecuteAsync(query, linea);
                return res > 0;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                await db.CloseAsync();
            }
        }
        public async Task<bool> Finalizar(Pedido pedido)
        {
            try
            {
                var query = @"UPDATE pedidos SET Estado = 'Surtido' WHERE Folio = @Folio";
                db = await GetConnection();
                await db.OpenAsync();
                int res = await db.ExecuteAsync(query, pedido);
                return res > 0;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                await db.CloseAsync();
            }
        }

        public async Task<List<ShowPedidoDTO>> GetPedidosSurtir(IEnumerable<int> ID_Tienda, IEnumerable<int> ID_Area)
        {
            try
            {
                var solicitud = new GetPedidoDTO()
                {
                    ListaAreas = string.Join(",", ID_Area),
                    ListaTiendas = string.Join(",", ID_Tienda)
                };


                List<ShowPedidoDTO> lista;
                db = await GetConnection();
                await db.OpenAsync();
                var query = @$"SELECT Consecutivo, Folio, ID_Area, ID_Tienda, Tipo, Estado, FechaSolicitado, DescripcionTienda, DescripcionArea FROM vsurtido 
                      WHERE Estado = 'Solicitado' AND ID_Area IN ({solicitud.ListaAreas}) AND ID_Tienda IN ({solicitud.ListaTiendas}) AND CantidadSurtida is null
                      group by Consecutivo, Folio, ID_Area, ID_Tienda, Tipo, Estado, FechaSolicitado, DescripcionTienda, DescripcionArea";

                lista = db.Query<ShowPedidoDTO>(query, solicitud).ToList();
                return lista;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                await db.CloseAsync();
            }
        }

        public async Task<PedidoDTO> GetSurtirById(int id_tienda, string folio, int id_area_surtir)
        {
            try
            {
                var pedidoDictionary = new Dictionary<string, PedidoDTO>();
                db = await GetConnection();
                await db.OpenAsync();
                var query = @"SELECT 
                        s.Consecutivo,
                        s.ID_Tienda,
                        CASE WHEN s.Tipo = 'Sugerido' THEN s.ID_Area 
                             WHEN s.Tipo = 'Especial' THEN 0 END AS ID_Area,
                        s.ID_Area as ID_AreaSurtir,
                        s.Tipo,
                        s.DescripcionTienda,
                        CASE WHEN s.Tipo = 'Sugerido' THEN s.DescripcionArea 
                             WHEN s.Tipo = 'Especial' THEN 'Varios' END AS DescripcionArea,
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
                        s.Folio,
                        s.DescripcionArea           
                       FROM vsurtido s LEFT JOIN vexistencias e ON s.SKU = e.SKU 
                         AND e.ID_Tienda = @ID_Tienda WHERE s.Folio = @Folio and s.ID_Area = @ID_Area";

                var entidad = db.Query<PedidoDTO, PedidoDetalleDTO, PedidoDTO>(query, (pedido, pedidoDetalle) =>
                {
                    // Verificar si el usuario ya está en el diccionario
                    if (!pedidoDictionary.TryGetValue(pedido.Folio, out var pedidoEntry))
                    {
                        // Si no está, crear un nuevo usuario y agregarlo al diccionario
                        pedidoEntry = pedido;
                        
                        pedidoEntry.PedidoDetalle = new List<PedidoDetalleDTO>();
                        pedidoDictionary.Add(pedido.Folio, pedidoEntry);
                    }
                    
                    pedidoEntry.PedidoDetalle.Add(pedidoDetalle);

                    return pedidoEntry;
                },
                new { Folio = folio, ID_Tienda = id_tienda, ID_Area = id_area_surtir }, // Parámetros de la consulta
                splitOn: "SKU" // Columnas para dividir los objetos
                );


                var pedido = pedidoDictionary.Values.FirstOrDefault();

                if (pedido != null)
                {
                    return pedido;
                }
                else
                    return null;

            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                await db.CloseAsync();
            }
        }

        public async Task<bool> Surtir(PedidoDetalle pedidoDetalle)
        {
            try
            {
                db = await GetConnection();
                await db.OpenAsync();
                var query = @"UPDATE pedidos_detalle 
                    SET CantidadSurtida = @CantidadSurtida, Contenedor = @Contenedor, FechaSurtido = NOW(), SurtidoPor = @SurtidoPor  
                    WHERE Folio = @Folio AND SKU = @SKU";

                int res = await db.ExecuteAsync(query, pedidoDetalle);


                return res > 0;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                await db.CloseAsync();
            }
        }
        public async Task<bool> SurtirVarios(string folio, List<PedidoDetalle> list)
        {
            db = await GetConnection();
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
                        " + string.Join(" ", list.Select((d, index) => $"WHEN SKU = @SKU{index} THEN @CantidadSurtida{index}")) + @"
                        ELSE CantidadSurtida 
                    END,
                    Contenedor = CASE 
                        " + string.Join(" ", list.Select((d, index) => $"WHEN SKU = @SKU{index} THEN @Contenedor{index}")) + @"
                        ELSE Contenedor 
                    END,
                    SurtidoPor = CASE 
                        " + string.Join(" ", list.Select((d, index) => $"WHEN SKU = @SKU{index} THEN @SurtidoPor{index}")) + @"
                        ELSE SurtidoPor 
                    END
                    WHERE Folio = @Folio AND SKU IN (" + string.Join(",", list.Select((d, index) => $"@SKU{index}")) + @");
            ";

                    // Crear un objeto anónimo con los parámetros
                    var parameters = new DynamicParameters();
                    parameters.Add("Folio", folio);

                    for (int i = 0; i < list.Count; i++)
                    {
                        parameters.Add($"SKU{i}", list[i].SKU);
                        parameters.Add($"CantidadSurtida{i}", list[i].CantidadSurtida);
                        parameters.Add($"Contenedor{i}", list[i].Contenedor);
                        parameters.Add($"SurtidoPor{i}", list[i].SurtidoPor);
                    }

                    // Ejecutar la actualización dentro de la transacción
                    int res = await db.ExecuteAsync(updateQuery, parameters, transaction);

                    // Confirmar la transacción si todo está bien
                    await transaction.CommitAsync();

                    return res>0;
                }
                catch (Exception ex)
                {
                    // Revertir la transacción en caso de error
                    await transaction.RollbackAsync();
                    throw new Exception($"Error al actualizar los detalles del pedido: {ex.Message}");
                }
                finally
                {
                    await db.CloseAsync();
                }
            }
        }
    }
    
}
