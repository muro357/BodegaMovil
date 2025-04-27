using BodegaMovil.UseCases.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using MySql.Data.MySqlClient;

namespace BodegaMovil.Plugins.DataStore.MySQL
{
    public class Conexion : IConexion
    {
        public async Task<bool> ProbarConexion(string datos)
        {
            try
            {
                using var connection = new MySqlConnection(datos);
                await connection.OpenAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
   
}
