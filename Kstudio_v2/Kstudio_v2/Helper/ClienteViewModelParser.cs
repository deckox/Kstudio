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


            for (var i = 0; i < clienteViewModel.AgendamentosViewModel.Count; i++)
            {

                var calculoIntervaloDeHoras = TimeSpan.Parse(clienteViewModel.AgendamentosViewModel[i].HorarioFinal) - TimeSpan.Parse(clienteViewModel.AgendamentosViewModel[i].HorarioInicio);


                if (calculoIntervaloDeHoras.TotalHours >= 1 &&
                    DateTime.Parse(clienteViewModel.AgendamentosViewModel[i].HorarioInicio) < DateTime.Parse("22:00") &&
                    DateTime.Parse(clienteViewModel.AgendamentosViewModel[i].HorarioFinal) <= DateTime.Parse("22:00") &&
                    DateTime.Parse(clienteViewModel.AgendamentosViewModel[i].HorarioInicio) >= DateTime.Parse("8:00") && 
                    DateTime.Parse(clienteViewModel.AgendamentosViewModel[i].HorarioFinal) >= DateTime.Parse("9:00") &&
                    DateTime.Parse(clienteViewModel.AgendamentosViewModel[i].Data) >= DateTime.Today && 
                    DateTime.Parse(clienteViewModel.AgendamentosViewModel[i].HorarioInicio) < DateTime.Parse(clienteViewModel.AgendamentosViewModel[i].HorarioFinal) &&
                    DateTime.Parse(clienteViewModel.AgendamentosViewModel[i].HorarioFinal) > DateTime.Parse(clienteViewModel.AgendamentosViewModel[i].HorarioInicio) &&
                    TimeSpan.Parse(clienteViewModel.AgendamentosViewModel[i].HorarioInicio) >= TimeSpan.FromHours(DateTime.Now.Hour) &&
                    TimeSpan.Parse(clienteViewModel.AgendamentosViewModel[i].HorarioFinal) > TimeSpan.FromHours(DateTime.Now.Hour))
                {
                    var data = DateTime.Parse(clienteViewModel.AgendamentosViewModel[i].Data).ToShortDateString();
                    var horarioInicio = DateTime.Parse(clienteViewModel.AgendamentosViewModel[i].HorarioInicio).ToString("HH:mm:ss");
                    var horarioFinal = DateTime.Parse(clienteViewModel.AgendamentosViewModel[i].HorarioFinal).ToString("HH:mm:ss");

                    cliente.Agendamentos.Add(new Agendamento());
                    cliente.Agendamentos[i].Data = DateTime.Parse(data);
                    cliente.Agendamentos[i].HorarioInicio = DateTime.Parse(horarioInicio);
                    cliente.Agendamentos[i].HorarioFinal = DateTime.Parse(horarioFinal);
                    cliente.Id = clienteViewModel.Id;
                }

            }

            return cliente;
        }



    }
}