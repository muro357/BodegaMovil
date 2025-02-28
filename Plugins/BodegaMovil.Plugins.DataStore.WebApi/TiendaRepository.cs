using BodegaMovil.CoreBusiness;
using BodegaMovil.UseCases.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BodegaMovil.UseCases.DTO;

namespace BodegaMovil.Plugins.DataStore.WebApi
{
    public class TiendaRepository : ITiendaRepository
    {
        private HttpClient _httpClient;
        private JsonSerializerOptions _serializerOptions;

        public TiendaRepository()
        {
            _httpClient = new HttpClient();
            _serializerOptions = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true,
            };
        }
        public async Task<Tienda> GetById(int id)
        {
            Uri uri = new Uri($"{Constants.url}/tiendas/{id}");
            Tienda tienda = null;
            var response = await _httpClient.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                tienda = JsonSerializer.Deserialize<Tienda>(content, _serializerOptions);
            }

            return tienda;
        }

        public async Task<List<Tienda>> GetTiendasHabilitadas()
        {
            var tiendas = new List<Tienda>();

            Uri uri = new Uri($"{Constants.url}/tiendas");

            var response = await _httpClient.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                tiendas = JsonSerializer.Deserialize<List<Tienda>>(content, _serializerOptions);
            }

            return tiendas;
        }
    }
}
