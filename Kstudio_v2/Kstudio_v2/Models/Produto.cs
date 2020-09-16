using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Kstudio_v2.Models
{
    public class Produto
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public int Quantidade { get; set; }
        [Display(Name = "Horas de Ensaio")]
        public decimal HorasDeEnsaio { get; set; }
        [Display(Name = "Preço")]
        public decimal Preco { get; set; }
        [Display(Name = "Valor Total")]
        public decimal ValorTotal { get; set; }
        public DateTime Data { get; set; }
    }
}