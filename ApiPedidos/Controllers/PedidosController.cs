using Microsoft.AspNetCore.Mvc;
using Pedidos.Domain.Interfaces;
using Pedidos.Domain.Model;

namespace ApiPedidos.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PedidosController: ControllerBase
    {
        private readonly IProcessaPedidoService _processaPedidoService;
   

        public PedidosController(IProcessaPedidoService processaPedidoService)
        {
            _processaPedidoService = processaPedidoService;
        }

        [HttpPost]
        public IActionResult ProcessarPedidos([FromBody] List<Pedido> pedidos)
        {

            var resposta = _processaPedidoService.ProcessarPedido(pedidos);


            return Ok(resposta);
        }
    }
}
