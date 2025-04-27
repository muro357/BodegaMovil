using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BodegaMovil.CoreBusiness;
using BodegaMovil.UseCases.Interfaces;
using MySql.Data.MySqlClient;
using Dapper;
using BodegaMovil.UseCases.Interfaces.Services;

namespace BodegaMovil.Plugins.DataStore.MySQL
{
    public class AreaRepository : IAreaRepository
    {

        private MySqlConnection db;
        private readonly ISetting _settings;

        public AreaRepository(ISetting settings)
        {
            _settings = settings;
            
        }

       private async Task<MySqlConnection> GetConnection()
        {
            var cadena = await _settings.GetConnectionAsync();
            return new MySqlConnection(cadena);
        }

        public async Task<List<Area>> GetAreasHabilitadas()
        {
            List<Area> lista;
            try
            {
                db = await GetConnection();
                await db.OpenAsync();
                var query = "SELECT * FROM Areas WHERE Habilitada = 1";
                lista = db.Query<Area>(query).ToList();
                return lista;
            }
            catch (MySqlException e)
            {
                throw e;
            }
            finally 
            {
                await db.CloseAsync();
            }
        }

        public async Task<Area> GetById(int id)
        {
            try
            {
                db = await GetConnection();
                await db.OpenAsync();

                var query = "SELECT * FROM Areas WHERE ID_Area = @id";
                db = await GetConnection();
                await db.OpenAsync();
                using (var transaction = await db.BeginTransactionAsync())
                {
                    var entidad = db.QueryFirst<Area>(query, new { id = id });
                    return entidad;
                }
            }
            catch (MySqlException e)
            {
                throw e;
            }
            finally
            {
                await db.CloseAsync();
            }
        }
    }
}
