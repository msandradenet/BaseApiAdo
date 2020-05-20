using BaseApiAdo.CORE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseApiAdo.CORE.Services
{
    public interface ITokenService
    {
        string GenareteToken(Cliente cliente);
    }
}
