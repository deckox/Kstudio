using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kstudio_v2.Models
{
    public class Agendamento : Cliente
    {
        public DateTime Data { get; set; }
        public int Horario { get; set; }

         
    }
}