using System;

namespace Kstudio_v2.Models
{
    public class DetalheAgendamento
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public string HorarioInicial { get; set; }
        public string HorarioFinal { get; set; }
    }
}