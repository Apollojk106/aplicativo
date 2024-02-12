using System.Globalization;
using System.Data;

namespace Aplicativo
{
    public partial class Form1 : Form
    {
        public string nome;
        public string data;
        public double valor;
        public int id;
        DateTime dataAtual = DateTime.Now;
        public Form1()
        {
            InitializeComponent();
            Banco y = new Banco(nome, valor, data);
            DataTable dados = y.ObterDadosDaTabela();
            dataGridView1.DataSource = dados;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                nome = textBox1.Text;

                string temp = textBox2.Text;
                valor = double.Parse(temp.Replace(",","."));

                label4.Visible = false;

                Banco x;
                data = $"{dataAtual.Year}-{dataAtual.Month}-{dataAtual.Day}";

                if (valor > 0.0 && nome != null && nome != " ")
                {
                    x = new Banco(nome, valor, data);
                    x.Enviar();

                    MessageBox.Show("Cadastro Realizado!");
                    textBox1.Text = textBox2.Text = "";

                    DataTable dados = x.ObterDadosDaTabela();
                    dataGridView1.DataSource = dados;

                }
                else
                {
                    label4.Visible = true;
                }

            }
            catch
            {
                label4.Visible = true;
            }
        }

        void button1_Click(object sender, EventArgs e)
        {
            try
            {
                id = int.Parse(textBox3.Text);

                Banco x = new Banco("null", 0, data);
                x.Deletar(id);

                DataTable dados = x.ObterDadosDaTabela();
                dataGridView1.DataSource = dados;

                label7.Visible = false;
            }
            catch 
            { 
                label7.Visible = true;
            }
                
        }
    }
}
