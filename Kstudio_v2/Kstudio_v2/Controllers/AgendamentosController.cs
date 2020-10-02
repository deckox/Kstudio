using Kstudio_v2.Core.Repositories;
using Kstudio_v2.Models;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kstudio_v2.Controllers
{
    public class AgendamentosController: Controller
    {
        public ActionResult Index()
        {
            return View();
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

                if (agendamento.IdCliente == null)
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

        public ActionResult BuscarIdBanda()
        {
            

            return RedirectToAction("Cadastro");
        }

        public string BuscarClientesAutocomplete(string value)
        {
            var agendamentoRepository = new AgendamentosRepository();

            var listaDeClientesDoBD = agendamentoRepository.BuscarIdDaBanda(value);

            var jsonResult = JsonConvert.SerializeObject(listaDeClientesDoBD);

            return jsonResult;
        }
    }
}