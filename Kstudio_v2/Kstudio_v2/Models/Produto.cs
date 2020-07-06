using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kstudio_v2.Models
{
    public class Produto
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public int Quantidade { get; set; }
        public int Estoque { get; set; }
        public decimal Preco { get; set; }
        public decimal ValorTotal { get; set; }
        public DateTime Data { get; set; }
    }
}