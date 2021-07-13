using Kstudio_v2.Core.Repositories;
using Kstudio_v2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kstudio_v2.Controllers
{
    public class ComandasController : Controller
    {
        public ActionResult Index()
        {
            try
            {
                var listaDeComandas = new ComandasRepository();
                var listaDeComandasDoBd = listaDeComandas.Listar();

                return View(listaDeComandasDoBd);
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        [HttpPost]
        public ActionResult Index(string campoPesquisar)
        {
            try
            {
                var usuariosRepository = new UsuariosRepository();
                var listaAuxBd = usuariosRepository.BuscarUsuario(campoPesquisar);

                return View(listaAuxBd);
            }
            catch (Exception)
            {

                throw;
            }

        }

        public ActionResult Cadastro()
        {
            var listaAgendamentos = new Comanda();
            listaAgendamentos.Produto.Add(new Produto());       
            return View(listaAgendamentos);
        }

        [HttpPost]
        public ActionResult Cadastro(Comanda comanda)
        {
            try
            {
                var comandaRepository = new ComandasRepository();

                if (comandaRepository.Salvar(comanda))
                {
                    ViewData["mensagem"] = "<h1>Usuario cadastrado com sucesso!</h1>";
                }
                else
                {
                    ViewData["mensagem"] = "<h1>DEU RUIM</h1>";
                    return View(comanda);
                }

                return View();
            }
            catch (Exception e)
            {

                throw;
            }

        }

        //public ActionResult Editar(int id)
        //{
        //    try
        //    {
        //        var usuariosRepository = new UsuariosRepository();
        //        var user = usuariosRepository.Carregar(id);
        //        return View(user);
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }

        //}

        //[HttpPost]

        //public ActionResult Editar(Usuario usuario)
        //{
        //    try
        //    {
        //        var usuariosRepository = new UsuariosRepository();
        //        var user = usuariosRepository.Salvar(usuario);

        //        return View();
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }

        //}

        //public ActionResult Detalhes(int id)
        //{
        //    try
        //    {
        //        var usuariosRepository = new UsuariosRepository();
        //        var user = usuariosRepository.Carregar(id);
        //        return View(user);
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        //public ActionResult Deletar(int id)
        //{
        //    try
        //    {
        //        var usuariosRepository = new UsuariosRepository();
        //        var user = usuariosRepository.Carregar(id);
        //        return View(user);
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }


        //}

        //[HttpPost]
        //public ActionResult Deletar(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        var usuariosRepository = new UsuariosRepository();

        //        if (usuariosRepository.Excluir(id) == true)
        //        {
        //            ViewData["mensagem"] = "<h1>Usuario excluído com sucesso!</h1>";
        //        }
        //        else
        //        {
        //            ViewData["mensagem"] = "<h1>DEU RUIM</h1>";
        //        }

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}


    }

}
