using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Kstudio_v2.Models
{

    public class Detalhe
    {
        [Display(Name = "Descricão")]
        public string Descricao { get; set; } 
        
        [Display(Name = "Quantidade")]
        public int Quantidade { get; set; }

        [Display(Name = "Preço")]
        public decimal Preco { get; set; }
    }

    public class DetalheDoProduto
    {
        public int Id { get; set; }
        public List<Detalhe> Detalhes { get; set; }

        [Display(Name = "Horas De Ensaio")]
        public decimal HorasDeEnsaio { get; set; }
        
        [Display(Name = "Valor Total")]
        public decimal ValorTotal { get; set; }
        public DateTime Data { get; set; }

        public DetalheDoProduto()
        {
            Detalhes = new List<Detalhe>();
            Detalhes.Add(new Detalhe());
        }
    }
}