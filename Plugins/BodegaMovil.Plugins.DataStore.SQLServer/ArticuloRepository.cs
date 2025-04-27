using BodegaMovil.CoreBusiness;
using BodegaMovil.UseCases.DTO;
using BodegaMovil.UseCases.Interfaces;
using BodegaMovil.UseCases.Interfaces.Services;
using Dapper;
using MySql.Data.MySqlClient;


namespace BodegaMovil.Plugins.DataStore.MySQL
{
    public class ArticuloRepository : IArticuloRepository
    {
        private MySqlConnection db;
        private readonly ISetting _settings;

        public ArticuloRepository(ISetting settings)
        {
            _settings = settings;
            
            
        }
        private async Task<MySqlConnection> GetConnection()
        {
            var cadena = await _settings.GetConnectionAsync();
            return new MySqlConnection(cadena);
        }
        public async Task<ArticuloDTO> GetArticulo(string sku, int id_tienda)
        {
            try
            {
                db = await GetConnection();
                await db.OpenAsync();
                var query = @"SELECT *, ExistenciaActual AS ExistenciaCedis, Ubicacion AS UbicacionCedis
                                FROM vexistencias 
                    WHERE (SKU = @SKU OR CodigoDeBarra = @SKU) AND ID_Tienda = @ID_Tienda";

                var art = await db.QueryFirstOrDefaultAsync<ArticuloDTO>(query, new {SKU = sku, ID_Tienda = id_tienda });

                return art;
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
        public async Task<List<ArticuloDTO>> GetArticulos(string filtro, int id_tienda)
        {
            List<ArticuloDTO> lista;

            if (!string.IsNullOrWhiteSpace(filtro))
            {
                try
                {
                    db = await GetConnection();
                    await db.OpenAsync();
                    var query = @"SELECT 
                        a.*, e.* 
                        FROM Articulos a JOIN Existencias e 
                        ON a.SKU = e.SKU
                        WHERE e.ID_Tienda = @ID_Tienda AND Descripcion LIKE CONCAT('%',@Descripcion,'%')";

                    lista = db.Query<ArticuloDTO>(query,
                        new { Descripcion = filtro, ID_Tienda = id_tienda }).ToList();

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
            else
                lista = new List<ArticuloDTO>();

            return lista;
        }
    }
   
}
