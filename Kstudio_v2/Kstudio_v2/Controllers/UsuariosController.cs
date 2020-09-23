using Kstudio_v2.Core.Repositories;
using Kstudio_v2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kstudio_v2.Controllers
{
    public class UsuariosController : Controller
    {
        public ActionResult Index()
        {
            try
            {
                var usuariosRepository = new UsuariosRepository();
                var listaAuxBd = usuariosRepository.Listar();


                var result = new List<Usuario>();
                result = listaAuxBd;

                return View(result);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public ActionResult Index(string campoPesquisar, Usuario usuario)
        {
            try
            {
                var aux = new Usuario();
           

                var usuariosRepository = new UsuariosRepository();
               // var listaAuxBd = usuariosRepository.BuscarUsuario();


                var result = new List<Usuario>();
              //  result = listaAuxBd;

                return View(result);
            }
            catch (Exception)
            {

                throw;
            }

        }

        public ActionResult Cadastro()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Cadastro(Usuario usuario)
        {
            try
            {
                var usuariosRepository = new UsuariosRepository();

                if (usuariosRepository.Salvar(usuario) == true)
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

        public ActionResult Editar(int id)
        {
            try
            {
                var usuariosRepository = new UsuariosRepository();
                var user = usuariosRepository.Carregar(id);
                return View(user);
            }
            catch (Exception)
            {

                throw;
            }

        }

        [HttpPost]

        public ActionResult Editar(Usuario usuario)
        {
            try
            {
                var usuariosRepository = new UsuariosRepository();
                var user = usuariosRepository.Salvar(usuario);

                return View();
            }
            catch (Exception)
            {

                throw;
            }

        }

        public ActionResult Detalhes(int id)
        {
            try
            {
                var usuariosRepository = new UsuariosRepository();
                var user = usuariosRepository.Carregar(id);
                return View(user);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ActionResult Deletar(int id)
        {
            try
            {
                var usuariosRepository = new UsuariosRepository();
                var user = usuariosRepository.Carregar(id);
                return View(user);
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
                var usuariosRepository = new UsuariosRepository();

                if (usuariosRepository.Excluir(id) == true)
                {
                    ViewData["mensagem"] = "<h1>Usuario excluído com sucesso!</h1>";
                }
                else
                {
                    ViewData["mensagem"] = "<h1>DEU RUIM</h1>";
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
