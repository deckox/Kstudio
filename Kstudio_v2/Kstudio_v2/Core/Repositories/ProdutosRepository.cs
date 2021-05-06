using Kstudio_v2.Models;
using System;
using System.Collections.Generic;
using Kstudio_v2.Core.Repositories;
using System.Linq;
using System.Web;
using System.Data.SQLite;

namespace Kstudio_v2.Core.Repositories
{
    public class ProdutosRepository : BaseRepository
    {
        private const string Sql_Insert = "INSERT into Produtos (Nome,PrecoDeCusto,PrecoDeVenda,Preco) VALUES ('{0}','{1}','{2}','{3}')";
        private const string Sql_Update = "UPDATE Produtos SET Nome='{1}',PrecoDeCusto='{2}',PrecoDeVenda='{3}', Preco='{4}' WHERE Id = {0}";
        private const string Sql_Delete = "DELETE from Produtos WHERE Id = {0}";
        private const string Sql_Select = "SELECT * from Produtos";
        private const string Sql_SelectProduto = "SELECT * from Produtos WHERE Nome = '{0}'";
        private const string Sql_SelectOne = "SELECT * from Produtos WHERE Id={0}";
        private const string Sql_SelectPesquisaQualquerProduto = "SELECT * from Produtos WHERE Nome LIKE '%{0}%'";



        public bool Excluir(int id)
        {
            var sql = string.Format(Sql_Delete, id);
            return ExecuteCommand(sql);
        }

        public bool Salvar(Produto produto)
        {
            var result = false;

            if (ValidarProdutoJaCadastrado(produto))
            {
                return result;
            }

            else
            {
                var sql = "";

                if (produto.Id == 0) //Se o Id for 0 o produto e Novo, entao deve Inserir
                    sql = string.Format(Sql_Insert, produto.Nome.ToLower(), produto.PrecoDeCusto, produto.PrecoDeVenda, produto.Preco = 0);
                else //produto com Id entao os dados devem ser alterados
                    sql = string.Format(Sql_Update, produto.Id, produto.Nome.ToLower(), produto.PrecoDeCusto, produto.PrecoDeVenda, produto.Preco = 0);

                result = ExecuteCommand(sql);
                return result;

            }

           
        }

        public Produto Carregar(int id)
        {
            var connection = GetConnection();
            connection.Open();
            var command = new SQLiteCommand(connection);

            command.CommandText = string.Format(Sql_SelectOne, id);
            var result = new Produto();

            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    result = Parse(reader);
                }
            }

            command.Dispose();
            connection.Close();
            connection.Dispose();

            return result;
        }

        public List<Produto> Listar()
        {
            var connection = GetConnection();
            connection.Open();
            var command = new SQLiteCommand(connection);


            command.CommandText = Sql_Select;
            var result = new List<Produto>();

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var produto = Parse(reader);
                    result.Add(produto);
                }
            }

            command.Dispose();
            connection.Close();
            connection.Dispose();

            return result;
        }

        private Produto Parse(SQLiteDataReader reader)
        {
            var produto = new Produto
            {
                  Id = int.Parse(reader["Id"].ToString()),
                  Nome = reader["Nome"].ToString(),
                  PrecoDeCusto = decimal.Parse(reader["PrecoDeCusto"].ToString()),
                  PrecoDeVenda = decimal.Parse(reader["PrecoDeVenda"].ToString()),
                  Preco = decimal.Parse(reader["Preco"].ToString()),
            };
            return produto;
        }
        public bool ValidarProdutoJaCadastrado(Produto produto)
        {
            var connection = GetConnection();
            connection.Open();

            var command = new SQLiteCommand(connection);
            command.CommandText = string.Format(Sql_SelectProduto,produto.Nome);

            var result = new Produto();

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    result = Parse(reader);
                    break;
                }
            }

            if(result.Nome == produto.Nome)
            {
                return true;
            }

            else
            {
                return false;
            }

        }

        public List<Produto> BuscarProdutos(string pesquisa)
        {
            var connection = GetConnection();
            connection.Open();

            var command = new SQLiteCommand(connection);

            command.CommandText = string.Format(Sql_SelectPesquisaQualquerProduto, pesquisa);

            var result = new List<Produto>();

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var auxResult = Parse(reader);
                    result.Add(auxResult);
                }
            }

            return result; 
        }

        public Produto BuscarProdutoPorId(int id)
        {

            var connection = GetConnection();
            connection.Open();
            var command = new SQLiteCommand(connection);

            command.CommandText = string.Format(Sql_SelectOne, id);

            var result = new Produto();

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    result = Parse(reader);
                }
            }

            return result;
        }

    }
}