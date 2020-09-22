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
            return View();
        }

        [HttpPost]
        public ActionResult Index(Usuario usuario)
        {
            try
            {
                var usuariosRepository = new UsuariosRepository();

                if (usuariosRepository.ValidarLogin(usuario) == true)
                {
                    ViewData["mensagem"] = "<h1>Usuario cadastrado com sucesso!</h1>";
                    return RedirectToAction("ListaUsuarios");
                }
                else
                {
                    ViewData["mensagem"] = "<h1>DEU RUIM</h1>";
                    return View();
                }
            }

            catch (Exception)
            {

                return View();
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

        public ActionResult ListaUsuarios()
        {
            try
            {
                var usuariosRepository = new UsuariosRepository();
                var listaAuxBd = usuariosRepository.Listar();


                var result = new Usuario();
                result.ListaDeUsuario = listaAuxBd;

                return View(result);
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

                return View();
            }
            catch
            {
                return View();
            }
        }

       
    }

}
