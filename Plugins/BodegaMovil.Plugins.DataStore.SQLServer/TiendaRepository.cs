using BodegaMovil.CoreBusiness;
using BodegaMovil.UseCases.Interfaces;
using MySql.Data.MySqlClient;
using Dapper;
using BodegaMovil.UseCases.Interfaces.Services;

namespace BodegaMovil.Plugins.DataStore.MySQL
{
    public class TiendaRepository : ITiendaRepository
    {
        private MySqlConnection db;
        private readonly ISetting _settings;

        public TiendaRepository(ISetting settings)
        {
            _settings = settings;
            
        }
        private async Task<MySqlConnection> GetConnection()
        {
            var cadena = await _settings.GetConnectionAsync();
            return new MySqlConnection(cadena);
        }
        public async Task<Tienda> GetById(int id)
        {
            try
            {
                db = await GetConnection();
                await db.OpenAsync();
                var query = "SELECT * FROM Tiendas WHERE ID_Tienda = @id";

                var entidad = db.QueryFirst<Tienda>(query, new { id = id });
                return entidad;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<List<Tienda>> GetTiendasHabilitadas()
        {
            List<Tienda> lista;
            db = await GetConnection();
            await db.OpenAsync();
            var query = "SELECT * FROM Tiendas WHERE Habilitada = 1";
            lista = db.Query<Tienda>(query).ToList();
            return lista;
        }
    }
}
