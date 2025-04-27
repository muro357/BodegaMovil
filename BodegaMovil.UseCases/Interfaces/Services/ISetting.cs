using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BodegaMovil.UseCases.Interfaces.Services
{
    public interface ISetting
    {
        Task SetApiUrlAsync(string url);
        Task<string> GetApiUrlAsync();
        Task SetStoreIdAsync(string storeId);
        Task<string> GetStoreIdAsync();
        Task SetConnectionAsync(string cadena);
        Task<string> GetConnectionAsync();
    }
}
