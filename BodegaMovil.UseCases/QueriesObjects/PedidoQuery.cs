using BodegaMovil.CoreBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BodegaMovil.UseCases.QueriesObjects
{
    //public class ProductQuery
    //{
    //    private IQueryable<PedidoDetalle> _query;

    //    public ProductQuery(IQueryable<PedidoDetalle> query)
    //    {
    //        _query = query;
    //    }

    //    public ProductQuery ByCriterio(string tipoDato, string filtro)
    //    {
    //        if(tipoDato.ToLower() == "sku")
    //            _query = _query.Where(p => p.SKU.StartsWith(filtro));

    //        if (tipoDato.ToLower() == "descripcion")
    //            _query = _query.Where(p => p.StartsWith(filtro));

    //        return this;
    //    }

    //    public ProductQuery MinPrice(decimal price)
    //    {
    //        _query = _query.Where(p => p.Price >= price);
    //        return this;
    //    }

    //    public ProductQuery OnlyAvailable()
    //    {
    //        _query = _query.Where(p => p.IsAvailable);
    //        return this;
    //    }

    //    public IQueryable<Product> Build() => _query;
    //}
}
