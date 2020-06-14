using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace WebAppTrabalho.Models
{
    public class Conexao
    {
        public static String Tabela { get; } = "tblUsuario";
        public static String Nome { get; } = "Nome_usuario";
        public static String Email { get; } = "Email_usuario";
        public static String Senha { get; } = "Senha_usuario";

        SqlConnection con;
        public Conexao()
        {
            string stringConexao = "Data Source=NOTEBOOK-BRENNO;Initial Catalog=DBAppWebTrabalho;Integrated Security=True";
            con = new SqlConnection(stringConexao);
        }

        public SqlConnection Conectar()
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            return con;
        }

        public SqlConnection Desconectar()
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }

            return con;
        }
    }
}
