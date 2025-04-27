using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BodegaMovil.CoreBusiness;
using BodegaMovil.UseCases.Interfaces;

namespace BodegaMovil.Plugins.DataStore.WebApi
{
    public class ContenedorRepository : IContenedorRepository
    {
        private HttpClient _httpClient;
        private JsonSerializerOptions _serializerOptions;
        public ContenedorRepository()
        {
            _httpClient = new HttpClient();
            _serializerOptions = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true,
            };
        }

        public async Task<int> Insert(Contenedor tara)
        {
            string json = JsonSerializer.Serialize(tara, _serializerOptions);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            Uri uri = new Uri($"{Constants.url}/taras");
            int res = 0;
            var response = await _httpClient.PostAsync(uri, content);
            if (response.IsSuccessStatusCode)
            {
                string respuesta = await response.Content.ReadAsStringAsync();
                res = JsonSerializer.Deserialize<int>(respuesta, _serializerOptions);
            }

            return res;
        }

        public async Task<bool> Existe(Contenedor tara)
        {
            Uri uri = new Uri($"{Constants.url}/taras/existe?id={tara.Tara}&s={tara.Folio}");
            bool res = false;
            var response = await _httpClient.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                res = JsonSerializer.Deserialize<bool>(content, _serializerOptions);
            }

            return res;
        }
        public async Task<bool> IsBusy(Contenedor tara)
        {
            Uri uri = new Uri($"{Constants.url}/taras/isbusy?id={tara.Tara}&s={tara.Folio}");
            bool res = false;
            var response = await _httpClient.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                res = JsonSerializer.Deserialize<bool>(content, _serializerOptions);
            }

            return res;
        }

        public async Task<int> Update(Contenedor tara)
        {
            string json = JsonSerializer.Serialize(tara, _serializerOptions);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            Uri uri = new Uri($"{Constants.url}/taras");
            int res = 0;
            var response = await _httpClient.PutAsync(uri, content);
            if (response.IsSuccessStatusCode)
            {
                string respuesta = await response.Content.ReadAsStringAsync();
                res = JsonSerializer.Deserialize<int>(respuesta, _serializerOptions);
            }

            return res;
        }

        public async Task<int> Delete(Contenedor tara)
        {
            string json = JsonSerializer.Serialize(tara, _serializerOptions);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            Uri uri = new Uri($"{Constants.url}/taras");
            int res = 0;
            var response = await _httpClient.DeleteAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                string respuesta = await response.Content.ReadAsStringAsync();
                res = JsonSerializer.Deserialize<int>(respuesta, _serializerOptions);
            }

            return res;
        }

        public async Task<List<Contenedor>> GetTaras(string folio)
        {
            Uri uri = new Uri($"{Constants.url}/taras/lista/{folio}");
            List<Contenedor>? lista = null;
            var response = await _httpClient.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                lista = JsonSerializer.Deserialize<List<Contenedor>>(content, _serializerOptions);
            }

            return lista;
        }
    }
}
