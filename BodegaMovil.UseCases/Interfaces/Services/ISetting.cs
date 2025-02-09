using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BodegaMovil.UseCases.Interfaces.Services
{
    public interface ISetting
    {
        Task<string> Get { get; }

        Task Update(string url);
    }
}
