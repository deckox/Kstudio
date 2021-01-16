using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kstudio_v2.Models
{
    public class ClienteViewModel
    {
        public int Id { get; set; }
        public string Banda { get; set; }
        public string Responsavel { get; set; }
        public string EstiloMusical { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public List<AgendamentoViewModel> AgendamentosViewModel { get; set; }

        public ClienteViewModel()
        {
           
            AgendamentosViewModel = new List<AgendamentoViewModel>();
        }
    }
}