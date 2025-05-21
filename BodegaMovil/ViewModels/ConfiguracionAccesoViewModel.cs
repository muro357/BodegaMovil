using BodegaMovil.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BodegaMovil.ViewModels
{
    public partial class ConfiguracionAccesoViewModel : ObservableObject
    {
        private readonly ILogger<ConfiguracionAccesoViewModel> _logger;

        public ConfiguracionAccesoViewModel(ILogger<ConfiguracionAccesoViewModel> logger)
        {
            _logger = logger;
        }

        public ConfiguracionAccesoViewModel(){}

        [ObservableProperty]
        private string _user;

        [ObservableProperty]
        private string _password;

        [RelayCommand]
        public async Task Acceder()
        {
            if ((string.IsNullOrWhiteSpace(User) || string.IsNullOrWhiteSpace(Password)))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "El usuario y/o password no pueden estar vacíos", "OK");
                return;
            }
            else if (User == "admin" && Password == "control")
            {
                try
                {
                    _logger.LogInformation("Accediendo a la configuración con usuario: {User}", User);
                    await Shell.Current.GoToAsync($"{nameof(ConfiguracionPage)}");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error al navegar a la página de configuración");
                    await Application.Current.MainPage.DisplayAlert("Error", "No se pudo acceder a la página de configuración" + ex.Message, "OK");
                }
            }
            else
                await Application.Current.MainPage.DisplayAlert("Error", "El usuario y/o password es incorrecto", "OK");
        }
    }
}
