using Pedidos.Domain.Model;
using Pedidos.Domain.ReturnPedidos;

namespace Pedidos.Domain.Interfaces
{
    public interface IProcessaPedidoService
    {
        List<PedidosReturn> ProcessarPedido(List<Pedido> pedido);
        List<PedidosReturn> ProcessaEmbalagem(List<RespostaPedido> respostaPedidos);
    }
}
