using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Kstudio_v2.Models
{
    public class PesquisaCliente : Cliente
    {
        [Display(Name = "Procure por banda,responsavel,email ou telefone")]
        public string LabelPesquisaComanda { get; set; }
        public string ProcuraPor { get; set; }

        public Cliente ResultadoCliente { get; set; }
        public Cliente Resultado { get; set; }
    }
}