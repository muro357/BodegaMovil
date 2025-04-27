using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using BodegaMovil.CoreBusiness;
using BodegaMovil.UseCases;
using BodegaMovil.Services.Settings.Preferences;
using BodegaMovil.UseCases.Interfaces.Services;

namespace BodegaMovil.ViewModels
{
    public partial class ConfiguracionViewModel : ObservableObject
    {
        private readonly ISetting _settings;
        private readonly ProbarConexionUseCase _probarConexion;
        private readonly GetTiendasHabilitadasUseCase _getTiendasHabilitadas;
        private int _idTiendaSelected = 0;
        //private readonly ApiService _api;

        //[ObservableProperty] private string apiUrl="";
        [ObservableProperty] private string cadena = "";
        [ObservableProperty] private ObservableCollection<Tienda> tiendas = new();
        [ObservableProperty] private Tienda tiendaSeleccionada = new();
        [ObservableProperty] private bool conexionExitosa = false;
        [ObservableProperty] private string mensajeConexion="";

        public ConfiguracionViewModel(ISetting settings, ProbarConexionUseCase probarConexion, GetTiendasHabilitadasUseCase getTiendasHabilitadas)
        {
            _settings = settings;
            _probarConexion = probarConexion;
            _getTiendasHabilitadas = getTiendasHabilitadas;

            CargarConfiguracionGuardada();
        }

        public async Task CargarConfiguracionGuardada()
        {
            //ApiUrl = await _settings.GetApiUrlAsync();
            Cadena = await _settings.GetConnectionAsync();
            var tiendaId = await _settings.GetStoreIdAsync();

            if(string.IsNullOrWhiteSpace(Cadena))
            {
                Cadena = "Server=10.0.2.2;Database=ferre;User ID=root;Password=12345678";
            }
            


            _idTiendaSelected = int.TryParse(tiendaId, out var id) ? id : 0;

            if (!string.IsNullOrWhiteSpace(tiendaId) && _idTiendaSelected > 0) 
            {
                await CargarTiendasAsync();
            }
                //TiendaSeleccionada =  // Se puede reemplazar al cargar la lista real
        }

        [RelayCommand]
        private async Task ProbarConexionAsync()
        {
            if (string.IsNullOrWhiteSpace(Cadena))
            {
                MensajeConexion = "La Cadena de Conexion no puede estar vacía.";
                ConexionExitosa = false;
                return;
            }

            /*
            if (!Uri.TryCreate(ApiUrl, UriKind.Absolute, out _))
            {
                MensajeConexion = "La URL no es válida.";
                ConexionExitosa = false;
                return;
            }
            */
            var resultado = await _probarConexion.ExecuteAsync(Cadena);
            ConexionExitosa = resultado;
            MensajeConexion = resultado ? "Conexión exitosa." : "Falló la conexión.";

            if (resultado)
            {
                await _settings.SetConnectionAsync(Cadena);
                await CargarTiendasAsync();
            }
        }

        private async Task CargarTiendasAsync()
        {
            var tiendasDesdeApi = await _getTiendasHabilitadas.ExecuteAsync();
            Tiendas.Clear();
            foreach (var item in tiendasDesdeApi)
            {
                Tiendas.Add(item);
            }

            if(Tiendas.Count > 0)
            {
                var res = Tiendas.Where(x => x.ID_Tienda == _idTiendaSelected).FirstOrDefault(); 

                if(res != null)
                {
                    TiendaSeleccionada = res;
                }
            }
            else
            {
                MensajeConexion = "No se encontraron tiendas habilitadas.";
                ConexionExitosa = false;
            }
        }

        [RelayCommand]
        private async Task GuardarAsync()
        {
            if (ConexionExitosa && TiendaSeleccionada != null)
            {
                //await _settings.SetApiUrlAsync(ApiUrl);
                await _settings.SetConnectionAsync(Cadena);
                await _settings.SetStoreIdAsync(TiendaSeleccionada.ID_Tienda.ToString());
                await Shell.Current.GoToAsync("..");
            }
            else
            {
                await Shell.Current.DisplayAlert("Error", "Debe probar conexión y seleccionar una tienda.", "OK");
            }
        }
    }
}
