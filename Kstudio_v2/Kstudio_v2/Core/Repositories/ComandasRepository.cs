using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Presentation;
using Kstudio_v2.Models;
using Nest;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Kstudio_v2.Core.Repositories
{
    public class ComandasRepository : BaseRepository
    {
        private const string Sql_Insert = "INSERT into Comandas (IdBanda,Data,HorarioDeInicio,HorarioFinal,HorasDeEnsaio,ValorDasHoras,ValorTotalDaComanda,Status) VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')";
        private const string Sql_InsertComandaItens = "INSERT into ComandaItens (ProdutoNome,ProdutoQuantidade,ProdutoValor,IdComanda) VALUES ('{0}','{1}','{2}','{3}')";
        private const string Sql_Update = "UPDATE Agendamentos SET Data='{1}',HorarioInicio='{2}',HorarioFim='{3}' WHERE Id = {0}";
        private const string Sql_Delete = "DELETE from Comandas WHERE Id = {0}";
        private const string Sql_Select = "SELECT * from Comandas";
        private const string Sql_SelectByIdJoin = "SELECT a.*, c.* FROM Comandas a INNER JOIN ComandaItens c ON a.Id = c.IdComanda WHERE a.Id={0}";
        private const string SelectById = "SELECT * from Comandas WHERE Id = {0}";
        private const string Sql_SelectByIdCliente = "SELECT a.*, c.* FROM Agendamentos a INNER JOIN Clientes c ON a.IdCliente = c.Id WHERE a.IdCliente={0}";
        private const string Sql_SelectJoin = "SELECT a.*, c.* FROM Comandas a INNER JOIN ComandaItens c ON a.Id = c.IdComanda";
        private const string Sql_SelectBandaResponsavel = "SELECT * FROM CLIENTES WHERE Banda LIKE '%{0}%' OR Responsavel LIKE '%{0}%'";
        private const string Sql_SelectDisponibilidadeDeHorario = "SELECT * FROM agendamentos WHERE HorarioFim BETWEEN '{0}' AND '{1}' AND Data='{2}'";
        private const string Sql_SelectAgendamentoPorData = "SELECT a.*, c.* FROM Agendamentos a INNER JOIN Clientes c ON a.IdCliente = c.Id WHERE Data = '{0}'";
        private const string Sql_SelectComandaId = "SELECT * from Comandas WHERE IdBanda = '{0}' AND Data = '{1}'";

        public bool Excluir(int id)
        {
            var sql = string.Format(Sql_Delete, id);

            return ExecuteCommand(sql);
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
                    horaFinal, comanda.HorasDeEnsaio, comanda.ValorDeHoras, comanda.ValorTotalDaComanda, comanda.StatusComanda);

                result = ExecuteCommand(sql);

                var comandaId = BuscarIdDaComanda(idBanda, dataConvertida);

                if (result && comanda.Produto.Count > 0)
                {
                    for (int i = 0; i < comanda.Produto.Count; i++)
                    {
                        if (comanda.Produto[i].Id == 0) //Se o Id for 0 o usuario e Novo, entao deve Inserir
                        {
                            sql = string.Format(Sql_InsertComandaItens, comanda.Produto[i].Nome,
                                comanda.Produto[i].Quantidade, comanda.Produto[i].Preco, comandaId);
                            ExecuteCommand(sql);
                        }
                    }
                }

                return result;
            }

            return false;
        }

        public Comanda Carregar(int id)
        {
            var connection = GetConnection();
            connection.Open();
            var command = new SQLiteCommand(connection);

            command.CommandText = string.Format(Sql_SelectByIdJoin, id);
            var checkSql = true;

            var result = new Comanda();

            using (var reader = command.ExecuteReader())
            {
                if (!reader.Read())
                {
                    checkSql = false;
                }

                else
                {
                    while (reader.Read())
                    {
                        result = Parse(reader);
                    }
                }

            }

            if (checkSql == false)
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
            result.Produto.Add(new Produto());

            result.Id = int.Parse(reader["Id"].ToString());
            result.Banda = reader["IdBanda"].ToString();
            result.Data = DateTime.Parse(reader["Data"].ToString());
            result.HoraDeInicio = DateTime.Parse(reader["HorarioDeInicio"].ToString());
            result.HoraFinal = DateTime.Parse(reader["HorarioFinal"].ToString());
            result.HorasDeEnsaio = decimal.Parse(reader["HorasDeEnsaio"].ToString());
            result.ValorDeHoras = decimal.Parse(reader["ValorDasHoras"].ToString());
            result.ValorTotalDaComanda = decimal.Parse(reader["ValorTotalDaComanda"].ToString());
            result.StatusComanda = bool.Parse(reader["Status"].ToString());


            foreach (var item in result.Produto)
            {

            }


            return result;
        }

        private int ParseId(SQLiteDataReader reader)
        {
            var result = int.Parse(reader["Id"].ToString());
            return result;
        }

        public int BuscarIdDaComanda(string idBanda, string data)
        {
            var result = 0;

            var connection = GetConnection();
            connection.Open();
            var command = new SQLiteCommand(connection);

            command.CommandText = string.Format(Sql_SelectComandaId, idBanda, data);

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    result = ParseId(reader);
                }
            }

            return result;
        }

        //public List<Cliente> BuscarIdDaBanda(int id)
        //{

        //    var connection = GetConnection();
        //    connection.Open();
        //    var command = new SQLiteCommand(connection);

        //    command.CommandText = string.Format(Sql_SelectByIdCliente, id);

        //    var result = new List<Cliente>();
        //    result.Add(new Cliente());

        //    using(var reader = command.ExecuteReader())
        //    {
        //        while (reader.Read())
        //        {
        //            var cliente = Parse(reader);
        //            if (cliente.Id == result[0].Id)
        //            {
        //                var agendamento = cliente.Agendamentos[0];
        //                result[0].Agendamentos.Add(agendamento);
        //            }
        //            else
        //            {
        //                if (result[0].Id == null || result[0].Id == 0)
        //                {
        //                   result[0] = cliente;
        //                }

        //            }

        //        }
        //    }

        //    return result;
        //}

        //public List<Cliente> BuscarAgendamentosPorData(string data)
        //{
        //    var connection = GetConnection();
        //    connection.Open();
        //    var command = new SQLiteCommand(connection);

        //    command.CommandText = string.Format(Sql_SelectAgendamentoPorData, data);

        //    var result = new List<Cliente>();
        //    result.Add(new Cliente());

        //    using (var reader = command.ExecuteReader())
        //    {
        //        while (reader.Read())
        //        {
        //            var cliente = Parse(reader);
        //            result.Add(cliente);
        //        }
        //    }

        //    return result;
        //}

        //public bool ValidarHorarioDisponivel(Cliente cliente)
        //{
        //    var connection = GetConnection();
        //    connection.Open();
        //    var command = new SQLiteCommand(connection);
        //    var result = 0;

        //    for (int i = 0; i < cliente.Agendamentos.Count; i++)
        //    {
        //        var data = DateTime.Parse(cliente.Agendamentos[i].Data.ToString());
        //        var dataConvertida = data.ToString("yyyy-MM-dd");
        //        var horaInicio = cliente.Agendamentos[i].HorarioInicio.ToString("HH:mm:ss");
        //        var horaFim = cliente.Agendamentos[i].HorarioFinal.ToString("HH:mm:ss");

        //        command.CommandText = string.Format(Sql_SelectDisponibilidadeDeHorario, horaInicio, horaFim, dataConvertida);


        //        using (var reader = command.ExecuteReader())
        //        {
        //            while (reader.Read())
        //            {
        //                result++;
        //                break;
        //            }
        //        }
        //    }

        //    if (result > 0)
        //    {
        //        return false;
        //    }

        //    return true;
        //}
    }


}