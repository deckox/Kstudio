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
        public Cliente Banda { get; set; }

        public List<DetalheAgendamento> Detalhes { get; set; }

         
    }
}