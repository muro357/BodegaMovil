using BodegaMovil.WebAPI.EndPoints;
using Microsoft.Extensions.DependencyInjection;
using BodegaMovil.CoreBusiness;
using BodegaMovil.UseCases;
using MySql.Data.MySqlClient;
using Dapper;
namespace BodegaMovil.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            //builder.Services.AddAuthorization();
            var connectionString = builder.Configuration.GetConnectionString("cnnMySql");

            builder.Services.AddScoped(_ => new MySqlConnection(connectionString));


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            //app.UseAuthorization();

            AreaEndPoints.MapAreaRoutes(app);
            TiendaEndPoints.MapTiendaRoutes(app);
            ArticuloEndPoints.MapArticuloRoutes(app);
            UsuarioEndPoints.MapUsuarioRoutes(app);
            GetPedidoEndPoints.MapGetPedidoRoutes(app);
            PedidoEndPoints.MapPedidoRoutes(app);

            app.Run();
        }
    }
}
