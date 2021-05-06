﻿using DocumentFormat.OpenXml.Office2013.Word;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nest;
using System.ComponentModel.DataAnnotations;

namespace Kstudio_v2.Models
{
    public class Comanda
    {
        public int Id { get; set; }
        public Cliente Banda { get; set; }
        public DateTime Data { get; set; }
        [Display(Name = "Hora De Inicio")]
        public decimal HoraDeInicio { get; set; }
        [Display(Name = "Hora De Ensaio")]
        public decimal HorasDeEnsaio { get; set; }
        [Display(Name = "Valor de Horas")]
        public decimal ValorDeHoras { get; set; }
        public List<DetalheComanda> Detalhes { get; set; }

        public ClienteViewModel ClienteViewModel { get; set; }

        public  Produto Produto { get; set; }

        public Comanda()
        {
            ClienteViewModel = new ClienteViewModel();
        }
    }
}