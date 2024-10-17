using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pedidos.Domain.Model
{
    public class CaixaProdutos
    {
        public int pedido_id {  get; set; }
        public int caixa_Id {  get; set; }
        public string produto_Id {  get; set; }
    }
}
