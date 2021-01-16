using Kstudio_v2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kstudio_v2.Helper
{
    public class ClienteViewModelParser
    {
        public ClienteViewModel clienteParser(Cliente cliente)
        {
            var clienteViewModel = new ClienteViewModel();
            return clienteViewModel;
        }

        public Cliente clienteViewModelParser(ClienteViewModel clienteViewModel)
        {
            
            var cliente = new Cliente();
            cliente.Agendamentos.Add(new Agendamento());

            for (var i = 0; i < cliente.Agendamentos.Count; i++)
            {
                var data = DateTime.Parse(clienteViewModel.AgendamentosViewModel[i].Data).ToShortDateString();
                var horarioInicio = DateTime.Parse(clienteViewModel.AgendamentosViewModel[i].HorarioInicio).ToString("hh:mm:ss");
                var horarioFinal = DateTime.Parse(clienteViewModel.AgendamentosViewModel[i].HorarioFinal).ToString("hh:mm:ss");

                cliente.Agendamentos[i].Data = DateTime.Parse(data);
                cliente.Agendamentos[i].HorarioInicio = DateTime.Parse(horarioInicio);
                cliente.Agendamentos[i].HorarioFinal = DateTime.Parse(horarioFinal);
                cliente.Id = clienteViewModel.Id;
            }

            return cliente;
        }
    }
}