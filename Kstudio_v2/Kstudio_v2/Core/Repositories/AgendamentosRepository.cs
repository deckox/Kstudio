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
        private const string Sql_Update = "UPDATE Agendamentos SET Data='{1}',HorarioInicio='{2}',HorarioFim='{3}' WHERE Id = {0}";
        private const string Sql_Delete = "DELETE from Agendamentos WHERE Id = {0}";
        private const string Sql_Select = "SELECT * from Agendamentos";
        private const string Sql_SelectOne = "SELECT a.*, c.* FROM Agendamentos a INNER JOIN Clientes c ON a.IdCliente = c.Id WHERE a.Id={0}";
        private const string Sql_SelectByIdCliente = "SELECT a.*, c.* FROM Agendamentos a INNER JOIN Clientes c ON a.IdCliente = c.Id WHERE a.IdCliente={0}";
        private const string Sql_SelectJoin = "SELECT a.*, c.* FROM Agendamentos a INNER JOIN Clientes c ON a.IdCliente = c.Id";
        private const string Sql_SelectBandaResponsavel = "SELECT * FROM CLIENTES WHERE Banda LIKE '%{0}%' OR Responsavel LIKE '%{0}%'";


        public bool Excluir(int id)
        {
            var sql = string.Format(Sql_Delete, id);

            return ExecuteCommand(sql);
        }

        public bool Salvar(Cliente cliente)
        {
            var sql = "";
            var listSql = new List<string>();
            var ok = 0;

          
            //   var dataConvertida = "2010-11-22";

            for (int i = 0; i < cliente.Agendamentos.Count; i++)
            {
                var data = DateTime.Parse(cliente.Agendamentos[i].Data.ToString());
                var dataConvertida = data.ToString("yyyy-MM-dd");


                if (cliente.Agendamentos[i].Id == 0) //Se o Id for 0 o usuario e Novo, entao deve Inserir
                {
                    sql = string.Format(Sql_Insert, cliente.Id, dataConvertida, cliente.Agendamentos[i].HorarioInicio, cliente.Agendamentos[i].HorarioFinal);
                }

                else //Usuario com Id entao os dados devem ser alterados
                {
                    sql = string.Format(Sql_Update, cliente.Agendamentos[i].Id, dataConvertida, cliente.Agendamentos[i].HorarioInicio, cliente.Agendamentos[i].HorarioFinal);
                }

                listSql.Add(sql);
                

                if (ExecuteCommand(listSql[i]) == true)
                {
                    ok++;
                }
            }

            if (cliente.Agendamentos.Count == ok)
            {
                return true;
            }

            else
            {
                return false;
            }

          
        }

        public Cliente Carregar(int id)
        {
            var connection = GetConnection();
            connection.Open();
            var command = new SQLiteCommand(connection);

            command.CommandText = string.Format(Sql_SelectOne, id);

            var result = new Cliente();

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

        public Cliente CarregarLista(int id)
        {
            var connection = GetConnection();
            connection.Open();
            var command = new SQLiteCommand(connection);

            command.CommandText = string.Format(Sql_SelectByIdCliente, id);

            var result = new Cliente();

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



        public List<Cliente> Listar()
        {
            var connection = GetConnection();
            connection.Open();
            var command = new SQLiteCommand(connection);

            command.CommandText = Sql_SelectJoin;

            var result = new List<Cliente>();

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                   var cliente = Parse(reader);
                   result.Add(cliente);
                }
            }

            command.Dispose();
            connection.Close();
            connection.Dispose();

            return result;
        }

        private Cliente Parse(SQLiteDataReader reader)
        {
            var result = new Cliente();
            result.Agendamentos.Add(new Agendamento());

            result.Agendamentos[0].HorarioInicio = reader["HorarioInicio"].ToString();
            result.Agendamentos[0].HorarioFinal = reader["HorarioFim"].ToString();
            result.Agendamentos[0].Data = DateTime.Parse(reader["Data"].ToString());
            result.Agendamentos[0].Id = int.Parse((reader["Id"].ToString()));

            result.Id = int.Parse(reader["IdCliente"].ToString());
            result.Responsavel = reader["Responsavel"].ToString();
            result.Telefone = reader["Telefone"].ToString();
            result.Email = reader["Email"].ToString();
            result.Banda = reader["Banda"].ToString();
            result.EstiloMusical = reader["EstiloMusical"].ToString();

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

        public List<Cliente> BuscarIdDaBanda(int id)
        {

            var connection = GetConnection();
            connection.Open();
            var command = new SQLiteCommand(connection);

            command.CommandText = string.Format(Sql_SelectByIdCliente, id);

            var result = new List<Cliente>();
            result.Add(new Cliente());

            using(var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var cliente = Parse(reader);
                    if (cliente.Id == result[0].Id)
                    {
                        var agendamento = cliente.Agendamentos[0];
                        result[0].Agendamentos.Add(agendamento);
                    }
                    else
                    {
                        if (result[0].Id == null || result[0].Id == 0)
                        {
                           result[0] = cliente;
                        }
                       
                    }
                  
                }
            }

            return result;
        }

        public Cliente ValidarHorario(Cliente cliente)
        {
            var connection = GetConnection();
            connection.Open();
            var command = new SQLiteCommand(connection);



            return cliente;
        }
    }

   
}