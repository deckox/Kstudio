using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Kstudio_v2.Models
{
    public class Agendamento
    {
        public int Id { get; set; }
        public Cliente IdCliente { get; set; }
        public DateTime Data { get; set; }
        [Display(Name = "Horário")]
        public string HorarioInicio { get; set; }
        [Display(Name = "Até")]
        public string HorarioFinal { get; set; }
    }
}