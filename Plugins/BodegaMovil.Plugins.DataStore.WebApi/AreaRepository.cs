using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BodegaMovil.CoreBusiness;
using BodegaMovil.UseCases.DTO;
using BodegaMovil.UseCases.Interfaces;

namespace BodegaMovil.Plugins.DataStore.WebApi
{
    public class AreaRepository : IAreaRepository
    {
        private HttpClient _httpClient;
        private JsonSerializerOptions _serializerOptions;
        public AreaRepository()
        {
            _httpClient = new HttpClient();
            _serializerOptions = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true,
            };
        }
        public async Task<List<Area>> GetAreasHabilitadas()
        {
            var areas = new List<Area>();

            Uri uri = new Uri($"{Constants.url}/areas");

            var response = await _httpClient.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                areas = JsonSerializer.Deserialize<List<Area>>(content, _serializerOptions);
            }

            return areas;
        }

        public async Task<Area> GetById(int id)
        {
            Uri uri = new Uri($"{Constants.url}/areas/{id}");
            Area area = null;
            var response = await _httpClient.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                area = JsonSerializer.Deserialize<Area>(content, _serializerOptions);
            }

            return area;
        }
    }
}
