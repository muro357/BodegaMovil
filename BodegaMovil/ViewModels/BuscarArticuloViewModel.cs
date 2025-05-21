using System.Collections.ObjectModel;
using BodegaMovil.UseCases;
using BodegaMovil.UseCases.DTO;
using BodegaMovil.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Text.Json;
using BodegaMovil.UseCases.Interfaces.Services;

namespace BodegaMovil.ViewModels
{
    public partial class BuscarArticuloViewModel : ObservableObject
    {
        private readonly GetArticuloUseCase _getArticulo;
        private readonly GetArticulosUseCase _getArticulos;
        private readonly ISetting _settings;
        //private JsonSerializerOptions _serializerOptions;
        private int _id_tienda = 0;     
        private PedidoDTO _pedido;
        
        public BuscarArticuloViewModel(GetArticuloUseCase getArticulo, GetArticulosUseCase getArticulos, ISetting settings)
        {
            //_serializerOptions = new JsonSerializerOptions()
            //{
            //    PropertyNameCaseInsensitive = true,
            //    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            //    WriteIndented = true,
            //};

            _getArticulo = getArticulo;
            _getArticulos = getArticulos;
            _settings = settings;
            //TipoBusqueda = "SKU";
        }

        [ObservableProperty]
        private string filtro;

        [ObservableProperty]
        private string tipoBusqueda = "SKU";

        [ObservableProperty]
        private ObservableCollection<ArticuloDTO> listaArticulos = new ObservableCollection<ArticuloDTO>();

        public async Task Iniciar(PedidoDTO pedido)
        {
            ListaArticulos = new ObservableCollection<ArticuloDTO>();
            Filtro = string.Empty;
            _id_tienda = int.Parse(await _settings.GetStoreIdAsync());
            _pedido = pedido;
        }

        [RelayCommand]
        private async Task AgregarArticulo(ArticuloDTO articulo)
        {
            try
            {
                var parametros = new Dictionary<string, object>
                {
                    { "pedido", _pedido },
                    { "art", articulo }
                };

                await Shell.Current.GoToAsync($"{nameof(AgregaArticuloPage)}", parametros);
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"Error al agregar artículo: {ex.Message}", "OK");
            }
        }

        [RelayCommand]
        public async Task Buscar(string filtro)
        {
            if (string.IsNullOrWhiteSpace(filtro))
            {
                await App.Current.MainPage.DisplayAlert("Error", "Tipo de búsqueda no válido.", "OK");

                return;
            }
            

            if (TipoBusqueda == "SKU")
            {
                await BuscarPorSku(filtro);
            }
            else if (TipoBusqueda == "Descripcion")
            {
                await BuscarPorDescripcion(filtro);
            }
            else
            {
                // Handle invalid search type
                await App.Current.MainPage.DisplayAlert("Error", "Tipo de búsqueda no válido.", "OK");
            }
        }

        
        private async Task BuscarPorSku(string sku)
        {
            ListaArticulos.Clear();
            var art = await _getArticulo.ExecuteAsync(sku, _id_tienda);
            if (art != null)
            {
                ListaArticulos.Add(art);
            }
            else
            {
                // Handle the case when the article is not found
                await App.Current.MainPage.DisplayAlert("Error", "Artículo no encontrado.", "OK");
            }
        }
        
        private async Task BuscarPorDescripcion(string descripcion)
        {
            ListaArticulos.Clear();
            var res = await _getArticulos.ExecuteAsync(descripcion, _id_tienda);

            if (res != null)
            {
                foreach(var item in res)
                {
                    ListaArticulos.Add(item);
                }
            }
           
        }

        
    }
}
