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
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true,
            };
        }
        public async Task<ArticuloDTO> GetArticulo(string sku, int id_tienda)
        {
            var param = new GetArticuloDTO()
            {
                SKU = sku,
                ID_Tienda = id_tienda
            };
            string json = JsonSerializer.Serialize(param, _serializerOptions);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            ArticuloDTO? art = null;
           
            Uri uri = new Uri($"{Constants.url}/articulos");

            var response = await _httpClient.PostAsync(uri,content);

            if (response.IsSuccessStatusCode)
            {
                string respuesta = await response.Content.ReadAsStringAsync();
                art = JsonSerializer.Deserialize<ArticuloDTO>(respuesta, _serializerOptions);
            }
            
            return art;
        }

        public async Task<List<ArticuloDTO>> GetArticulos(string filtro, int id_tienda)
        {
            var arts = new List<ArticuloDTO>();

            Uri uri = new Uri($"{Constants.url}/articulos?id={id_tienda}&s={filtro}");

            var response = await _httpClient.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                arts = JsonSerializer.Deserialize<List<ArticuloDTO>>(content, _serializerOptions);
            }

            return arts;
        }
    }
}
