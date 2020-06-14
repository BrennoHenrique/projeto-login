using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppTrabalho.Models
{
    public class Controle
    {
        Conexao conexao = new Conexao();
        private String menssagem;
        private String nome;

        public String Nome
        {
            get { return nome; }
            set { nome = value; }
        }

        public String Menssagem
        {
            get { return menssagem; }
            set { menssagem = value; }
        }

        public Controle()
        { Menssagem = string.Empty; }

        public void Cadastrar(string nome, string email, string senha, string confSenha)
        {
            SqlCommand command = new SqlCommand();
            nome = nome.ToUpper();

            try
            {
                command = new SqlCommand($"INSERT INTO {Conexao.Tabela} VALUES (@nome, @email, @senha, @num)");

                if (nome == null)
                    Menssagem = "Nome inválido!";
                else if (email == null)
                    Menssagem = "Email inválido!";
                else if (senha == null)
                    Menssagem = "Senha Inválida!";
                else if (senha.Length < 8)
                    Menssagem = "Senha deve ter 8 ou mais caracteres!";
                else if (!senha.Equals(confSenha))
                    Menssagem = "Confirme a senha!";
                else
                {
                    if (!ExistEmail(email))
                    {
                        command.Connection = conexao.Conectar();

                        command.Parameters.AddWithValue("@nome", nome);
                        command.Parameters.AddWithValue("@email", email);
                        command.Parameters.AddWithValue("@senha", senha);
                        command.Parameters.AddWithValue("@num", 0);

                        command.ExecuteNonQuery();
                        Menssagem = "Cadastrado com sucesso!";
                    }
                    else
                    {
                        Menssagem = Menssagem.Equals(string.Empty) ? "Email já cadastrado!" : Menssagem;
                    }
                }
                command.Connection = conexao.Desconectar();
            }
            catch
            {
                Menssagem = "Erro no cadastro!";
                command.Connection = conexao.Desconectar();
            }
        }

        public void AutenticarUsuario(string email, string senha)
        {
            SqlCommand command = new SqlCommand($"UPDATE {Conexao.Tabela} SET Autenticacao_usuario=@value WHERE {Conexao.Email}=@email AND " +
                                                $"{Conexao.Senha}=@senha");
            command.Parameters.AddWithValue("@email", email);
            command.Parameters.AddWithValue("@senha", senha);

            try
            {
                SqlDataReader rows = ExistUsuario(email, senha);

                if (rows.Read())
                {
                    if (rows[4].ToString() == "0")
                    {
                        command.Parameters.AddWithValue("@value", 1);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@value", 0);
                    }

                    rows.Close();
                    command.Connection = conexao.Conectar();
                    command.ExecuteNonQuery();
                }
                command.Connection = conexao.Desconectar();
            }
            catch
            {
                command.Connection = conexao.Desconectar();
                Console.WriteLine("Error");
            }
        }

        public bool VerificaAutenticacao(string email, string senha)
        {
            SqlCommand command = new SqlCommand($"SELECT * FROM {Conexao.Tabela} WHERE {Conexao.Email}=@email AND {Conexao.Senha}=@senha");
            command.Parameters.AddWithValue("@email", email);
            command.Parameters.AddWithValue("@senha", senha);

            try
            {
                command.Connection = conexao.Conectar();
                SqlDataReader rows = ExistUsuario(email, senha);

                if (rows.Read())
                {
                    if (rows[4].ToString() == "1")
                    {
                        Nome = rows[1].ToString();
                        command.Connection = conexao.Desconectar();
                        rows.Close();
                        return true;
                    }
                    else
                    {
                        command.Connection = conexao.Desconectar();
                        rows.Close();
                        return false;
                    }
                }
                else
                {
                    command.Connection = conexao.Desconectar();
                    rows.Close();
                    return false;
                }
            }
            catch
            {
                command.Connection = conexao.Desconectar();
                return false;
            }
        }

        public SqlDataReader ExistUsuario(string email, string senha)
        {
            SqlDataReader rows;
            SqlCommand command = new SqlCommand($"SELECT * FROM {Conexao.Tabela} WHERE {Conexao.Email}=@email AND {Conexao.Senha}=@senha");
            command.Parameters.AddWithValue("@email", email);
            command.Parameters.AddWithValue("@senha", senha);

            command.Connection = conexao.Conectar();
            rows = command.ExecuteReader();

            return rows;
        }

        public bool ExistEmail(string email)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = $"SELECT * FROM {Conexao.Tabela} WHERE {Conexao.Email}=@email";

            sqlCommand.Parameters.AddWithValue("@email", email);

            try
            {
                sqlCommand.Connection = conexao.Conectar();
                SqlDataReader rows = sqlCommand.ExecuteReader();

                if (rows.Read())
                {
                    sqlCommand.Connection = conexao.Desconectar();
                    rows.Close();

                    return true;
                }
                else
                {
                    sqlCommand.Connection = conexao.Desconectar();
                    rows.Close();

                    return false;
                }
            }
            catch
            {
                Menssagem = "Erro no cadastro!";
                sqlCommand.Connection = conexao.Desconectar();
                return true;
            }
        }
    }
}
