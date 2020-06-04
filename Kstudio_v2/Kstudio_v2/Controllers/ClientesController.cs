using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kstudio_v2.Core.Repositories;
using Kstudio_v2.Models;

namespace Kstudio_v2.Controllers
{
    public class ClientesController : Controller
    {
        // GET: CLientes
        public ActionResult Index()
        {
            var clientesRepository = new ClientesRepository();
            var result = clientesRepository.Listar();

            return View(result);
        }

        public ActionResult Cadastro()
        {
            return View (new Cliente());
        }

        [HttpPost]

        public ActionResult Cadastro(Cliente cliente)
        {
            var clientesRepository = new ClientesRepository();
            var isClienteDuplicated = clientesRepository.IsClienteDuplicated(cliente);
            bool result;

            if (isClienteDuplicated == true)
            {
                var showDuplicatedCliente = clientesRepository.MostraClienteDuplicado(cliente);

                ViewData["mensagem"] = "<h3> Cliente existente: " + showDuplicatedCliente.Banda + " " + showDuplicatedCliente.Responsavel
                    + " " + showDuplicatedCliente.Email + " " + showDuplicatedCliente.Telefone + ", Favor verificar </h3>"; 
            }

            else
            {
                result = clientesRepository.Salvar(cliente);

                if (result == true)
                {
                    ViewData["mensagem"] = "<h3> Cliente cadastrado com sucesso!</h3>";
                }

                else
                {
                    ViewData["mensagem"] = "<h3> Nenhum campo pode estar vazio ou em branco, favor preencher os seguintes campos: Banda, Responsavel, Email e Telefone </h3>";
                }

            }
 
            return View(cliente);
        }

        // GET: Usuarios/Details/5
        public ActionResult Details(int id)
        {
            var clientesRepository = new ClientesRepository();
            var cliente = clientesRepository.Carregar(id);
            return View(cliente);
        }


        // GET: Clientes/Pesquisa/5
        public ActionResult Pesquisa()
        {
            var clientesRepository = new ClientesRepository();
            var result = clientesRepository.Listar();

            var model = new PesquisaCliente();
            model.Resultado = result;
            return View(model);
        }

        [HttpPost]
        public ActionResult Pesquisa(PesquisaCliente cliente)
        {
            var clientesRepository = new ClientesRepository();
            var result = clientesRepository.ListarClientesDoCampoPesquisa(cliente);
            cliente.Resultado = result;
            return View(cliente);
        }
        // GET: Clientes/Details/5
        public ActionResult Comanda()
        {
            return View(new Cliente());
        }

        [HttpPost]
        public ActionResult Comanda(Cliente cliente)
        {
            var clientesRepository = new ClientesRepository();
          //  var validateCliente = clientesRepository.IsAnyClienteNullOrEmpty(cliente);
            Cliente result = null;
           
       /*     if (validateCliente == true)
            {
                ViewData["mensagem"] = "<h3> Não Existe nenhuma informação cadastrada </h3>";
            }*/

            //else
            //{
            //    result = clientesRepository.ListaCliente(cliente);

            //    if (result == null)
            //    {
            //        ViewData["mensagem"] = "<h3> Não Existe nenhuma informação cadastrada </h3>";
            //    }

            //}
          
           
 
            return View(result);
        }

        // GET: Clientes/Create
        public ActionResult Create()
        {
            return View(new Cliente());
        }

        // POST: Clientes/Create
        [HttpPost]
        public ActionResult Create(Cliente cliente)
        {
            try
            {
                var clientesRepository = new ClientesRepository();
                clientesRepository.Salvar(cliente);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(cliente);
            }
        }

        // GET: Clientes/Edit/5
        public ActionResult Edit(int id)
        {
            var clientesRepository = new ClientesRepository();
            var cliente = clientesRepository.Carregar(id);
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        [HttpPost]
        public ActionResult Edit(Cliente cliente)
        {
            try
            {
                var clientesRepository = new ClientesRepository();
                var result = clientesRepository.Salvar(cliente);
                
                if (result == true)
                {
                    ViewData["mensagem"] = "<h3> Cliente alterado com sucesso!</h3>";
                }

                else
                {
                    ViewData["mensagem"] = "<h3> Ocorreu um erro, " + "result da query = " + result + "</h3>";
                }
               
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: Clientes/Delete/5
        public ActionResult Delete(int id)
        {
            var clientesRepository = new ClientesRepository();
            var cliente = clientesRepository.Carregar(id);
            return View(cliente);
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                var clientesRepository = new ClientesRepository();
                clientesRepository.Excluir(id);

                return RedirectToAction("Pesquisa");
            }
            catch
            {
                return View();
            }
        }
    }
}