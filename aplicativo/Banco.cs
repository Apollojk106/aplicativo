using MySql.Data.MySqlClient;
using System.Data;
namespace Aplicativo
{
    internal class Banco

    {
        public string Nome;
        public double Valor;
        public string Data;

        public Banco(string nome, double valor, string data) 
        { 
            Nome = nome;
            Valor = valor; 
            Data = data;
        }

        const string Conexao = "server=localhost;uid=root;pwd=jk106;database=clientes";

        public void Enviar() 
        {
            try
            {
                using (MySqlConnection conexao = new MySqlConnection(Conexao))
                {
                    conexao.Open();

                    string query = $"insert into pedido (nome, dia, valor) values ('{Nome}', '{Data}', '{Valor}');";
                    MySqlCommand cmd = new MySqlCommand(query, conexao);

                    cmd.Parameters.AddWithValue("nome", Nome);
                    cmd.Parameters.AddWithValue("valor", double.Parse($"{Valor}"));
                    cmd.Parameters.AddWithValue("dia", Data);

                    cmd.ExecuteNonQuery();

                    conexao.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro com o banco de dados: " + ex.Message);
            }
        }

        public DataTable ObterDadosDaTabela()
        {
            using (MySqlConnection conexao = new MySqlConnection(Conexao))
            {
                try
                {
                    conexao.Open();

                    string query = $"select * from pedido;";
                    MySqlCommand cmd = new MySqlCommand(query, conexao);

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        return dataTable;
                    }
                }
                finally
                {
                    if (conexao.State == ConnectionState.Open)
                        conexao.Close();
                }
            }

        }

        public void Deletar(int id) 
        {
            try 
            {
                using (MySqlConnection conexao = new MySqlConnection(Conexao)) 

                {
                    conexao.Open();

                    string quary = $"delete from pedido where id = {id}";

                    using (MySqlCommand cmd = new MySqlCommand(quary, conexao))
                    { 
                        int LinhasAfetadas = cmd.ExecuteNonQuery();

                        if (LinhasAfetadas > 0)
                        {
                            MessageBox.Show($"Exclusão bem-sucedida!");
                        }
                        else
                        {
                            MessageBox.Show($"Nenhuma linha foi excluída. Verifique o ID fornecido.");
                        }
                    }
                    conexao.Close();
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show("Erro: " + ex.Message);
            }
        }

    }
}
