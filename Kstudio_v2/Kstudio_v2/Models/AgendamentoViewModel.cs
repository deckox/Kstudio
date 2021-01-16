using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Kstudio_v2.Models
{
    public class AgendamentoViewModel
    {
        public ClienteViewModel ClienteViewModel { get; set; }
        public string Data { get; set; }
        [Display(Name = "Horário")]
        public string HorarioInicio { get; set; }
        [Display(Name = "Até")]
        public string HorarioFinal { get; set; }
        public AgendamentoViewModel()
        {
            ClienteViewModel = new ClienteViewModel();
        }
    }
}