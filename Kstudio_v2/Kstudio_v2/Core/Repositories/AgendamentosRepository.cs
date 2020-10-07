using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Presentation;
using Kstudio_v2.Models;
using Nest;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Kstudio_v2.Core.Repositories
{
    public class AgendamentosRepository : BaseRepository
    {
        private const string Sql_Insert = "INSERT into Agendamentos (IdCliente,Data,HorarioInicio,HorarioFim) VALUES ('{0}','{1}','{2}','{3}')";
        private const string Sql_Update = "UPDATE Agendamentos SET IdCliente='{1}',Data='{2}',HorarioInicio='{3}',HorarioFim='{4}' WHERE Id = {0}";
        private const string Sql_Delete = "DELETE from Agendamentos WHERE Id = {0}";
        private const string Sql_Select = "SELECT * from Agendamentos";
        private const string Sql_SelectOne = "SELECT a.*, c.* FROM Agendamentos a INNER JOIN Clientes c ON a.IdCliente = c.Id WHERE a.Id={0}";//"SELECT * from Agendamentos WHERE Id={0}";
        private const string Sql_SelectJoin = "SELECT a.*, c.* FROM Agendamentos a INNER JOIN Clientes c ON a.IdCliente = c.Id";
        private const string Sql_SelectBandaResponsavel = "SELECT * FROM CLIENTES WHERE Banda LIKE '%{0}%' OR Responsavel LIKE '%{0}%'";

        public bool Excluir(int id)
        {
            var sql = string.Format(Sql_Delete, id);

            return ExecuteCommand(sql);
        }

        public bool Salvar(Agendamento agendamento)
        {
            var sql = "";
            var data = DateTime.Parse(agendamento.Data.ToString());
            var dataConvertida = data.ToString("yyyy-MM-dd");

            if (agendamento.Id == 0) //Se o Id for 0 o usuario e Novo, entao deve Inserir
            {
                sql = string.Format(Sql_Insert, agendamento.Cliente.Id, dataConvertida, agendamento.HorarioInicio, agendamento.HorarioFinal);
            }

            else //Usuario com Id entao os dados devem ser alterados
            {
                sql = string.Format(Sql_Update, agendamento.Id, agendamento.Cliente.Id, dataConvertida, agendamento.HorarioInicio, agendamento.HorarioFinal);
            }

            return ExecuteCommand(sql);
        }

        public Agendamento Carregar(int id)
        {
            var connection = GetConnection();
            connection.Open();
            var command = new SQLiteCommand(connection);

            command.CommandText = string.Format(Sql_SelectOne, id);

            var result = new Agendamento();

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                   result = Parse(reader);
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

            command.CommandText = Sql_SelectJoin;

            var result = new List<Agendamento>();

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                   var agendamento = Parse(reader);
                   result.Add(agendamento);
                }
            }

            command.Dispose();
            connection.Close();
            connection.Dispose();

            return result;
        }

        public Agendamento Parse(SQLiteDataReader reader)
        {
            var result = new Agendamento();
            result.Id = int.Parse(reader["Id"].ToString());
            result.Data = DateTime.Parse(reader["Data"].ToString());
            result.HorarioInicio = reader["HorarioInicio"].ToString();
            result.HorarioFinal = reader["HorarioFim"].ToString();

            result.Cliente = new Cliente();
            result.Cliente.Id = int.Parse(reader["IdCliente"].ToString());
            result.Cliente.Responsavel = reader["Responsavel"].ToString();
            result.Cliente.Telefone = reader["Telefone"].ToString();
            result.Cliente.Email = reader["Email"].ToString();
            result.Cliente.Banda = reader["Banda"].ToString();
            result.Cliente.EstiloMusical = reader["EstiloMusical"].ToString();

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