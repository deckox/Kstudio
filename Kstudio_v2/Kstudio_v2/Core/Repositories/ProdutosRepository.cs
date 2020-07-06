﻿using Kstudio_v2.Models;
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
        private const string Sql_Insert = "INSERT into Produtos (Descricao,Quantidade,Estoque,Preco,ValorTotal,Data) VALUES ('{0}','{1}','{2}','{3}','{4}','{5}',{6})";
        private const string Sql_Update = "UPDATE Produtos SET Descricao='{1}',Quantidade='{2}',Estoque='{3}',Preco='{4}',ValorTotal='{5}',Data={6} WHERE Id = {0}";
        private const string Sql_Delete = "DELETE from Produtos WHERE Id = {0}";
        private const string Sql_Select = "SELECT * from Produtos";
        private const string Sql_SelectOne = "SELECT * from Produtos WHERE Id={0}";

        public bool Excluir(int id)
        {
            var sql = string.Format(Sql_Delete, id);
            return ExecuteCommand(sql);
        }

        public bool Salvar(Produto produto)
        {
            var sql = "";
            if (produto.Id == 0) //Se o Id for 0 o produto e Novo, entao deve Inserir
                sql = string.Format(Sql_Insert, produto.Descricao, produto.Quantidade, produto.Estoque, produto.Preco, produto.ValorTotal, produto.Data);
            else //produto com Id entao os dados devem ser alterados
                sql = string.Format(Sql_Update, produto.Descricao, produto.Quantidade, produto.Estoque, produto.Preco, produto.ValorTotal, produto.Data);

            var result = ExecuteCommand(sql);
            return result;
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
            var produto = new Produto()
            {
                Id = int.Parse(reader["Id"].ToString()),
                Descricao = reader["Descricao"].ToString(),
                Quantidade = int.Parse(reader["Quantidade"].ToString()),
                Estoque = int.Parse(reader["Estoque"].ToString()),
                Preco = int.Parse(reader["Preco"].ToString()),
                ValorTotal = int.Parse(reader["ValorTotal"].ToString()),
                Data = DateTime.Parse(reader["Data"].ToString())
            };
            return produto;
        }

    }
}