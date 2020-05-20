using BaseApiAdo.CORE.Services;
using BaseApiAdo.INFRA.Repository;
using BaseApiAdo.CORE.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace BaseApiAdo.INFRA.Services
{
    public class ClienteService : SqlAdapter, IClienteService
    {
        ClienteRepository _cliente;
        IConfiguration _configuration;

        public ClienteService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Cliente GetCredentials(string userName, string password)
        {
            OpenConnection(_configuration);

            _cliente = new ClienteRepository(this._sqlConnection, this._sqlTransaction);

            return _cliente.GetCredentials(userName, password);
        }

        public IEnumerable<Cliente> GetAll()
        {
            try
            {
                OpenConnection(_configuration);

                _cliente = new ClienteRepository(this._sqlConnection, this._sqlTransaction);

                return _cliente.GetAll();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                Dispose();
            }
        }

        public bool Create(Cliente cliente)
        {
            try
            {
                OpenConnection(_configuration);

                OpenTransaction();

                _cliente = new ClienteRepository(this._sqlConnection, this._sqlTransaction);

                bool ret = _cliente.Create(cliente);

                if (ret)
                    SaveChanges();
                else
                    Dispose();

                return ret;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                Dispose();
            }          
        }

        public bool Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public Cliente Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public bool Update(Cliente cliente)
        {
            throw new System.NotImplementedException();
        }        
    }
}
