using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Reflection;
using System.Web;
using Kstudio_v2.Models;
using Microsoft.Ajax.Utilities;

namespace Kstudio_v2.Core.Repositories
{
    public class ClientesRepository : BaseRepository
    {
        private const string Sql_Insert = "INSERT into Clientes (Banda,Responsavel,EstiloMusical,Email,Telefone) VALUES ('{0}','{1}','{2}','{3}','{4}')";
        private const string Sql_Update = "UPDATE Clientes SET Banda='{1}',Responsavel='{2}',EstiloMusical='{3}',Email='{4}',Telefone={5} WHERE Id = {0}";
        private const string Sql_Delete = "DELETE from Clientes WHERE Id = {0}";
        private const string Sql_Select = "SELECT * from Clientes";
        private const string Sql_SelectOne = "SELECT * from Clientes WHERE Id={0}";
       // Cliente result = null;

        public bool Excluir(int id)
        {
            var sql = string.Format(Sql_Delete, id);
            return ExecuteCommand(sql);
        }

        public bool Salvar(Cliente cliente)
        {
            var validateField = IsAnyFieldOnCadastroNullOrEmpty(cliente);

            if (validateField == true)
            {
                return false;
            }

            else
            {
                var sql = "";
                if (cliente.Id == 0) //Se o Id for 0 o usuario e Novo, entao deve Inserir
                    sql = string.Format(Sql_Insert, cliente.Banda, cliente.Responsavel, cliente.EstiloMusical, cliente.Email, cliente.Telefone);
                else //Usuario com Id entao os dados devem ser alterados
                    sql = string.Format(Sql_Update, cliente.Id, cliente.Banda, cliente.Responsavel, cliente.EstiloMusical, cliente.Email, cliente.Telefone);

                var result = ExecuteCommand(sql);
                return result;
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

        public List<Cliente> Listar()
        {
            var connection = GetConnection();
            connection.Open();
            var command = new SQLiteCommand(connection);


            command.CommandText = Sql_Select;
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

        public List<Cliente> ListarClientesDoCampoPesquisa(PesquisaCliente resultado)
        {
            Cliente cliente = null;
            var listaDeClientes = Listar();
            var result = new List<Cliente>();

            for (int i = 0; i < listaDeClientes.Count; i++)
            {
                if (listaDeClientes[i].Banda.Contains(resultado.ProcuraPor.ToLower()) || listaDeClientes[i].Responsavel.Contains(resultado.ProcuraPor.ToLower())
                || listaDeClientes[i].Email.Contains(resultado.ProcuraPor.ToLower()) || listaDeClientes[i].Telefone.Contains(resultado.ProcuraPor.ToLower()))
                {
                    cliente = new Cliente()
                    {
                        Id = listaDeClientes[i].Id,
                        Banda = listaDeClientes[i].Banda.ToString(),
                        Responsavel = listaDeClientes[i].Responsavel.ToString(),
                        Email = listaDeClientes[i].Email.ToString(),
                        EstiloMusical = listaDeClientes[i].EstiloMusical.ToString(),
                        Telefone = listaDeClientes[i].Telefone.ToString(),
                    };

                    result.Add(cliente);
                }
            }

            return result;
        }

        public Cliente MostraClienteDuplicado(Cliente cliente)
        {
            Cliente result = null;
            var listaDeClientes = Listar();

            for (int i = 0; i < listaDeClientes.Count; i++)
            {
                if (listaDeClientes[i].Banda.Equals(cliente.Banda.ToLower()) && 
                    listaDeClientes[i].Email.Equals(cliente.Email.ToLower()) && listaDeClientes[i].Telefone.Equals(cliente.Telefone.ToLower()))

                {
                    result = new Cliente()
                    {
                        Id = listaDeClientes[i].Id,
                        Banda = listaDeClientes[i].Banda.ToString(),
                        Responsavel = listaDeClientes[i].Responsavel.ToString(),
                        Email = listaDeClientes[i].Email.ToString(),
                        EstiloMusical = listaDeClientes[i].EstiloMusical.ToString(),
                        Telefone = listaDeClientes[i].Telefone.ToString(),
                    };
                    break;
                }
            }

            return result;
        }

        //public bool IsAnyClienteNullOrEmpty(Cliente cliente)
        //{
           
        //    if (cliente.Banda == null && cliente.Responsavel == null &&
        //        cliente.Email == null && cliente.Telefone == null)
        //    {
        //        return true;
        //    }

        //    else if (cliente.Banda == string.Empty && cliente.Responsavel == string.Empty &&
        //        cliente.Email == string.Empty && cliente.Telefone == string.Empty)
        //    {
        //        return true;
        //    }

        //    return false;
        //}

        public bool IsClienteDuplicated(Cliente cliente)
        {
            Cliente result = null;
            var listaDeClientes = Listar();

            for (int i = 0; i < listaDeClientes.Count; i++)
            {
                if (listaDeClientes[i].Banda.Equals(cliente.Banda.ToLower()) &&
                    listaDeClientes[i].Email.Equals(cliente.Email.ToLower()) && listaDeClientes[i].Telefone.Equals(cliente.Telefone.ToLower()))
                {
                    result = new Cliente()
                    {
                        Id = listaDeClientes[i].Id,
                        Banda = listaDeClientes[i].Banda.ToString(),
                        Responsavel = listaDeClientes[i].Responsavel.ToString(),
                        Email = listaDeClientes[i].Email.ToString(),
                        EstiloMusical = listaDeClientes[i].EstiloMusical.ToString(),
                        Telefone = listaDeClientes[i].Telefone.ToString(),
                    };

                    return true;
                }
            }

            return false;
        }

        public bool IsAnyFieldOnCadastroNullOrEmpty(Cliente cliente)
        {
            if (cliente.Banda == null || cliente.Responsavel == null ||
                cliente.Email == null || cliente.Telefone == null)
            {
                return true;
            }

            else if (cliente.Banda == string.Empty || cliente.Responsavel == string.Empty ||
               cliente.Email == string.Empty || cliente.Telefone == string.Empty)
            {
                return true;
            }

            return false;
        }

        private Cliente Parse(SQLiteDataReader reader)
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
    }


}