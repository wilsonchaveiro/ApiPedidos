using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pedidos.Domain.Model
{
    public class Embalagem
    {
        public int CaixaId { get; set; }
        public Produto Produto { get; set; }
    }
}
