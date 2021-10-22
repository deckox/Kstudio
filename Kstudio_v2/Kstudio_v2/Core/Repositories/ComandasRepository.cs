using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Presentation;
using Kstudio_v2.Models;
using LinqToDB.Data;
using Nest;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;

namespace Kstudio_v2.Core.Repositories
{
    public class ComandasRepository : BaseRepository
    {
        private const string Sql_Insert = "INSERT into Comandas (IdBanda,Data,HorarioDeInicio,HorarioFinal,HorasDeEnsaio,ValorDasHoras,ValorTotalDaComanda,Status) VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')";
        private const string Sql_InsertComandaItens = "INSERT into ComandaItens (ProdutoNome,ProdutoQuantidade,ProdutoValor,IdComanda) VALUES ('{0}','{1}','{2}','{3}')";
        private const string Sql_Update = "UPDATE Comandas SET Data='{1}',HorarioDeInicio='{2}',HorarioFinal='{3}',HorasDeEnsaio='{4}',ValorDasHoras='{5}',ValorTotalDaComanda='{6}',Status='{7}' WHERE Id = {0}";
        private const string Sql_UpdateComandaItens = "UPDATE ComandaItens SET ProdutoNome='{1}',ProdutoQuantidade='{2}',ProdutoValor='{3}' WHERE Id = {0}";
        private const string Sql_Delete = "DELETE from Comandas WHERE Id = {0}";
        private const string Sql_DeleteComandaItens = "DELETE from ComandaItens WHERE Id = {0}";
        private const string Sql_Select = "SELECT * from Comandas";
        private const string Sql_SelectByDate = "SELECT * FROM Comandas WHERE idBanda = '{0}' AND Data = '{1}'";
        private const string Sql_SelectByIdJoin = "SELECT c.id as IdItem, a.*, c.* FROM Comandas a INNER JOIN ComandaItens c ON a.Id = c.IdComanda WHERE a.Id={0}";
        private const string SelectById = "SELECT * from Comandas WHERE Id = {0}";
        private const string Sql_SelectByIdCliente = "SELECT a.*, c.* FROM Agendamentos a INNER JOIN Clientes c ON a.IdCliente = c.Id WHERE a.IdCliente={0}";
        private const string Sql_SelectJoin = "SELECT a.*, c.* FROM Comandas a INNER JOIN ComandaItens c ON a.Id = c.IdComanda";
        private const string Sql_SelectBandaResponsavel = "SELECT * FROM CLIENTES WHERE Banda LIKE '%{0}%' OR Responsavel LIKE '%{0}%'";
        private const string Sql_SelectDisponibilidadeDeHorario = "SELECT * FROM agendamentos WHERE HorarioFim BETWEEN '{0}' AND '{1}' AND Data='{2}'";
        private const string Sql_SelectAgendamentoPorData = "SELECT a.*, c.* FROM Agendamentos a INNER JOIN Clientes c ON a.IdCliente = c.Id WHERE Data = '{0}'";
        private const string Sql_SelectComandaId = "SELECT * from Comandas WHERE IdBanda = '{0}' AND Data = '{1}'";

        public bool Excluir(Comanda comanda)
        {
             
            var sql = string.Format(Sql_Delete, comanda.Id);
            var resultComanda = ExecuteCommand(sql);
            bool resultComandaItens;
            var resultComandaItensCount = 0;
            var result = false;

            if (resultComanda && comanda.Produto.Count > 0)
            {
                for (int i = 0; i < comanda.Produto.Count; i++)
                {
                    var sqlProduto = string.Format(Sql_DeleteComandaItens, comanda.Produto[i].Id);
                    resultComandaItens = ExecuteCommand(sqlProduto);

                    if (resultComandaItens)
                    {
                        resultComandaItensCount++;
                    }
                }

                if (resultComanda && comanda.Produto.Count == resultComandaItensCount)
                {
                    result = true;
                }
            }

            return result;

        }

        public bool Salvar(Comanda comanda)
        {
            var listSql = new List<string>();
            var agendamentosCadastrados = 0;
            string sql;
            bool result;

            if (comanda.Id == 0) //Se o Id for 0 o usuario e Novo, entao deve Inserir
            {
                var split = comanda.Banda.Split('-');
                var idBanda = split.FirstOrDefault();
                var data = DateTime.Parse(comanda.Data.ToString());
                var dataConvertida = data.ToString("yyyy-MM-dd");
                var horaDeInicio = comanda.HoraDeInicio.ToString("HH:mm:ss");
                var horaFinal = comanda.HoraFinal.ToString("HH:mm:ss");

                sql = string.Format(Sql_Insert, idBanda, dataConvertida, horaDeInicio,
                    horaFinal, comanda.HorasDeEnsaio, comanda.ValorDeHoras, comanda.ValorTotalDaComanda,
                    comanda.StatusComanda);

                result = ExecuteCommand(sql);

               

                if (result && comanda.Produto.Count > 0)
                {
                    var comandaId = BuscarIdDaComanda(idBanda, dataConvertida);

                    for (int i = 0; i < comanda.Produto.Count; i++)
                    {
                        if (comanda.Produto[i].Id == 0) //Se o Id for 0 o usuario e Novo, entao deve Inserir
                        {
                            sql = string.Format(Sql_InsertComandaItens, comanda.Produto[i].Nome,
                                comanda.Produto[i].Quantidade, comanda.Produto[i].Preco, comandaId);
                            ExecuteCommand(sql);
                        }

                        else
                        {
                            sql = string.Format(Sql_UpdateComandaItens, comanda.Produto[i].Nome,
                                comanda.Produto[i].Quantidade, comanda.Produto[i].Preco, comandaId);
                            ExecuteCommand(sql);
                        }
                    }
                }

                return result;
            }

            else
            {
                var split = comanda.Banda.Split('-');
                var idBanda = split.FirstOrDefault();
                var data = DateTime.Parse(comanda.Data.ToString());
                var dataConvertida = data.ToString("yyyy-MM-dd");
                var horaDeInicio = comanda.HoraDeInicio.ToString("HH:mm:ss");
                var horaFinal = comanda.HoraFinal.ToString("HH:mm:ss");
                var comandaId = BuscarIdDaComanda(idBanda, dataConvertida);

                sql = string.Format(Sql_Update, comandaId, dataConvertida, horaDeInicio,
                    horaFinal, comanda.HorasDeEnsaio, comanda.ValorDeHoras, comanda.ValorTotalDaComanda,
                    comanda.StatusComanda);

                result = ExecuteCommand(sql);

                if (result && comanda.Produto.Count > 0)
                {
                    for (int i = 0; i < comanda.Produto.Count; i++)
                    {
                        sql = string.Format(Sql_UpdateComandaItens, comanda.Produto[i].Id, comanda.Produto[i].Nome,
                            comanda.Produto[i].Quantidade, comanda.Produto[i].Preco);
                        ExecuteCommand(sql);
                    }
                }

                return result;
            }

        }

