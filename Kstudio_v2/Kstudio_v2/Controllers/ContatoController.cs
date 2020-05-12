using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kstudio_v2.Models;


namespace Kstudio_v2.Controllers
{
    public class ContatoController : Controller
    {
        // GET: Contato
        public ActionResult Index()
        {
            using (ContatoModel model = new ContatoModel())
            {
                List<Contato> lista = model.Read();
                return View(lista);
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection form)
        {
            Contato contato = new Contato();
            contato.Nome = form["Nome"];
            contato.Email = form["Email"];

            using (ContatoModel model = new ContatoModel())
            {
                model.Create(contato);
                return RedirectToAction("Index");
            }
        }
    }
}