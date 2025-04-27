using BodegaMovil.CoreBusiness;
using BodegaMovil.UseCases.DTO;
using BodegaMovil.UseCases.Interfaces;
using Microsoft.Maui.ApplicationModel.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BodegaMovil.Plugins.DataStore.WebApi
{
    public class PedidoRepository : IPedidoRepository
    {
        private HttpClient _httpClient;
        private JsonSerializerOptions _serializerOptions;
        public PedidoRepository()
        {
            _httpClient = new HttpClient();
            _serializerOptions = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true,
            };
        }
        public async Task<bool> AgregarArticulo(PedidoDetalle linea)
        {
            string json = JsonSerializer.Serialize(linea, _serializerOptions);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            Uri uri = new Uri($"{Constants.url}/pedido/addnew");
            
            var response = await _httpClient.PostAsync(uri, content);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }

        
        public async Task<bool> Finalizar(Pedido pedido)
        {
            string json = JsonSerializer.Serialize(pedido, _serializerOptions);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            Uri uri = new Uri($"{Constants.url}/pedido/finalizar");

            var response = await _httpClient.PutAsync(uri, content);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }

        public async Task<List<ShowPedidoDTO>> GetPedidosSurtir(IEnumerable<int> ID_Tienda, IEnumerable<int> ID_Area)
        {
            var solicitud = new GetPedidoDTO()
            {
                ListaAreas = string.Join(",", ID_Area),
                ListaTiendas = string.Join(",", ID_Tienda)
            };

            string json = JsonSerializer.Serialize(solicitud, _serializerOptions);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            Uri uri = new Uri($"{Constants.url}/getpedidos/surtir");
            List<ShowPedidoDTO> lista = null;
            var response = await _httpClient.PostAsync(uri, content);

            if (response.IsSuccessStatusCode)
            {
                string respuesta = await response.Content.ReadAsStringAsync();
                lista = JsonSerializer.Deserialize<List<ShowPedidoDTO>>(respuesta, _serializerOptions);
            }

            return lista;
        }

        public async Task<PedidoDTO> GetSurtirById(int id_tienda, string folio, int id_area_surtir)
        {
            Uri uri = new Uri($"{Constants.url}/getpedidos/{id_tienda}/{folio}");
            PedidoDTO p = null;
            var response = await _httpClient.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                p = JsonSerializer.Deserialize<PedidoDTO>(content, _serializerOptions);
            }

            return p;
        }

        public async Task<bool> Surtir(PedidoDetalle linea)
        {
            string json = JsonSerializer.Serialize(linea, _serializerOptions);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            Uri uri = new Uri($"{Constants.url}/pedido/surtir");
            
            var response = await _httpClient.PutAsync(uri, content);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> SurtirVarios(string folio, List<PedidoDetalle> lineas)
        {
            string json = JsonSerializer.Serialize(lineas, _serializerOptions);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            Uri uri = new Uri($"{Constants.url}/pedido/surtir/{folio}");

            var response = await _httpClient.PutAsync(uri, content);
           
            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }
    }
}
