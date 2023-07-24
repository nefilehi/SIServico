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
using System.Data;

namespace SIServico
{
    public partial class frmServico : Form
    {
        SqlConnection cn = new SqlConnection(Properties.Settings.Default.dbServicoConnectionString);
        SqlCommand cmd = null;
        public frmServico()
        {
            InitializeComponent();
        }
        private void tbServicoBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            try
            {
                //Desabilita o botão excluir para quem tiver nivel de acesso Operador
                 if (frmLogin.NivelAcesso == "")
                {
                    bindingNavigatorDeleteItem.Enabled = false;
                }
                //Se os campos estiver preenchido faça
                if (nomeTextBox.Text != "")
                {

                    //Mostrar a Data do Cadastro na Hora
                    if (dataDiaTextBox.Text == "")
                    {
                        dataDiaTextBox.Text =
                       DateTime.Now.ToString();
                    }
                    //Mostrar quem Cadastrou o usuario
                    if (cadastradoPorTextBox.Text == "")
                    {
                        cadastradoPorTextBox.Text =
                       frmLogin.usuarioConectado;
                    }
                    //Executar a aplicação
                    this.Validate();
                    this.tbServicoBindingSource.EndEdit();
                    this.tbServicoTableAdapter.Update(this.dbServicoDataSet.tbServico);
                    MessageBox.Show("Cadastrado realizado com sucesso");
                }

                else
                {
                    //Ser os campos não estiverem preenchido
                    MessageBox.Show("O Campo nome não pode ficar vazio");
                }
            }
            catch (Exception ex)
            {
                //Caso haja uma exceção será tratada neste código
                MessageBox.Show("Não foi possível salvar pelo seguinte motivo: " + ex.Message);
            }
            //this.Validate();
            //this.tbServicoBindingSource.EndEdit();
            //this.tableAdapterManager.UpdateAll(this.dbServicoDataSet);

        }

                private void frmServico_Load(object sender, EventArgs e)
        {
            // TODO: esta linha de código carrega dados na tabela 'dbServicoDataSet.tbServico'. Você pode movê-la ou removê-la conforme necessário.
            this.tbServicoTableAdapter.Fill(this.dbServicoDataSet.tbServico);

        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbmFiltrar.Text == "Código")
                {
                    //Define a instrução Sql
                    string sql = "SELECT * FROM tbServico WHERE idServico = " + txtPesquisar.Text + "";
                    //Lê os dados da variavel sql e conectar no cn
                    SqlCommand cmd = new SqlCommand(sql, cn);
                    //Abre conexão
                    cn.Open();
                    //Define o valor da CommandType para cmd
                    cmd.CommandType = CommandType.Text;
                    /*Representa um conjunto de comandos de dados e uma conexão de banco de dados 
                    que são usados para preencher o DataSet e atualizar um banco de dados SQL Server.*/
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    //Representa uma tabela de dados na memória.
                    DataTable servico = new DataTable();
                    /* Adiciona ou atualiza linhas em um DataTable para que correspondam na fonte de 
                    * dados usando o DataTable.*/
                    da.Fill(servico);
                    /*A tbUsuarioDataGridView recebe o DataTable usuario*/
                    tbServicoDataGridView.DataSource = servico;
                                    }
                if (cbmFiltrar.Text == "Nome")
                {
                    //define a instrução SQL
                    string sql = "SELECT * FROM tbServico WHERE nome LIKE '%" + txtPesquisar.Text + "%'";
                    cmd = new SqlCommand(sql, cn);
                    cn.Open();
                    cmd.CommandType = CommandType.Text;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable servico = new DataTable();
                    da.Fill(servico);
                    tbServicoDataGridView.DataSource = servico;
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

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            //Desabilita o botão excluir para quem tiver nivel de acesso Operador
            if (frmLogin.NivelAcesso == "")
            {
                bindingNavigatorDeleteItem.Enabled = false;
            }
        }
        private void LimparCampo() 
        {
            idServicoTextBox.Clear();
            nomeTextBox.Clear();
            descricaoTextBox.Clear();
            valorTextBox.Clear();
            dataDiaTextBox.Clear();
            cadastradoPorTextBox.Clear();
        }

        private void tbServicoDataGridView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LimparCampo();
            idServicoTextBox.Text =   tbServicoDataGridView.CurrentRow.Cells[0].Value.ToString();
            nomeTextBox.Text =        tbServicoDataGridView.CurrentRow.Cells[1].Value.ToString();
            descricaoTextBox.Text =   tbServicoDataGridView.CurrentRow.Cells[2].Value.ToString();
            observacaoTextBox.Text =  tbServicoDataGridView.CurrentRow.Cells[3].Value.ToString();
            valorTextBox.Text =       tbServicoDataGridView.CurrentRow.Cells[4].Value.ToString();
            dataDiaTextBox.Text =     tbServicoDataGridView.CurrentRow.Cells[5].Value.ToString();
            cadastradoPorTextBox.Text = tbServicoDataGridView.CurrentRow.Cells[6].Value.ToString();
        }
    }
}
