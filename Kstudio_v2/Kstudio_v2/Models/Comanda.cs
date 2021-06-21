using DocumentFormat.OpenXml.Office2013.Word;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nest;
using System.ComponentModel.DataAnnotations;

namespace Kstudio_v2.Models
{
    public class Comanda
    {
        public int Id { get; set; }
        public string Banda { get; set; }
        public string Data { get; set; }
        [Display(Name = "Hora De Inicio")]
        public string HoraDeInicio { get; set; }
        [Display(Name = "Hora Final")]
        public string HoraFinal { get; set; }
        [Display(Name = "Hora De Ensaio")]
        public decimal HorasDeEnsaio { get; set; }
        [Display(Name = "Valor de Horas")]
        public decimal ValorDeHoras { get; set; }
        public ClienteViewModel ClienteViewModel { get; set; }
        public List<Produto> Produto { get; set; }
        public bool StatusComanda { get; set; }
        public decimal ValorTotalDaComanda { get; set; }

        public Comanda()
        {
            ClienteViewModel = new ClienteViewModel();
            Produto = new List<Produto>();
        }
    }
}