using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kstudio_v2.Models
{
    public class Produto
    {
        public int CodigoDoProduto { get; set; }

        public string Bebida { get; set; }

        public string BebidaNaoAlcoolica { get; set; }

        public string Comida { get; set; }

        public string Equipamento { get; set; }

        public string Jogos { get; set; }
       
    }
}