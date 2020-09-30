using DocumentFormat.OpenXml.Presentation;
using Kstudio_v2.Models;
using Nest;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Web;

namespace Kstudio_v2.Core.Repositories
{
    public class AgendamentosRepository : BaseRepository
    {
        private const string Sql_Insert = "INSERT into Agendamentos (Data,HorarioInicio,HorarioFim) VALUES ('{0}','{1}','{2}')";
        private const string Sql_Update = "UPDATE Agendamentos SET Nome='{1}',Login='{2}',Senha='{3}' WHERE Id = {0}";
        private const string Sql_Delete = "DELETE from Agendamentos WHERE Id = {0}";
        private const string Sql_Select = "SELECT * from Agendamentos";
        private const string Sql_SelectOne = "SELECT * from Agendamentos WHERE Id={0}";
        private const string Sql_SelectLogin = "SELECT * from Agendamentos WHERE Login='{0}'";
        private const string Sql_SelectLoginSenha = "SELECT * from Agendamentos WHERE Login='{0}' AND Senha='{1}'";
        private const string Sql_SelectBandaResponsavel = "SELECT * FROM CLIENTES WHERE Banda LIKE '%{0}%' OR Responsavel LIKE '%{0}%'";

        public bool Excluir(int id)
        {
            var sql = string.Format(Sql_Delete, id);

            return ExecuteCommand(sql);
        }

        public bool Salvar(Agendamento agendamento)
        {
            var sql = "";

            if (agendamento.Id == 0) //Se o Id for 0 o usuario e Novo, entao deve Inserir
            {
                sql = string.Format(Sql_Insert, agendamento.IdCliente, agendamento.Data, agendamento.HorarioInicio, agendamento.HorarioFinal);
            }

            else //Usuario com Id entao os dados devem ser alterados
            {
                sql = string.Format(Sql_Update, agendamento.Id, agendamento.IdCliente, agendamento.Data, agendamento.HorarioInicio, agendamento.HorarioFinal);
            }

            return ExecuteCommand(sql);
        }

        public Agendamento Carregar(int id)
        {
            var connection = GetConnection();
            connection.Open();
            var command = new SQLiteCommand();

            command.CommandText = string.Format(Sql_SelectOne, id);

            var result = new Agendamento();

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                   // result = Parse(reader);
                }
            }

            command.Dispose();
            connection.Close();
            connection.Dispose();

            return result;
        }

        public List<Agendamento> Listar()
        {
            var connection = GetConnection();
            connection.Open();
            var command = new SQLiteCommand(connection);

            command.CommandText = Sql_Select;

            var result = new List<Agendamento>();

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                   // result.Add(Parse(reader));
                }
            }

            command.Dispose();
            connection.Close();
            connection.Dispose();

            return result;
        }

        public Agendamento Parse(SqlDataReader reader)
        {
            var result = new Agendamento()
            {
                Id = int.Parse(reader["Id"].ToString()),
                Data = DateTime.Parse(reader["Data"].ToString().ToLower()),
                //IdCliente = reader["IdCliente"].ToString(),
                HorarioInicio = reader["HorarioInicio"].ToString().ToLower(),
                HorarioFinal = reader["HorarioFinal"].ToString().ToLower(),
            };
 
             return result;
        }

        private Cliente ParseCliente(SQLiteDataReader reader)
        {
            var cliente = new Cliente()
            {
                Id = int.Parse(reader["Id"].ToString()),
                Banda = reader["Banda"].ToString().ToLower(),
                Responsavel = reader["Responsavel"].ToString().ToLower(),
                Email = reader["Email"].ToString().ToLower(),
                EstiloMusical = reader["EstiloMusical"].ToString().ToLower(),
                Telefone = reader["Telefone"].ToString().ToLower(),
            };
            return cliente;
        }

        public List<Cliente> BuscarIdDaBanda(string pesquisabanda)
        {

            var connection = GetConnection();
            connection.Open();
            var command = new SQLiteCommand(connection);

            command.CommandText = string.Format(Sql_SelectBandaResponsavel, pesquisabanda);

            var result = new List<Cliente>();

            using(var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var cliente = ParseCliente(reader);
                    result.Add(cliente);
                }
            }

            return result;
        }
    }

   
}