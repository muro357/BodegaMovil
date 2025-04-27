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
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true,
            };
        }
        public async Task<UsuarioDTO> Autentificarse(Usuario login)
        {
            var user1 = new AccesoDTO2
            {
                Usuario = login.usuario,
                Password = login.password
            };

            string json = JsonSerializer.Serialize(user1, _serializerOptions);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            Uri uri = new Uri($"{Constants.url}/usuarios");
            //Uri uri = new Uri($"http://localhost:5193/api/usuarios");
            //Uri uri2 = new Uri($"http://10.0.2.2:5193/api/usuarios");

            UsuarioDTO? user = null;
            //try
            //{
                var response = await _httpClient.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    string respuesta = await response.Content.ReadAsStringAsync();
                    user = JsonSerializer.Deserialize<UsuarioDTO>(respuesta, _serializerOptions);
                }

            //}
            //catch (Exception ex)
            //{
            //user = new UsuarioDTO
            //    {
            //        Nombre = ex.Message,
            //    };
            //}

            
            
            return user;
            

            
        }
    }
}
