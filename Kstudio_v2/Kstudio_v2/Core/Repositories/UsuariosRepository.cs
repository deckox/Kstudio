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
    public class UsuariosRepository : BaseRepository
    {
        private const string Sql_Insert = "INSERT into Usuarios (Nome,Login,Senha) VALUES ('{0}','{1}','{2}')";
        private const string Sql_Update = "UPDATE Usuarios SET Nome='{1}',Login='{2}',Senha='{3}' WHERE Id = {0}";
        private const string Sql_Delete = "DELETE from Usuarios WHERE Id = {0}";
        private const string Sql_Select = "SELECT * from Usuarios";
        private const string Sql_SelectOne = "SELECT * from Usuarios WHERE Id={0}";

        public bool Excluir(int id)
        {
            var sql = string.Format(Sql_Delete, id);
            return ExecuteCommand(sql);
        }

        public bool Salvar(Usuario usuario)
        {
            var sql = "";

            if (usuario.Id == 0) //Se o Id for 0 o produto e Novo, entao deve Inserir
            sql = string.Format(Sql_Insert, usuario.Nome, usuario.Login, usuario.Senha);
            else //produto com Id entao os dados devem ser alterados
            sql = string.Format(Sql_Update, usuario.Id, usuario.Nome, usuario.Login, usuario.Senha);

            var result = ExecuteCommand(sql);
            return result;
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

        public bool ValidarLogin(Usuario usuario)
        {
            var listaDeUsuarioDoBD = Listar();
            var result = false;

            for (int i = 0; i < listaDeUsuarioDoBD.Count; i++)
            {
                if (listaDeUsuarioDoBD[i].Login == usuario.Login && listaDeUsuarioDoBD[i].Senha == usuario.Senha)
                {
                    result = true;
                    break;
                }
            }

            return result;
            
        }
    }
}