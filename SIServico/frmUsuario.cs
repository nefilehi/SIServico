using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;

namespace SIServico
{
    public partial class frmUsuario : Form
    {
        SqlConnection cn = new SqlConnection(Properties.Settings.Default.dbServicoConnectionString);

        private void LimparCampo()
        {
            idUsuarioTextBox.Clear();
            usuarioTextBox.Clear();
            senhaTextBox.Clear();
            repitaSenhaTextBox.Clear();
            nivelAcessoComboBox.SelectedIndex = -1;
            dataDiaTextBox.Clear();
            cadastradorPorTextBox.Clear();


        }
        public frmUsuario()
        {
            InitializeComponent();
        }

        private void tbUsuarioBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.tbUsuarioBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.dbServicoDataSet);

        }

        private void tbUsuarioBindingNavigatorSaveItem_Click_1(object sender, EventArgs e)
        {
            this.Validate();
            this.tbUsuarioBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.dbServicoDataSet);

        }

        private void tbUsuarioBindingNavigatorSaveItem_Click_2(object sender, EventArgs e)
        {
            this.Validate();
            this.tbUsuarioBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.dbServicoDataSet);

        }

        private void tbUsuarioBindingNavigatorSaveItem_Click_3(object sender, EventArgs e)
        {
            try
            {
                //Se os campos estiver preenchido faça
                if ((usuarioTextBox.Text != "") && (nivelAcessoComboBox.Text != "") && (senhaTextBox.Text != ""))
                {
                    //Se as senhas forem igual faça
                    if (senhaTextBox.Text == repitaSenhaTextBox.Text)
                    {
                        //Mostrar a Data do Cadastro na Hora
                        if (dataDiaTextBox.Text == "")
                        {
                            dataDiaTextBox.Text =
                           DateTime.Now.ToString();
                        }
                        //Mostrar quem Cadastrou o usuario
                        if (cadastradorPorTextBox.Text == "")
                        {
                            cadastradorPorTextBox.Text =
                           frmLogin.usuarioConectado;
                        }
                        //Executar a aplicação
                        this.Validate();
                        this.tbUsuarioBindingSource.EndEdit();
                        MessageBox.Show("Cadastrado realizado com sucesso");
                        this.tbUsuarioTableAdapter.Update(this.dbServicoDataSet.tbUsuario);
                    }
                    else
                    {
                        //Caso as senhas são diferentes
                        MessageBox.Show("As senhas estão diferentes");
                    }
                }
                else
                {
                    //Ser os campos não estiverem preenchido
                    MessageBox.Show("Todos os campos não podem ficar vazios");
                }
            }
            catch (Exception ex)
            {
                //Caso haja uma exceção será tratada neste código
                MessageBox.Show("Não foi possível salvar pelo seguinte motivo: " + ex.Message);
            }
            // this.Validate();
            // this.tbUsuarioBindingSource.EndEdit();
            //this.tableAdapterManager.UpdateAll(this.dbServicoDataSet);
        }

        private void frmUsuario_Load(object sender, EventArgs e)
        {
            // TODO: esta linha de código carrega dados na tabela 'dbServicoDataSet.tbUsuario'. Você pode movê-la ou removê-la conforme necessário.
            this.tbUsuarioTableAdapter.Fill(this.dbServicoDataSet.tbUsuario);

        }

        private void tbUsuarioDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbmFiltrar.Text == "Código")
                {
                    //Define a instrução Sql
                    string sql = "SELECT * FROM tbUsuario WHERE idUsuario = " + txtPesquisar.Text + "";
               
                    //Lê os dados da variavel sql e conectar no cn
                    SqlCommand cmd = new SqlCommand(sql, cn);
                    //Abre conexão
                    cn.Open();
                    //Define o valor da CommandType para cmd
                    cmd.CommandType = CommandType.Text;
                    /*Representa um conjunto de comandos de dados e 
                   uma conexão de banco de dados 
                    que são usados para preencher o DataSet e 
                   atualizar um banco de dados SQL Server.*/
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    //Representa uma tabela de dados na memória.
                    DataTable usuario = new DataTable();
                    /* Adiciona ou atualiza linhas em um DataTable 
                   para que correspondam na fonte de 
                    * dados usando o DataTable.*/
                    da.Fill(usuario);
                    /*A tbUsuarioDataGridView recebe o DataTable 
                   usuario*/
                    tbUsuarioDataGridView.DataSource = usuario;
                    //Fechar a conexão

                }
                if (cbmFiltrar.Text == "Usuário")
                {
                    //define a instrução SQL
                    string sql = "SELECT * FROM tbUsuario WHERE usuario LIKE '%" + txtPesquisar.Text + "%'";
                    SqlCommand cmd = new SqlCommand(sql, cn);
                    cn.Open();
                    cmd.CommandType = CommandType.Text;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable usuario = new DataTable();
                    da.Fill(usuario);
                    tbUsuarioDataGridView.DataSource = usuario;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cn.Close();
            }
        }

        private void tbUsuarioDataGridView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LimparCampo();
            idUsuarioTextBox.Text = tbUsuarioDataGridView.CurrentRow.Cells[0].Value.ToString();
            usuarioTextBox.Text =   tbUsuarioDataGridView.CurrentRow.Cells[1].Value.ToString();
            senhaTextBox.Text =     tbUsuarioDataGridView.CurrentRow.Cells[2].Value.ToString();
            repitaSenhaTextBox.Text = tbUsuarioDataGridView.CurrentRow.Cells[3].Value.ToString();
            nivelAcessoComboBox.Text = tbUsuarioDataGridView.CurrentRow.Cells[4].Value.ToString();
            dataDiaTextBox.Text =  tbUsuarioDataGridView.CurrentRow.Cells[5].Value.ToString();
            cadastradorPorTextBox.Text = tbUsuarioDataGridView.CurrentRow.Cells[6].Value.ToString();
        }
    }
}
