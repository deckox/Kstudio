using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kstudio_v2.Core.Repositories
{
    public class AgendamentosRepository : BaseRepository
    {
        private const string Sql_Insert = "INSERT into Usuarios (Nome,Login,Senha) VALUES ('{0}','{1}','{2}')";
        private const string Sql_Update = "UPDATE Usuarios SET Nome='{1}',Login='{2}',Senha='{3}' WHERE Id = {0}";
        private const string Sql_Delete = "DELETE from Usuarios WHERE Id = {0}";
        private const string Sql_Select = "SELECT * from Usuarios";
        private const string Sql_SelectOne = "SELECT * from Usuarios WHERE Id={0}";
        private const string Sql_SelectLogin = "SELECT * from Usuarios WHERE Login='{0}'";
        private const string Sql_SelectLoginSenha = "SELECT * from Usuarios WHERE Login='{0}' AND Senha='{1}'";
        private const string Sql_SelectNomeLogin = "SELECT * from Usuarios WHERE Nome LIKE '%{0}%' OR Login LIKE '%{0}%'";
    }
}