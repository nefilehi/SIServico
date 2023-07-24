using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//Classes responsáveis por trabalhar com SQL
using System.Data.Sql;
using System.Data.SqlClient;

namespace SIServico
{
    public partial class frmLogin : Form
    {
        //Responsável pelo nivel de acesso 
        public static string NivelAcesso;
        //Responsável por mostrar quem está conectado ao sistema
        public static string usuarioConectado;

        SqlConnection conn = new SqlConnection(Properties.Settings.Default.dbServicoConnectionString);
        public frmLogin()
        {
            InitializeComponent();
        }

        private void tbUsuarioBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.tbUsuarioBindingSource.EndEdit();
            //this.tableAdapterManager.UpdateAll(this.dbServicoDataSet);

        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            usuarioComboBox.SelectedIndex = -1;
            // TODO: esta linha de código carrega dados na tabela 'dbServicoDataSet.tbUsuario'. Você pode movê-la ou removê-la conforme necessário.
            this.tbUsuarioTableAdapter.Fill(this.dbServicoDataSet.tbUsuario);
            // TODO: esta linha de código carrega dados na tabela 'dbServicoDataSet.tbUsuario'. Você pode movê-la ou removê-la conforme necessário.
            this.tbUsuarioTableAdapter.Fill(this.dbServicoDataSet.tbUsuario);

        }

        private void senhaLabel_Click(object sender, EventArgs e)
        {

        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            try
            {
                //Verificar ser os campos estão preenchidos
                if ((usuarioComboBox.Text != "") && (nivelAcessoComboBox.Text != "") && (senhaTextBox.Text != ""))
                {
                    //Responsavel pelo Comando Sql
                    SqlCommand comm = new SqlCommand("Select * From tbUsuario Where usuario = @usuario and " + "senha = @senha and nivelAcesso=@nivel", conn);
                    //Parametizar os codigos
                    comm.Parameters.Add("@usuario",SqlDbType.VarChar).Value = usuarioComboBox.Text;
                    comm.Parameters.Add("@senha", SqlDbType.VarChar).Value  = senhaTextBox.Text;
                    comm.Parameters.Add("@nivel", SqlDbType.VarChar).Value  = nivelAcessoComboBox.Text;
                    //Abre a conexão
                    conn.Open();
                    SqlDataReader reader = null;
                    //lê as linhas de uma base de dados SQL Server
                    reader = comm.ExecuteReader();
                    //Se tiver coisa pra lê faça:
                    if (reader.Read())
                    {
                        //Variaveil usuarioConectado recebe campo usuarioComboBox.Text
                         usuarioConectado = usuarioComboBox.Text;
                        //Variavei nivelAcesso recebe o campo nivelAcessoComboBox.Text
                         NivelAcesso = nivelAcessoComboBox.Text;
                        //Declara a variavel que recebe o formulario frmTelaPrinciapal
                         frmTelaPrincipal p = new frmTelaPrincipal();
                        //Esconde o formulario Tela de Login
                        this.Hide();
                        //Mostrar o formulario frmTelaPrinciapl
                        p.Show();
                    }
                    else
                    {
                        MessageBox.Show("Usuário e/ou senha incorretas",
                        "Aviso de Segurança",
                       MessageBoxButtons.OK,
                       MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Todos os Campos são obrigatórios",
                   "Aviso de Segurança",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Information);
                }

            }
            catch (Exception ex)
            {
                //Gerar a exceção
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //Finalizar tarefa
                conn.Close();
            }
        }

        private void frmLogin_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmUsuario Usr = new frmUsuario();
            Usr.Show();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            frmUsuario usu = new frmUsuario();
            usu.Show();
        }
    }
}
