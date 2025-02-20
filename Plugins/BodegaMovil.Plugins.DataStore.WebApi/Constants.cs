using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BodegaMovil.Plugins.DataStore.WebApi
{
    public class Constants
    {
        public const string url = "http://localhost:5193/api";

        #region Obtener Pedido
        public const string GetPedidosSurtir = "/getpedidos/surtir";
        public const string GetPedidos = "/getpedidos";
        #endregion
    }
}
