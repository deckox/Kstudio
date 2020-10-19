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

namespace Kstudio_v2.Controllers
{
    public class AgendamentosController: Controller
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
                listaAgendamentos.Agendamentos.Add(new Agendamento());


                return View(listaAgendamentos);
            }
            catch (Exception)
            {

                throw;
            }
           
        }
        
        [HttpPost]
        public ActionResult Cadastro(Cliente cliente)
        {

            try
            {
                var agendamentoRepository = new AgendamentosRepository();
                var clienteRepository = new ClientesRepository();

              
                // agendamento[0].Cliente = clienteRepository.Carregar(agendamento[0].Cliente.Id);

                //if (agendamento[0].Cliente.Id == 0)
                //{
                //    ViewData["mensagem"] = "Favor selecionar um cliente valido";
                //}

                //else if (agendamentoRepository.Salvar(agendamento) == true)
                //{
                //    ViewData["mensagem"] = "<h1>Usuario cadastrado com sucesso!</h1>";
                //}
                //else
                //{
                //    ViewData["mensagem"] = "<h1>DEU RUIM</h1>";
                //}

                return View();
            }
            catch (Exception)
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
        public ActionResult Editar(Agendamento agendamento, int id)
        {
            try
            {
                var agendamentoRepository = new AgendamentosRepository();
                var result = agendamentoRepository.Carregar(id);
                agendamento.Cliente = result.Cliente;
     

                if (agendamentoRepository.Salvar(agendamento) == true)
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

        public ActionResult Detalhes(Agendamento agendamento, int id)
        {
            try
            {
                var agendamentoRepository = new AgendamentosRepository();
                var dadosCliente = agendamentoRepository.Carregar(id);
                agendamento = dadosCliente;
                var result = agendamentoRepository.CarregarLista(agendamento);
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
            var agendamentoRepository = new AgendamentosRepository();
            agendamentoRepository.Excluir(id);


            return RedirectToAction("Index");
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