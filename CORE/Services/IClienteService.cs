using BaseApiAdo.CORE.Models;
using System.Collections.Generic;

namespace BaseApiAdo.CORE.Services
{
    public interface IClienteService
    {
        IEnumerable<Cliente> GetAll();
        Cliente Get(int id);
        Cliente GetCredentials(string userName, string password);
        bool Create(Cliente cliente);
        bool Update(Cliente cliente);
        bool Delete(int id);
    }
}
