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
    public partial class frmCliente : Form
    {
        public frmCliente()
        {
            InitializeComponent();
        }
        SqlConnection cn = new SqlConnection(Properties.Settings.Default.dbServicoConnectionString);
        SqlCommand cmd = null;

        private void tbClienteBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            try
            {
                //Desabilita o botão excluir para quem tiver nivel de acesso Operador
                 if (frmLogin.NivelAcesso == "Operador")
                {
                    bindingNavigatorDeleteItem.Enabled = false;
                }
                if ((nomeTextBox.Text != "") && (cpfMaskedTextBox.Text != ""))
                {
                    //Insere a Data
                    if (dataDiaTextBox.Text == "")
                    {
                        dataDiaTextBox.Text = DateTime.Now.ToString();
                    }
                    //Mostrar o usuário que cadastrou 
                    if (cadastradoPorTextBox.Text == "")
                    {
                        cadastradoPorTextBox.Text = frmLogin.usuarioConectado;
                    }
                    //Verificar o cpf
                    if (cpf.ValidarCPF(cpfMaskedTextBox.Text))
                    {
                        this.Validate();
                        this.tbClienteBindingSource.EndEdit();

                        this.tbClienteTableAdapter.Update(this.dbServicoDataSet.tbCliente);
                    }
                    else
                    {
                        MessageBox.Show("CPF incorreto",
                        "Aviso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("O Campo 'Nome' e 'CPF não podem  ficar vazio",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possível salvar pelo seguinte motivo: " + ex.Message);
            }

            //this.Validate();
            //this.tbClienteBindingSource.EndEdit();
            //this.tableAdapterManager.UpdateAll(this.dbServicoDataSet);

        }

        private void LimparCampo()
        {
            idClienteTextBox.Clear();
            nomeTextBox.Clear();
            cpfMaskedTextBox.Clear();
            telefoneMaskedTextBox.Clear();
            cepTextBox.Clear();
            logradouroTextBox.Clear();
            numeroTextBox.Clear();
            bairroTextBox.Clear();
            cidadeTextBox.Clear();
            estadoTextBox.Clear();
            dataDiaTextBox.Clear();
            emailTextBox.Clear();
            cadastradoPorTextBox.Clear();
        }

        private void frmCliente_Load(object sender, EventArgs e)
        {
            // TODO: esta linha de código carrega dados na tabela 'dbServicoDataSet.tbOrdemServico'. Você pode movê-la ou removê-la conforme necessário.
            this.tbOrdemServicoTableAdapter.Fill(this.dbServicoDataSet.tbOrdemServico);
            // TODO: esta linha de código carrega dados na tabela 'dbServicoDataSet.tbCliente'. Você pode movê-la ou removê-la conforme necessário.
            this.tbClienteTableAdapter.Fill(this.dbServicoDataSet.tbCliente);

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbmFiltrar.Text == "Código")
                {
                    //Define a instrução Sql
                    string sql = "SELECT * FROM tbCliente WHERE idCliente = " + txtPesquisar.Text + "";
                    //Lê os dados da variavel sql e conectar no cn 
                    cmd = new SqlCommand(sql, cn);
                    //Abre conexão
                    cn.Open();
                    //Define o valor da CommandType para cmd
                    cmd.CommandType = CommandType.Text;
                    /*Representa um conjunto de comandos de dados e uma conexão de banco de dados 
                    que são usados para preencher o DataSet e atualizar um banco de dados SQL Server.*/
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    //Representa uma tabela de dados na memória.
                    DataTable cliente = new DataTable();
                    /* Adiciona ou atualiza linhas em um DataTable para que correspondam na fonte de 
                    * dados usando o DataTable.*/
                    da.Fill(cliente);
                    /*A tbUsuarioDataGridView recebe o DataTable usuario*/
                    tbClienteDataGridView.DataSource = cliente;
                    //Fechar a conexão
                }
                if (cbmFiltrar.Text == "Nome")
                {
                    //define a instrução SQL
                    string sql = "SELECT * FROM tbCliente WHERE nome LIKE '%" + txtPesquisar.Text + "%'";
                     cmd = new SqlCommand(sql, cn);
                    cn.Open();
                    cmd.CommandType = CommandType.Text;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable cliente = new DataTable();
                    da.Fill(cliente);
                    tbClienteDataGridView.DataSource = cliente;
                }
                if (cbmFiltrar.Text == "CPF")
                {
                    //define a instrução SQL
                    string sql = "SELECT * FROM tbCliente WHERE cpf = '" + txtPesquisar.Text + "'";
                     cmd = new SqlCommand(sql, cn);
                    cn.Open();
                    cmd.CommandType = CommandType.Text;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable cliente = new DataTable();
                    da.Fill(cliente);
                    tbClienteDataGridView.DataSource = cliente;
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

        private void cbmFiltrar_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbmFiltrar.Text == "Código")
            {
                txtPesquisar.Mask = "";
            }
            if (cbmFiltrar.Text == "Nome")
            {
                txtPesquisar.Mask = "";
            }
            if (cbmFiltrar.Text == "CPF")
            {
                txtPesquisar.Mask = "000,000,000-00";
            }
            if (cbmFiltrar.Text == "")
            {
                txtPesquisar.Mask = "";
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                //define a instrução SQL
                string sql = "SELECT * FROM tbOrdemServico WHERE idCliente = '" + idClienteTextBox.Text + "'";
                cmd = new SqlCommand(sql, cn);
                cn.Open();
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable os = new DataTable();
                da.Fill(os);
                tbOrdemServicoDataGridView.DataSource = os;
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
            if (frmLogin.NivelAcesso == "Operador")
            {
                bindingNavigatorDeleteItem.Enabled = false;
            }
        }

        private void tbClienteDataGridView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LimparCampo();
            idClienteTextBox.Text = tbClienteDataGridView.CurrentRow.Cells[0].Value.ToString();
            nomeTextBox.Text =    tbClienteDataGridView.CurrentRow.Cells[1].Value.ToString();
            cpfMaskedTextBox.Text =  tbClienteDataGridView.CurrentRow.Cells[2].Value.ToString();
            telefoneMaskedTextBox.Text = tbClienteDataGridView.CurrentRow.Cells[3].Value.ToString();
            logradouroTextBox.Text = tbClienteDataGridView.CurrentRow.Cells[4].Value.ToString();
            numeroTextBox.Text = tbClienteDataGridView.CurrentRow.Cells[5].Value.ToString();
            bairroTextBox.Text =   tbClienteDataGridView.CurrentRow.Cells[6].Value.ToString();
            cidadeTextBox.Text =   tbClienteDataGridView.CurrentRow.Cells[7].Value.ToString();
            cepTextBox.Text = tbClienteDataGridView.CurrentRow.Cells[8].Value.ToString();
            estadoTextBox.Text =   tbClienteDataGridView.CurrentRow.Cells[9].Value.ToString();
            dataDiaTextBox.Text =   tbClienteDataGridView.CurrentRow.Cells[10].Value.ToString();
            emailTextBox.Text = tbClienteDataGridView.CurrentRow.Cells[11].Value.ToString();
            cadastradoPorTextBox.Text =  tbClienteDataGridView.CurrentRow.Cells[12].Value.ToString();
        }
    }
}
