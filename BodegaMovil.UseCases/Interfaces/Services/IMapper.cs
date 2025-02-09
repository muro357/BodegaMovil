using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BodegaMovil.UseCases.Interfaces.Services
{
    //public  interface IMapper<Tin, Tout> where Tin : class where Tout : class
    //{
    //    Tout GetEntity(Tin entity);
        
        
    //}

    public interface IMapa
    {
        Tout GetEntity<Tin,Tout>(Tin entity) where Tout : class where Tin : class;


    }
}
