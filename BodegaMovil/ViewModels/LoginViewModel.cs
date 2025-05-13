using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using BodegaMovil.CoreBusiness;
using BodegaMovil.UseCases.DTO;
using BodegaMovil.UseCases.Interfaces;
using BodegaMovil.UseCases;
using BodegaMovil.Views;
using System.Text.Json;
using Microsoft.Maui.Controls.PlatformConfiguration;
using BodegaMovil.Services.Settings.Preferences;
using BodegaMovil.UseCases.Interfaces.Services;

namespace BodegaMovil.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        LoguearseUseCase _loguearse;
        private readonly ISetting _settings;
        private AccesoDTO _acceso;

        public AccesoDTO Acceso
        {
            get
            {
                return _acceso;
            }
            set
            {
                if (_acceso != value)
                {
                    SetProperty(ref _acceso, value);
                }
            } 
        }

        public LoginViewModel(LoguearseUseCase loguearse, ISetting settings)
        {
            _acceso = new AccesoDTO();
            _loguearse = loguearse;
            _settings = settings;
        }

        [RelayCommand]
        private async void Login()
        {
            if (string.IsNullOrWhiteSpace(Acceso.usuario) || string.IsNullOrWhiteSpace(Acceso.password))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Ingrese usuario y contraseña", "OK");
                return;
            }

            var id_cedis = await _settings.GetStoreIdAsync();
            var cadena = await _settings.GetConnectionAsync();

            if (string.IsNullOrWhiteSpace(id_cedis) || string.IsNullOrWhiteSpace(cadena))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "No se ha configurado la tienda o la cadena de conexión", "OK");
                return;
            }

            var user = await _loguearse.ExecuteAsync(_acceso);

            if (user != null)
            {   //Autenticación exitosa, almacena el usuario en el SecureStorage
                await SecureStorage.Default.SetAsync("user", user.usuario);
                await SecureStorage.Default.SetAsync("nombreUser", user.Nombre);

                

                // Luego recarga el ViewModel del AppShell
                if (Shell.Current is AppShell appShell &&
                    appShell.BindingContext is AppShellViewModel vm)
                {
                    await vm.CargarDatosUsuarioAsync();
                }


                await Shell.Current.GoToAsync($"//{nameof(ListaPedidosPage)}", 
                    new Dictionary<string, object>
                    {
                        { "Usuario", user } 
                    });
            }
            else
                await Application.Current.MainPage.DisplayAlert("Error", "El usuario y/o password es incorrecto", "OK");

        }

        [RelayCommand]
        private async Task GoToConfiguracion()
        {
            await Shell.Current.GoToAsync($"{nameof(ConfiguracionAccesoPage)}");
        }
    }
}
