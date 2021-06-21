using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Kstudio_v2.Models
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        [Display(Name = "Preço de Custo ")]
        public decimal PrecoDeCusto { get; set; }
        [Display(Name = "Preço de Venda ")]
        public decimal PrecoDeVenda { get; set; }
        [Display(Name = "Preço")]
        public decimal Preco { get; set; }
        public int Quantidade { get; set; }

    }
}