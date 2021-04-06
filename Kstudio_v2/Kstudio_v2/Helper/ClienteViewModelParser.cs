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
            clienteViewModel.AgendamentosViewModel.Add(new AgendamentoViewModel());

            clienteViewModel.Banda = cliente.Banda;
            clienteViewModel.Email = cliente.Email;
            clienteViewModel.EstiloMusical = cliente.EstiloMusical;
            clienteViewModel.Id = cliente.Id;
            clienteViewModel.Responsavel = cliente.Responsavel;
            clienteViewModel.Telefone = cliente.Telefone;

            clienteViewModel.AgendamentosViewModel.FirstOrDefault().Data = cliente.Agendamentos.FirstOrDefault().Data.ToShortDateString();
            clienteViewModel.AgendamentosViewModel.FirstOrDefault().HorarioInicio = cliente.Agendamentos.FirstOrDefault().HorarioInicio.ToString("HH:mm:ss");
            clienteViewModel.AgendamentosViewModel.FirstOrDefault().HorarioFinal = cliente.Agendamentos.FirstOrDefault().HorarioFinal.ToString("HH:mm:ss");
            clienteViewModel.AgendamentosViewModel.FirstOrDefault().Id = cliente.Agendamentos.FirstOrDefault().Id;

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
                    DateTime.Parse(clienteViewModel.AgendamentosViewModel[i].HorarioInicio) < DateTime.Parse(clienteViewModel.AgendamentosViewModel[i].HorarioFinal) &&
                    DateTime.Parse(clienteViewModel.AgendamentosViewModel[i].HorarioFinal) > DateTime.Parse(clienteViewModel.AgendamentosViewModel[i].HorarioInicio))
                 
                {

                    if (DateTime.Parse(clienteViewModel.AgendamentosViewModel[i].Data) > DateTime.Today)
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

                    else if (DateTime.Parse(clienteViewModel.AgendamentosViewModel[i].Data) >= DateTime.Today && TimeSpan.Parse(clienteViewModel.AgendamentosViewModel[i].HorarioInicio) >= TimeSpan.FromHours(DateTime.Now.Hour) &&
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

            }

            return cliente;
        }

        public List<AgendamentoViewModel> ConvertClientToClienteViewModelList(List<Cliente> listaClientes)
        {
            var result = new List<AgendamentoViewModel>();

            AgendamentoViewModel agendamento = null;

            foreach (var cliente in listaClientes)
            {
                agendamento = new AgendamentoViewModel();
                agendamento.ClienteViewModel.Banda = cliente.Banda;
                agendamento.ClienteViewModel.Email = cliente.Email;
                agendamento.ClienteViewModel.EstiloMusical = cliente.EstiloMusical;
                agendamento.ClienteViewModel.Id = cliente.Id;
                agendamento.ClienteViewModel.Responsavel = cliente.Responsavel;
                agendamento.ClienteViewModel.Telefone = cliente.Telefone;

                agendamento.Data = cliente.Agendamentos.FirstOrDefault().Data.ToShortDateString();
                agendamento.HorarioInicio = cliente.Agendamentos.FirstOrDefault().HorarioInicio.ToString("HH:mm:ss");
                agendamento.HorarioFinal = cliente.Agendamentos.FirstOrDefault().HorarioFinal.ToString("HH:mm:ss");
                agendamento.Id = cliente.Agendamentos.FirstOrDefault().Id;

                result.Add(agendamento);
               
            }

            return result;
        }

    }
}