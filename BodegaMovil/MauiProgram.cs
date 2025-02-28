using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using BodegaMovil.Views;
using BodegaMovil.ViewModels;
using BodegaMovil.UseCases;
using BodegaMovil.UseCases.Interfaces;
using BodegaMovil.Plugins.DataStore.WebApi;
using BodegaMovil.UseCases.Interfaces.Services;
using BodegaMovil.Services.Maps.AutoMapper;

namespace BodegaMovil
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
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
            
            builder.Services.AddSingleton<IMapa, AutoMapperConfig>();
            builder.Services.AddSingleton<IUsuarioRepository, UsuarioRepository>();
            builder.Services.AddSingleton<IPedidoRepository, PedidoRepository>();
            builder.Services.AddSingleton<IArticuloRepository, ArticuloRepository>();
            
            builder.Services.AddSingleton<LoguearseUseCase>();
            builder.Services.AddSingleton<GetPedidosSurtirUseCase>();
            builder.Services.AddSingleton<GetPedidoSurtirUseCase>();
            builder.Services.AddSingleton<GetArticulosUseCase>();
            builder.Services.AddSingleton<SurtirUseCase>();
            builder.Services.AddSingleton<GetArticuloUseCase>();
            builder.Services.AddSingleton<ContemplarExistenciaUseCase>();
            builder.Services.AddSingleton<DepurarUseCase>();
            builder.Services.AddSingleton<AddNewArtSurtirUseCase>();

            builder.Services.AddSingleton<LoginPage>();
            builder.Services.AddSingleton<ListaPedidosPage>();
            builder.Services.AddSingleton<ListaArticulosPage>();
            builder.Services.AddSingleton<CapturaArticuloPage>();

            builder.Services.AddSingleton<LoginViewModel>();
            builder.Services.AddSingleton<ListaPedidosViewModel>();
            builder.Services.AddSingleton<ListaArticulosViewModel>();
            builder.Services.AddSingleton<ArticuloViewModel>();


            return builder.Build();
        }
    }
}
