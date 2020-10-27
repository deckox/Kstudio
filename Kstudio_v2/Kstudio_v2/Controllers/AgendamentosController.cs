using Kstudio_v2.Core.Repositories;
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

                return View(listaDeAgendamentosDoBD);
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
                var listaAgendamentos = new Cliente();
                listaAgendamentos.Agendamentos.Add(new Agendamento()); //adiciona um objeto vazio somente para startar o for


                return View(listaAgendamentos);
            }
            catch (Exception)
            {

                throw;
            }

        }

        [HttpPost]
        public ActionResult Cadastro(int Id, Cliente cliente)
        {

            try
            {

                var listaAgendamentos = cliente;
                var agendamentoRepository = new AgendamentosRepository();

                if (agendamentoRepository.Salvar(listaAgendamentos) == true)
                {
                    ViewData["mensagem"] = "<h1>Agendamento Cadastrado com sucesso!</h1>";
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewData["mensagem"] = "<h1>Não foi possível cadastrar um Agendamento!</h1>";
                    return View(listaAgendamentos);
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

                return View(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        [HttpPost]
        public ActionResult Editar(Cliente cliente, int id)
        {
            try
            {
                cliente.Agendamentos[0].Id = cliente.Id;
                var agendamentoRepository = new AgendamentosRepository();

                if (agendamentoRepository.Salvar(cliente) == true)
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

        public ActionResult Detalhes(Cliente cliente, int id)
        {
            try
            {
                var agendamentoRepository = new AgendamentosRepository();
                var dadosCliente = agendamentoRepository.Carregar(id);
                cliente = dadosCliente;
                var result = agendamentoRepository.CarregarLista(cliente);
                ViewBag.id = id;

                return View(result);
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

                return View(result);
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
    }
}