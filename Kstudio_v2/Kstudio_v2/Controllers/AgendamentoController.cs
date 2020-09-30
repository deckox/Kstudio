using Kstudio_v2.Core.Repositories;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kstudio_v2.Controllers
{
    public class AgendamentoController: Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Cadastro()
        {
            return View();
        }

        public ActionResult BuscarIdBanda()
        {
            

            return RedirectToAction("Cadastro");
        }

        [HttpPost]
        public ActionResult BuscarIdBanda(string campoPesquisa)
        {
            var agendamentoRepository = new AgendamentosRepository();

            var listaDeClientesDoBD = agendamentoRepository.BuscarIdDaBanda(campoPesquisa);

            return View(listaDeClientesDoBD);
        }
    }
}