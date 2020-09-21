using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kstudio_v2.Models
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal PrecoDeCusto { get; set; }
        public decimal PrecoDeVenda { get; set; }
        public decimal Preco { get; set; }

    }
}