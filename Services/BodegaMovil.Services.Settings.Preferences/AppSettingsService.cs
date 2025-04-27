using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BodegaMovil.UseCases.Interfaces.Services;

namespace BodegaMovil.Services.Settings.Preferences
{
    public class AppSettingsService : ISetting
    {
        const string ApiUrlKey = "Url";
        const string StoreIdKey = "ID_Cedis";
        const string CadenaConexion = "Conexion";

        public AppSettingsService()
        {
            
        }

        #region URL
        public async Task SetApiUrlAsync(string url) =>
            await Task.Run(() => SecureStorage.Default.SetAsync(ApiUrlKey, url));

        public async Task<string> GetApiUrlAsync()
        {
            var url = await Task.Run(() => SecureStorage.Default.GetAsync(ApiUrlKey));
            if (string.IsNullOrWhiteSpace(url))
            {
                url = string.Empty;
            }
            return url;
        }
        #endregion

        #region Tienda
        public async Task SetStoreIdAsync(string storeId) =>
            await Task.Run(() => SecureStorage.Default.SetAsync(StoreIdKey, storeId));

        
        public async Task<string> GetStoreIdAsync()
        {
            //return await SecureStorage.Default.GetAsync(StoreIdKey);
            var id_tienda = await Task.Run(() => SecureStorage.Default.GetAsync(StoreIdKey));
            if (string.IsNullOrWhiteSpace(id_tienda))
            {
                id_tienda = string.Empty;
            }
            return id_tienda;
        }
        #endregion

        #region Cadena de Conexion
        public async Task SetConnectionAsync(string cadena) =>
            await Task.Run(() => SecureStorage.Default.SetAsync(CadenaConexion, cadena));


        public async Task<string> GetConnectionAsync()
        {
            var cadena = await Task.Run(() => SecureStorage.Default.GetAsync(CadenaConexion));
            if (string.IsNullOrWhiteSpace(cadena))
            {
                cadena = string.Empty;
            }
            return cadena;
        }
        #endregion
    }
}
