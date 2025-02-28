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

namespace BodegaMovil.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        LoguearseUseCase _loguearse;
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

        public LoginViewModel(LoguearseUseCase loguearse)
        {
            _acceso = new AccesoDTO();
            _loguearse = loguearse;
        }

        [RelayCommand]
        private async void Login()
        {
            if (string.IsNullOrWhiteSpace(Acceso.usuario) || string.IsNullOrWhiteSpace(Acceso.password))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Ingrese usuario y contraseña", "OK");
                return;
            }

            // Aquí iría la lógica de autenticación
            //await Application.Current.MainPage.DisplayAlert("Éxito", "Inicio de sesión exitoso", "OK");

            var user = await _loguearse.ExecuteAsync(_acceso);

            if (user != null)
            {
                var json = JsonSerializer.Serialize(user);

                await Shell.Current.GoToAsync($"{nameof(ListaPedidosPage)}?user={json}");
                //await Application.Current.MainPage.DisplayAlert("Éxito", "Inicio de sesión exitoso", "OK");
            }
            else
                await Application.Current.MainPage.DisplayAlert("Error", "El usuario y/o password es incorrecto", "OK");

        }
    }
}
