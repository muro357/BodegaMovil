using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BodegaMovil.CoreBusiness.Enums
{
    public enum FormaDeCalculo 
    { 
        Venta, 
        Ultimo_Pedido, 
        Sin_Historial, 
        No_Inventariada, 
        Pedido_Especial, 
        Articulo_Agregado, 
        Ultimo_Sugerido 
    }
}
