using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kstudio_v2.Core.Repositories;
using Kstudio_v2.Models;

namespace Kstudio_v2.Controllers
{
    public class ProdutosController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        //public ActionResult Comanda()
        //{
        //    return View(new DetalheComanda());
        //}

        //[HttpPost]
        //public ActionResult Comanda(DetalheComanda produto)
        //{
        //    var produtoRepository = new ProdutosRepository();
        //    var result = produtoRepository.Salvar(produto);

        //    return View(produto);

        //}
    }
}