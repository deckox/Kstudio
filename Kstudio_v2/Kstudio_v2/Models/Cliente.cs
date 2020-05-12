using DocumentFormat.OpenXml.Wordprocessing;
using Nest;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Kstudio_v2.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Banda { get; set; }
        public string Responsavel { get; set; }
        public string EstiloMusical { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
    }
}