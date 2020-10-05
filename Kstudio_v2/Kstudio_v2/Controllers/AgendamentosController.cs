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
                var clienteRepository = new ClientesRepository();

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
            return View();
        }
        
        [HttpPost]
        public ActionResult Cadastro(Agendamento agendamento)
        {

            try
            {
                var agendamentoRepository = new AgendamentosRepository();
                var clienteRepository = new ClientesRepository();

                agendamento.Cliente = clienteRepository.Carregar(agendamento.Cliente.Id);

                if (agendamento.Cliente.Id == 0)
                {
                    ViewData["mensagem"] = "Favor selecionar um cliente valido";
                }

                else if (agendamentoRepository.Salvar(agendamento) == true)
                {
                    ViewData["mensagem"] = "<h1>Usuario cadastrado com sucesso!</h1>";
                }
                else
                {
                    ViewData["mensagem"] = "<h1>DEU RUIM</h1>";
                }

                return View();
            }
            catch (Exception)
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