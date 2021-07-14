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

        public ActionResult Editar(int id)
        {
            try
            {
                var comandasRepository = new ComandasRepository();
                var comanda = comandasRepository.Carregar(id);
                return View(comanda);
            }
            catch (Exception)
            {

                throw;
            }

        }

        [HttpPost]
        public ActionResult Editar(Comanda comanda)
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

        public ActionResult Detalhes(int id)
        {
            try
            {
                var comandasRepository = new ComandasRepository();
                var comanda = comandasRepository.Carregar(id);
                return View(comanda);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public ActionResult Deletar(int id)
        {
            try
            {
                var comandasRepository = new ComandasRepository();
                var comanda = comandasRepository.Carregar(id);
                return View(comanda);
            }
            catch (Exception)
            {

                throw;
            }


        }

        [HttpPost]
        public ActionResult Deletar(int id, FormCollection collection)
        {
            try
            {
                var comandaRepository = new ComandasRepository();

                if (comandaRepository.Excluir(id))
                {
                    ViewData["mensagem"] = "<h1>Usuario cadastrado com sucesso!</h1>";
                }
                else
                {
                    ViewData["mensagem"] = "<h1>DEU RUIM</h1>";
                    return View();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


    }

}
