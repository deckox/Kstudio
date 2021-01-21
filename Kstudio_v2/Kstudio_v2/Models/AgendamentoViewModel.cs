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

        [Required(ErrorMessage = "please enter username")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        public string Data { get; set; }

        [Required(ErrorMessage = "please enter username")]
        [Display(Name = "Horário")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:hh:mm tt}")]
        [DataType(DataType.Time)]
        public string HorarioInicio { get; set; }

        [Required(ErrorMessage = "please enter username")]
        [Display(Name = "Até")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:hh:mm tt}")]
        [DataType(DataType.Time)]
        public string HorarioFinal { get; set; }
        public AgendamentoViewModel()
        {
            ClienteViewModel = new ClienteViewModel();
        }
    }
}