using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI.WebControls;
using Kstudio_v2.Models;
using Microsoft.Ajax.Utilities;

namespace Kstudio_v2.Core.Repositories
{
    public class UsuariosRepository : BaseRepository
    {
        private const string Sql_Insert = "INSERT into Usuarios (Nome,Login,Senha) VALUES ('{0}','{1}','{2}')";
        private const string Sql_Update = "UPDATE Usuarios SET Nome='{1}',Login='{2}',Senha='{3}' WHERE Id = {0}";
        private const string Sql_Delete = "DELETE from Usuarios WHERE Id = {0}";
        private const string Sql_Select = "SELECT * from Usuarios";
        private const string Sql_SelectOne = "SELECT * from Usuarios WHERE Id={0}";
        private const string Sql_SelectLogin = "SELECT * from Usuarios WHERE Login='{0}'";
        private const string Sql_SelectLoginSenha = "SELECT * from Usuarios WHERE Login='{0}' AND Senha='{1}'";
        private const string Sql_SelectNomeLogin = "SELECT * from Usuarios WHERE Nome LIKE '%{0}%' OR Login LIKE '%{0}%'";



        public bool Excluir(int id)
        {
            var sql = string.Format(Sql_Delete, id);
            return ExecuteCommand(sql);
        }

        public bool Salvar(Usuario usuario)
        {
            var sql = "";
            var result = false;

            if (ValidarUsuarioJaCadastrado(usuario) == true)
            {
                return result;
            }

            else
            {
                if (usuario.Id == 0) //Se o Id for 0 o produto e Novo, entao deve Inserir
                    sql = string.Format(Sql_Insert, usuario.Nome, usuario.Login, usuario.Senha); 
                else //produto com Id entao os dados devem ser alterados
                    sql = string.Format(Sql_Update, usuario.Id, usuario.Nome, usuario.Login, usuario.Senha);

                result = ExecuteCommand(sql);

                return result;
            }
            
           
        }

        public List<Usuario> Listar()
        {
            var connection = GetConnection();
            connection.Open();
            var command = new SQLiteCommand(connection);


            command.CommandText = Sql_Select;
            var result = new List<Usuario>();

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var usuario = Parse(reader);
                    result.Add(usuario);
                }
            }
            command.Dispose();
            connection.Close();
            connection.Dispose();

            return result;
        }
 
        public Usuario Carregar(int id)
        {
            var connection = GetConnection();
            connection.Open();
            var command = new SQLiteCommand(connection);

            command.CommandText = string.Format(Sql_SelectOne, id);
            var result = new Usuario();

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

        private Usuario Parse(SQLiteDataReader reader)
        {
            var usuario = new Usuario()
            {
                Id = int.Parse(reader["Id"].ToString()),
                Nome = reader["Nome"].ToString().ToLower(),
                Login = reader["Login"].ToString().ToLower(),
                Senha = reader["Senha"].ToString().ToLower(),
            };

            return usuario;
        }

        public bool ValidarLogin(Usuario usuario)
        {
            var aux = false;

            var connection = GetConnection();
            connection.Open();
            var command = new SQLiteCommand(connection);

            command.CommandText = string.Format(Sql_SelectLoginSenha, usuario.Login, usuario.Senha);
            var result = new Usuario();

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

            if (result.Login != null)
            {
                aux = true;
            }

            return aux;
        }

        public bool ValidarUsuarioJaCadastrado(Usuario usuario)
        {
            var aux = false;

            var connection = GetConnection();
            connection.Open();
            var command = new SQLiteCommand(connection);

            command.CommandText = string.Format(Sql_SelectLogin, usuario.Login);
            var result = new Usuario();

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

            if (result.Login == usuario.Login)
            {
                aux = true;
            }

            return aux;
        }

        public List<Usuario> BuscarUsuario(string pesquisa)
        {
            var connection = GetConnection();
            connection.Open();
            var command = new SQLiteCommand(connection);


            command.CommandText = string.Format(Sql_SelectNomeLogin, pesquisa);
            var result = new List<Usuario>();

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var user = Parse(reader);
                    result.Add(user);
                }
            }
            command.Dispose();
            connection.Close();
            connection.Dispose();

            return result;
        }
    }
}