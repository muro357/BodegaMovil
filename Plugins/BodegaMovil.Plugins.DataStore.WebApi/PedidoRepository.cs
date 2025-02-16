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

            Uri uri = new Uri($"{Constants.url}/pedidos/addnew");
            //PedidoDTO p = null;
            var response = await _httpClient.PostAsync(uri, content);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }

        public async Task ContemplarExistencia(IEnumerable<PedidoDetalle> pedidoDetalles)
        {
            string json = JsonSerializer.Serialize(pedidoDetalles, _serializerOptions);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            Uri uri = new Uri($"{Constants.url}/pedidos/setcero");
            await _httpClient.PostAsync(uri, content);
        }

        public async Task Depurar(IEnumerable<PedidoDetalle> pedidoDetalles)
        {
            string json = JsonSerializer.Serialize(pedidoDetalles, _serializerOptions);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            Uri uri = new Uri($"{Constants.url}/pedidos/setcero");
            await _httpClient.PostAsync(uri, content);
        }

        public async Task<List<ShowPedidoDTO>> GetPedidosSurtir(IEnumerable<int> ID_Tienda, IEnumerable<int> ID_Area)
        {
            var solicitud = new GetPedidoDTO()
            {
                ListaAreas = string.Join(",", ID_Tienda),
                ListaTiendas = string.Join(",", ID_Area)
            };

            string json = JsonSerializer.Serialize(solicitud, _serializerOptions);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            Uri uri = new Uri($"{Constants.url}/getpedidos");
            List<ShowPedidoDTO> lista = null;
            var response = await _httpClient.PostAsync(uri, content);

            if (response.IsSuccessStatusCode)
            {
                string respuesta = await response.Content.ReadAsStringAsync();
                lista = JsonSerializer.Deserialize<List<ShowPedidoDTO>>(respuesta, _serializerOptions);
            }

            return lista;
        }

        public async Task<PedidoDTO> GetSurtirById(int id, int id_tienda, int id_area)
        {
            Uri uri = new Uri($"{Constants.url}/getpedidos/{id}");
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

            Uri uri = new Uri($"{Constants.url}/pedidos/surtir");
            //PedidoDTO p = null;
            var response = await _httpClient.PostAsync(uri, content);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }
    }
}
