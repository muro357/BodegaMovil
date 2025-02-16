using BodegaMovil.CoreBusiness;
using BodegaMovil.UseCases.DTO;
using BodegaMovil.UseCases.Interfaces;
using Microsoft.Maui.ApplicationModel.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BodegaMovil.Plugins.DataStore.WebApi
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private HttpClient _httpClient;
        private JsonSerializerOptions _serializerOptions;

        public UsuarioRepository()
        {
            _httpClient = new HttpClient();
            _serializerOptions = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true,
            };
        }
        public async Task<UsuarioDTO> Autentificarse(Usuario login)
        {
            string json = JsonSerializer.Serialize(login, _serializerOptions);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            Uri uri = new Uri($"{Constants.url}/usuarios");
            UsuarioDTO user = null;
            var response = await _httpClient.PostAsync(uri,content);

            if (response.IsSuccessStatusCode)
            {
                string respuesta = await response.Content.ReadAsStringAsync();
                user = JsonSerializer.Deserialize<UsuarioDTO>(respuesta, _serializerOptions);
            }

            return user;
        }
    }
}
