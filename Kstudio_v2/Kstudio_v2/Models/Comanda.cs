using DocumentFormat.OpenXml.Office2013.Word;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kstudio_v2.Models
{
    public class Comanda
    {
        public int Id { get; set; }
        public Cliente Banda { get; set; }
        public DateTime Data { get; set; }
        public string HoraDeInicio { get; set; }
        public decimal HorasDeEnsaio { get; set; }
        public decimal ValorDeHoras { get; set; }
        public List<DetalheComanda> Detalhes { get; set; }
    }
}