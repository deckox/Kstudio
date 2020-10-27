using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kstudio_v2.Core.Repositories;
using Kstudio_v2.Models;
using Microsoft.Ajax.Utilities;

namespace Kstudio_v2.Controllers
{
   


    public class ClientesController : Controller
    {
        PesquisaCliente globalCliente;

        // GET: CLientes
        //public ActionResult Index()
        //{
        //    var clientesRepository = new ClientesRepository();
        //    var result = clientesRepository.Listar();

        //    return View(result);
        //}

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
        public ActionResult Detalhes(int id)
        {
            var clientesRepository = new ClientesRepository();
            var cliente = clientesRepository.Carregar(id);
            return View(cliente);
            
        }


        // GET: Clientes/Pesquisa/5
        public ActionResult Index()
        {
            var clientesRepository = new ClientesRepository();
            var result = clientesRepository.Listar();

            var model = new PesquisaCliente();
            model.Resultado = result;
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(PesquisaCliente cliente)
        {
            var clientesRepository = new ClientesRepository();
            var result = clientesRepository.ListarClientesDoCampoPesquisa(cliente);


            cliente.Resultado = result;
            return View(cliente);
        }
        
        // GET: Clientes/Edit/5
        public ActionResult Editar(int id)
        {
            var clientesRepository = new ClientesRepository();
            var cliente = clientesRepository.Carregar(id);
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        [HttpPost]
        public ActionResult Editar(Cliente cliente)
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
        public ActionResult Deletar(int id)
        {
            var clientesRepository = new ClientesRepository();
            var cliente = clientesRepository.Carregar(id);
            return View(cliente);
        }

        [HttpPost]
        public ActionResult Deletar(int id, FormCollection collection)
        {
            try
            {
                var clientesRepository = new ClientesRepository();
                clientesRepository.Excluir(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}