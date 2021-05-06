using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kstudio_v2.Core.Repositories;
using Kstudio_v2.Models;
using Newtonsoft.Json;

namespace Kstudio_v2.Controllers
{
    public class ProdutosController : Controller
    {
        public ActionResult Index()
        {
            try
            {
                var produtoRepository = new ProdutosRepository();
                var listaDeProdutosDoBD = produtoRepository.Listar();

                return View(listaDeProdutosDoBD);
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
                var produtoRepository = new ProdutosRepository();
                var result = produtoRepository.BuscarProdutos(campoPesquisar);

                return View(result);

            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public ActionResult Cadastro()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Cadastro(Produto produto)
        {
            try
            {
                var produtoRepository = new ProdutosRepository();

                if (produtoRepository.Salvar(produto) == true)
                {
                    ViewData["mensagem"] = "<h1>Usuario cadastrado com sucesso!</h1>";
                }
                else
                {
                    ViewData["mensagem"] = "<h1>DEU RUIM</h1>";
                }

                return View();
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public ActionResult Deletar(int id)
        {
            try
            {
                var produtoRepository = new ProdutosRepository();
                var result = produtoRepository.Carregar(id);

                return View(result);
            }
            catch (Exception)
            {

                throw;
            }

        }

        [HttpPost]
        public ActionResult Deletar(int Id, FormCollection collection)
        {
            try
            {
                var produtoRepository = new ProdutosRepository();
                if (produtoRepository.Excluir(Id) == true)
                {
                    ViewData["mensagem"] = "<h1>Usuario cadastrado com sucesso!</h1>";
                    return RedirectToAction("index");
                }

                else
                {
                    ViewData["mensagem"] = "<h1>DEU RUIM</h1>";
                    return View();
                }

            }
            catch (Exception ex)
            {

                throw;
            }


        }

        public ActionResult Editar(int Id)
        {
            try
            {
                var produtoRepository = new ProdutosRepository();
                var result = produtoRepository.Carregar(Id);

                return View(result);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        public ActionResult Editar(Produto produto)
        {
            try
            {
                var produtoRepository = new ProdutosRepository();

                if (produtoRepository.Salvar(produto))
                {
                    ViewData["mensagem"] = "<h1>Usuario cadastrado com sucesso!</h1>";
                    return RedirectToAction("index");
                }

                else
                {
                    ViewData["mensagem"] = "<h1>DEU RUIM</h1>";
                    return View();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public ActionResult Detalhes(int Id)
        {
            try
            {
                var produtoRepository = new ProdutosRepository();
                var result = produtoRepository.Carregar(Id);
                return View(result);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public string BuscarProdutosAutocomplete(string value)
        {
            try
            {
                var produtosRepository = new ProdutosRepository();

                var listaDeProdutosDoBD = produtosRepository.BuscarProdutos(value);

                var jsonResult = JsonConvert.SerializeObject(listaDeProdutosDoBD);

                return jsonResult;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public string BuscarProdutos(string value)
        {
            try
            {
                var split = value.Split('-');
                var id = int.Parse(split[0]);

                var produtosRepository = new ProdutosRepository();

                var produto = produtosRepository.BuscarProdutoPorId(id);

                var jsonResult = JsonConvert.SerializeObject(produto);

                return jsonResult;

            }
            catch (Exception ex)
            {

                throw;
            }
        }

    }
}