using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Kstudio_v2.Models
{
    public class ContatoModel : IDisposable
    {
        private SqlConnection connection;

        public ContatoModel()
        {
            string strConn = "Data Source=localhost;Initial Catalog=BDContato;Integrated Security=true";
            connection = new SqlConnection(strConn);
            connection.Open();
        }

        public void Dispose()
        {
            connection.Close();
        }

        public List<Contato> Read()
        {
            List<Contato> lista = new List<Contato>();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"SELECT * FROM Contato";

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Contato contato = new Contato();
                contato.IdContato = (int)reader["IdContato"];
                contato.Nome = (string)reader["Nome"];
                contato.Email = (string)reader["Email"];

                lista.Add(contato);
            }

            return lista;
        }

        public void Create(Contato contato)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"INSERT INTO Contato VALUES (@nome, @email)";

            cmd.Parameters.AddWithValue("@nome", contato.Nome);
            cmd.Parameters.AddWithValue("@nome", contato.Email);

            cmd.ExecuteNonQuery();

        }

        public void Update(Contato contato)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"UPDATE Contato Set Nome=@nome, Email=@email WHERE IdContato=@id";

            cmd.Parameters.AddWithValue("@nome", contato.Nome);
            cmd.Parameters.AddWithValue("@nome", contato.Email);
            cmd.Parameters.AddWithValue("@Id", contato.IdContato);

            cmd.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"DELETE FROM Contato WHERE IdContato=@id";

            cmd.Parameters.AddWithValue("@Id", id);

            cmd.ExecuteNonQuery();
        }
    }

   
}