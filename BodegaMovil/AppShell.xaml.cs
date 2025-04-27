using BodegaMovil.Views;
namespace BodegaMovil
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ListaPedidosPage), typeof(ListaPedidosPage));
            Routing.RegisterRoute(nameof(ListaArticulosPage), typeof(ListaArticulosPage));
            Routing.RegisterRoute(nameof(CapturaArticuloPage), typeof(CapturaArticuloPage));
            Routing.RegisterRoute(nameof(BuscarArticuloPage), typeof(BuscarArticuloPage));
            Routing.RegisterRoute(nameof(ListaTarasPage), typeof(ListaTarasPage));
            Routing.RegisterRoute(nameof(AgregaArticuloPage), typeof(AgregaArticuloPage));
            Routing.RegisterRoute(nameof(ConfiguracionPage), typeof(ConfiguracionPage));
            Routing.RegisterRoute(nameof(ConfiguracionAccesoPage), typeof(ConfiguracionAccesoPage));
        }
    }
}
