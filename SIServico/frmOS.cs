using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SIServico
{
    public partial class frmOS : Form
    {
        public frmOS()
        {
            InitializeComponent();
        }

        private void tbOrdemServicoBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.tbOrdemServicoBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.dbServicoDataSet);

        }

        private void frmOS_Load(object sender, EventArgs e)
        {
            // TODO: esta linha de código carrega dados na tabela 'dbServicoDataSet.tbOrdemServico'. Você pode movê-la ou removê-la conforme necessário.
            this.tbOrdemServicoTableAdapter.Fill(this.dbServicoDataSet.tbOrdemServico);

        }
    }
}
