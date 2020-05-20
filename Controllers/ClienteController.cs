using System.Net;
using BaseApiAdo.CORE.Services;
using BaseApiAdo.CORE.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;

namespace BaseApiAdo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        IConfiguration _configuration;
        private readonly IClienteService _clienteService;
        private ITokenService _tokenService;

        public ClienteController(IConfiguration configuration, IClienteService clienteService, ITokenService tokenService)
        {
            _configuration = configuration;
            _clienteService = clienteService;
            _tokenService = tokenService;
        }

        #region Token        

        [HttpGet("GetToken")]
        public ActionResult<dynamic> GetToken(string userName, string password)
        {
            var cliente = _clienteService.GetCredentials(userName, password);

            if (cliente == null)
                return NotFound(new { message = "Usuário ou senha inválidos!" });

            var token = _tokenService.GenareteToken(cliente);

            cliente.Password = "";

            return new {
                cliente = cliente,
                token = token
            };
        }

        [HttpGet]
        [Route("anonymous")]
        [AllowAnonymous]
        public string Anonimous() => "Anônimo";

        [HttpGet]
        [Route("authenticated")]
        [Authorize]
        public string Authenticated() => string.Format("Autenticado - {0}", User.Identity.Name);

        [HttpGet]
        [Route("employee")]
        [Authorize(Roles = "employee,manager")]
        public string Employee() => "Funcionário";

        [HttpGet]
        [Route("manager")]
        [Authorize(Roles = "manager")]
        public string manager() => "Gerente";

        #endregion

        [HttpGet]
        public ActionResult<Cliente> Get()
        {
            var result = _clienteService.GetAll();

            return Ok(result);
        }

        [HttpGet("{id}", Name = "Get")]
        [Authorize]
        public ActionResult<Cliente> Get(int id)
        {
            var result = _clienteService.Get(id);

            return Ok(result);
        }

        [HttpPost]
        public ContentResult Post([FromBody] Cliente value)
        {
            var result = _clienteService.Create(value);

            if (result)
                return new ContentResult() { StatusCode = (int)HttpStatusCode.OK, Content = "Incluido com sucesso!" };
            else
                return new ContentResult() { StatusCode = (int)HttpStatusCode.BadRequest, Content = "Erro ao incluir!" };
        }

        [HttpPut("{id}")]
        public ContentResult Put(int id, [FromBody] Cliente value)
        {
            var result = _clienteService.Update(value);

            if (result)
                return new ContentResult() { StatusCode = (int)HttpStatusCode.OK, Content = "Atualizado com sucesso!" };
            else
                return new ContentResult() { StatusCode = (int)HttpStatusCode.BadRequest, Content = "Erro ao atualizar!" };
        }

        [HttpDelete("{id}")]
        public ContentResult Delete(int id)
        {
            var result = _clienteService.Delete(id);

            if (result)
                return new ContentResult() { StatusCode = (int)HttpStatusCode.OK, Content = "Deletado com sucesso!" };
            else
                return new ContentResult() { StatusCode = (int)HttpStatusCode.BadRequest, Content = "Erro ao deletar!" };
        }
    }
}
