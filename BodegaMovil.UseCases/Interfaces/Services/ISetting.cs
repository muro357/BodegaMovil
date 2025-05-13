using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BodegaMovil.UseCases.Interfaces.Services
{
    public interface ISetting
    {
        #region Metodos Asincronicos
        Task SetApiUrlAsync(string url);
        Task<string> GetApiUrlAsync();
        Task SetStoreIdAsync(string storeId);
        Task<string> GetStoreIdAsync();
        Task SetConnectionAsync(string cadena);
        Task<string> GetConnectionAsync();
        #endregion

#region Metodos Sincronicos
        void SetApiUrl(string url);
        string SetApiUrl();
        void SetStoreId(string storeId);
        string GetStoreId();
        void SetConnection(string cadena);
        string GetConnection();
#endregion
    }
}
