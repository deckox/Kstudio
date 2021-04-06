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

 
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        public string Data { get; set; }

 
        [Display(Name = "Horário")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm tt}")]
        [DataType(DataType.Time, ErrorMessage = "Data em formato inválido")]
        public string HorarioInicio { get; set; }

         
        [Display(Name = "Até")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm tt}")]
        [DataType(DataType.Time)]
        public string HorarioFinal { get; set; }

        public int Id { get; set; }
        public AgendamentoViewModel()
        {
            ClienteViewModel = new ClienteViewModel();
        }
    }
}