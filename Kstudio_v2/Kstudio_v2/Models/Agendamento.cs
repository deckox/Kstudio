using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kstudio_v2.Models
{
    public class Agendamento
    {
        public int Id { get; set; }
        public Cliente IdCliente { get; set; }
        public DateTime Data { get; set; }
        public string HorarioInicio { get; set; }
        public string HorarioFinal { get; set; }
    }
}