        public Comanda Carregar(int id)
        {
            var connection = GetConnection();
            connection.Open();
            var command = new SQLiteCommand(connection);

            command.CommandText = string.Format(Sql_SelectByIdJoin, id);

            var result = new Comanda();

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    result.Id = int.Parse(reader["Id"].ToString());
                    result.Banda = reader["IdBanda"].ToString();
                    result.Data = DateTime.Parse(reader["Data"].ToString());
                    result.HoraDeInicio = DateTime.Parse(reader["HorarioDeInicio"].ToString());
                    result.HoraFinal = DateTime.Parse(reader["HorarioFinal"].ToString());
                    result.HorasDeEnsaio = decimal.Parse(reader["HorasDeEnsaio"].ToString());
                    result.ValorDeHoras = decimal.Parse(reader["ValorDasHoras"].ToString());
                    result.ValorTotalDaComanda = decimal.Parse(reader["ValorTotalDaComanda"].ToString());
                    result.StatusComanda = bool.Parse(reader["Status"].ToString());

                    var produto = new Produto
                    {
                        Id = int.Parse(reader["IdItem"].ToString()),
                        Nome = reader["ProdutoNome"].ToString(),
                        Quantidade = int.Parse(reader["ProdutoQuantidade"].ToString()),
                        Preco = decimal.Parse(reader["ProdutoValor"].ToString()),
                    };

                    result.Produto.Add(produto);
                }

            }

            if (result.Produto.Count < 1)
            {
                command.CommandText = string.Format(SelectById, id);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result = Parse(reader);
                    }

                }
            }

            command.Dispose();
            connection.Close();
            connection.Dispose();

            return result;
        }

        //public Cliente CarregarLista(int id)
        //{
        //    var connection = GetConnection();
        //    connection.Open();
        //    var command = new SQLiteCommand(connection);

        //    command.CommandText = string.Format(Sql_SelectByIdCliente, id);

        //    var result = new Cliente();

        //    using (var reader = command.ExecuteReader())
        //    {
        //        while (reader.Read())
        //        {
        //            result = Parse(reader);
        //        }
        //    }

        //    command.Dispose();
        //    connection.Close();
        //    connection.Dispose();

        //    return result;
        //}



        public List<Comanda> Listar()
        {
            var connection = GetConnection();
            connection.Open();
            var command = new SQLiteCommand(connection);

            command.CommandText = Sql_Select;

            var result = new List<Comanda>();

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var comanda = Parse(reader);
                    result.Add(comanda);
                }
            }

            command.Dispose();
            connection.Close();
            connection.Dispose();

            return result;
        }

        private Comanda Parse(SQLiteDataReader reader)
        {
            var result = new Comanda();

            result.Id = int.Parse(reader["Id"].ToString());
            result.Banda = reader["IdBanda"].ToString();
            result.Data = DateTime.Parse(reader["Data"].ToString());
            result.HoraDeInicio = DateTime.Parse(reader["HorarioDeInicio"].ToString());
            result.HoraFinal = DateTime.Parse(reader["HorarioFinal"].ToString());
            result.HorasDeEnsaio = decimal.Parse(reader["HorasDeEnsaio"].ToString());
            result.ValorDeHoras = decimal.Parse(reader["ValorDasHoras"].ToString());
            result.ValorTotalDaComanda = decimal.Parse(reader["ValorTotalDaComanda"].ToString());
            result.StatusComanda = bool.Parse(reader["Status"].ToString());

            return result;
        }

        public int BuscarIdDaComanda(string idBanda, string data)
        {
            Comanda comanda;
            var result = 0;

            var connection = GetConnection();
            connection.Open();
            var command = new SQLiteCommand(connection);

            command.CommandText = string.Format(Sql_SelectComandaId, idBanda, data);

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read()) //rever um possivel agendamento da mesma banda no mesmo dia porem em horarios diferentes
                {
                    comanda = Parse(reader);
                    result = comanda.Id;
                }

            }

            return result;
        }



        public bool ValidarComandaExistente(Comanda comanda)
        {
            var connection = GetConnection();
            connection.Open();
            var command = new SQLiteCommand(connection);

            var result = false;
            var data = comanda.Data.ToString("yyyy-MM-dd");
            var bandaId = comanda.Banda.Split('-');

            command.CommandText = string.Format(Sql_SelectByDate, bandaId.FirstOrDefault(), data);

            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    result = true;
                }
            }

            return result;
        }

    }
}