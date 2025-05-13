using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using BodegaMovil.Views;
using BodegaMovil.ViewModels;
using BodegaMovil.UseCases;
using BodegaMovil.UseCases.Interfaces;
//using BodegaMovil.Plugins.DataStore.WebApi;
using BodegaMovil.Plugins.DataStore.MySQL;
using BodegaMovil.UseCases.Interfaces.Services;
using BodegaMovil.Services.Maps.AutoMapper;
using BodegaMovil.Services.Settings.Preferences;

namespace BodegaMovil
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            AppContext.SetSwitch("System.Reflection.NullabilityInfoContext.IsSupported", true);

            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.UseMauiApp<App>().UseMauiCommunityToolkit();

#if DEBUG
            builder.Logging.AddDebug();
#endif
            builder.Services.AddSingleton<ISetting, AppSettingsService>();
            builder.Services.AddSingleton<IMapa, AutoMapperConfig>();
            builder.Services.AddSingleton<IUsuarioRepository, UsuarioRepository>();
            builder.Services.AddSingleton<IPedidoRepository, PedidoRepository>();
            builder.Services.AddSingleton<IArticuloRepository, ArticuloRepository>();
            builder.Services.AddSingleton<ITiendaRepository, TiendaRepository>();
            builder.Services.AddSingleton<IConexion, Conexion>();

            builder.Services.AddSingleton<LoguearseUseCase>();
            builder.Services.AddSingleton<GetPedidosSurtirUseCase>();
            builder.Services.AddSingleton<GetPedidoSurtirUseCase>();
            builder.Services.AddSingleton<GetArticulosUseCase>();
            builder.Services.AddSingleton<GetArticuloUseCase>();
            builder.Services.AddSingleton<SurtirUseCase>();
            builder.Services.AddSingleton<ContemplarExistenciaUseCase>();
            builder.Services.AddSingleton<DepurarUseCase>();
            builder.Services.AddSingleton<AddNewArtSurtirUseCase>();
            builder.Services.AddSingleton<DeleteTaraUseCase>();
            builder.Services.AddSingleton<FinalizarSurtidoUseCase>();
            builder.Services.AddSingleton<ProbarConexionUseCase>();
            builder.Services.AddSingleton<GetTiendasHabilitadasUseCase>();

            builder.Services.AddSingleton<LoginPage>();
            builder.Services.AddSingleton<ListaPedidosPage>();
            builder.Services.AddSingleton<ListaArticulosPage>();
            builder.Services.AddSingleton<CapturaArticuloPage>();
            builder.Services.AddSingleton<ConfiguracionPage>();
            builder.Services.AddSingleton<BuscarArticuloPage>();
            builder.Services.AddSingleton<AgregaArticuloPage>();
            builder.Services.AddSingleton<ConfiguracionAccesoPage>();

            builder.Services.AddSingleton<LoginViewModel>();
            builder.Services.AddSingleton<ListaPedidosViewModel>();
            builder.Services.AddSingleton<ListaArticulosViewModel>();
            builder.Services.AddSingleton<ArticuloViewModel>();
            builder.Services.AddSingleton<BuscarArticuloViewModel>();
            builder.Services.AddSingleton<ConfiguracionViewModel>();
            builder.Services.AddSingleton<ConfiguracionAccesoViewModel>();

            return builder.Build();
        }
    }
}
