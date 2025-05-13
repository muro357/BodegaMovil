using BodegaMovil.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BodegaMovil.ViewModels
{
    public partial class ConfiguracionAccesoViewModel : ObservableObject
    {
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
                await Shell.Current.GoToAsync($"{nameof(ConfiguracionPage)}");
            }
            else
                await Application.Current.MainPage.DisplayAlert("Error", "El usuario y/o password es incorrecto", "OK");
        }
    }
}
