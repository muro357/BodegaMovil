using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
using System.Threading.Tasks;

namespace BodegaMovil.ViewModels
{
    public partial class AppShellViewModel : ObservableObject
    {
        [ObservableProperty]
        private string nombreUsuario = "Invitado";

        //[ObservableProperty]
        //private string fotoUsuario = "usuario_default.png";

        public AppShellViewModel()
        {
            //_ = CargarDatosUsuarioAsync();//Se llama por si quieres valores por default
        }

        public async Task CargarDatosUsuarioAsync()
        {
            var nombre = await SecureStorage.Default.GetAsync("nombreUser");
            //var foto = await SecureStorage.Default.GetAsync("fotoUser");

            NombreUsuario = string.IsNullOrWhiteSpace(nombre) ? "Invitado" : nombre;
            //FotoUsuario = string.IsNullOrWhiteSpace(foto) ? "usuario_default.png" : foto;
        }

        [RelayCommand]
        private async Task CerrarSesion()
        {
            bool confirmar = await Shell.Current.DisplayAlert(
                "Cerrar Sesión",
                "¿Estás seguro de cerrar sesión?",
                "Sí", "No");

            if (confirmar)
            {
                SecureStorage.Default.Remove("nombreUser");
                SecureStorage.Default.Remove("fotoUser");
                SecureStorage.Default.Remove("user");

                // Ocultar el flyout
                Shell.Current.FlyoutIsPresented = false;

                // Navegar al login
                await Shell.Current.GoToAsync("//LoginPage");

                // Opcional: reiniciar nombre/foto
                NombreUsuario = "Invitado";
                //FotoUsuario = "usuario_default.png";
            }
        }

        [RelayCommand]
        private async Task Salir()
        {
            bool confirmar = await Shell.Current.DisplayAlert(
                "Salir",
                "¿Deseas cerrar la aplicación?",
                "Sí", "No");

            if (confirmar)
            {
                Application.Current.Quit();
            }
        }
    }
}

