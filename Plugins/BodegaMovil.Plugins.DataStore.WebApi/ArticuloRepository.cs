using BodegaMovil.CoreBusiness;
using BodegaMovil.UseCases.DTO;
using BodegaMovil.UseCases.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BodegaMovil.Plugins.DataStore.WebApi
{
    public class ArticuloRepository : IArticuloRepository
    {
        private HttpClient _httpClient;
        private JsonSerializerOptions _serializerOptions;

        public ArticuloRepository()
        {
            _httpClient = new HttpClient();
            _serializerOptions = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true,
            };
        }
        public async Task<ArticuloDTO> GetArticulo(string sku, int id_tienda)
        {

            Uri uri = new Uri($"{Constants.url}/articulos/{id_tienda}/{sku}");
            ArticuloDTO art = null;
            var response = await _httpClient.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                art = JsonSerializer.Deserialize<ArticuloDTO>(content, _serializerOptions);
            }

            return art;
        }

        public async Task<List<Articulo>> GetArticulos(string filtro, int id_tienda)
        {
            var arts = new List<Articulo>();

            Uri uri = new Uri($"{Constants.url}/articulos?id={id_tienda}&s={filtro}");

            var response = await _httpClient.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                arts = JsonSerializer.Deserialize<List<Articulo>>(content, _serializerOptions);
            }

            return arts;
        }
    }
}
