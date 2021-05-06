using Kstudio_v2.Core.Repositories;
using Kstudio_v2.Helper;
using Kstudio_v2.Models;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Compilation;
using System.Web.Mvc;
using System.Web.UI;

namespace Kstudio_v2.Controllers
{
    public class AgendamentosController : Controller
    {
        public ActionResult Index()
        {
            try
            {
                var agendamentoRepository = new AgendamentosRepository();
                var listaDeAgendamentosDoBD = agendamentoRepository.Listar();

                var clientViewModelParser = new ClienteViewModelParser();

                var model = new ClienteViewModel();
                model.AgendamentosViewModel = clientViewModelParser.ConvertClientToClienteViewModelList(listaDeAgendamentosDoBD);
                return View(model);

               
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public ActionResult Cadastro()
        {
            try
            {
                //var listaAgendamentos = new Cliente();
                //listaAgendamentos.Agendamentos.Add(new Agendamento()); //adiciona um objeto vazio somente para startar o for

                var listaAgendamentos = new ClienteViewModel();
                listaAgendamentos.AgendamentosViewModel.Add(new AgendamentoViewModel());

                return View(listaAgendamentos);
            }
            catch (Exception)
            {

                throw;
            }

        }

        [HttpPost]
        public ActionResult Cadastro(ClienteViewModel clienteViewModel)
        {

            try
            {
                var newclienteViewModel = new ClienteViewModelParser();
                var convertToCliente = newclienteViewModel.clienteViewModelParser(clienteViewModel);
                var agendamentoRepository = new AgendamentosRepository();

                var resultDisponivel = agendamentoRepository.ValidarHorarioDisponivel(convertToCliente);

                if (clienteViewModel.AgendamentosViewModel.Count != convertToCliente.Agendamentos.Count)
                {
                    ViewData["mensagem"] = "<h1>Não foi possível cadastrar um Agendamento!</h1>";
                    return View(clienteViewModel);
                }



                if (resultDisponivel && agendamentoRepository.Salvar(convertToCliente))
                {
                    ViewData["mensagem"] = "<h1>Agendamento Cadastrado com sucesso!</h1>";
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewData["mensagem"] = "<h1>Agendamento Cadastrado com sucesso!</h1>";
                    return RedirectToAction("Index");
                }


            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public ActionResult Editar(int id)
        {
            try
            {
                var agendamentoRepository = new AgendamentosRepository();
                var result = agendamentoRepository.Carregar(id);

                var clientViewModelParser = new ClienteViewModelParser();
                var clientParsed = clientViewModelParser.clienteParser(result);


                return View(clientParsed);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        [HttpPost]
        public ActionResult Editar(ClienteViewModel clienteViewModel)
        {
            try
            {
                var newclienteViewModel = new ClienteViewModelParser();
                var convertToCliente = newclienteViewModel.clienteViewModelParser(clienteViewModel);
                var agendamentoRepository = new AgendamentosRepository();

                if (convertToCliente.Banda == null || convertToCliente.Agendamentos.Count == 0)
                {
                    ViewData["mensagem"] = "<h1>DEU RUIM</h1>";
                }

                else if (agendamentoRepository.Salvar(convertToCliente))
                {
                    ViewData["mensagem"] = "<h1>Agendamento alterado com sucesso!</h1>";
                }
                else
                {
                    ViewData["mensagem"] = "<h1>DEU RUIM</h1>";
                }

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        public ActionResult Detalhes(int id)
        {
            try
            {
                var agendamentoRepository = new AgendamentosRepository();
                var dadosCliente = agendamentoRepository.Carregar(id);

                var clientViewModelParser = new ClienteViewModelParser();
                var clientParsed = clientViewModelParser.clienteParser(dadosCliente);

                //  cliente = dadosCliente;
                // var result = agendamentoRepository.CarregarLista(cliente);
                // ViewBag.id = id;

                return View(clientParsed);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        public ActionResult Deletar(int id)
        {
            try
            {
                var agendamentoRepository = new AgendamentosRepository();

                var result = agendamentoRepository.Carregar(id);

                var clientViewModelParser = new ClienteViewModelParser();
                var clientParsed = clientViewModelParser.clienteParser(result);

                return View(clientParsed);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        [HttpPost]
        public ActionResult Deletar(int id, FormCollection collection)
        {

            try
            {
                var agendamentoRepository = new AgendamentosRepository();


                if (agendamentoRepository.Excluir(id) == true)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    var result = agendamentoRepository.Carregar(id);
                    ViewData["mensagem"] = "<h1>DEU RUIM</h1>";
                    return View(result);

                }
            }
            catch (Exception msg)
            {

                throw;
            }

        }

        public string BuscarClientesAutocomplete(string value)
        {
            try
            {
                var clienteRepository = new ClientesRepository();

                var listaDeClientesDoBD = clienteRepository.BuscarIdDaBanda(value);

                var jsonResult = JsonConvert.SerializeObject(listaDeClientesDoBD);

                return jsonResult;
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public string BuscarAgendamentosAutocomplete(string value)
        {
            try
            {
                var split = value.Split('-');
                var id = int.Parse(split[0]);

                var agendamentoRepository = new AgendamentosRepository();

                var listaDeClientesDoBD = agendamentoRepository.BuscarIdDaBanda(id);

                var jsonResult = JsonConvert.SerializeObject(listaDeClientesDoBD);


                return jsonResult;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public string BuscarAgendamentosPorData(string value)
        {
            try
            {
                var agendamentoRepository = new AgendamentosRepository();

                var listaAgendamentosDoBD = agendamentoRepository.BuscarAgendamentosPorData(value);

                var jsonResult = JsonConvert.SerializeObject(listaAgendamentosDoBD);


                return jsonResult;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public string BuscarAgendamentosDiariosAutocomplete()
        {
            try
            {

                var agendamentoRepository = new AgendamentosRepository();

                var listaDeClientesDoBD = agendamentoRepository.BuscarAgendamentoDoDia();

                var jsonResult = JsonConvert.SerializeObject(listaDeClientesDoBD);


                return jsonResult;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }

}