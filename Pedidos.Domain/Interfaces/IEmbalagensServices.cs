using Pedidos.Domain.Model;

namespace Pedidos.Domain.Interfaces
{
    public interface IEmbalagensServices
    {
        int CalcularVolume(Produto produto);
        List<Embalagem> SelecionarCaixas(List<Produto> produtos);
    }
}
