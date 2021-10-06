using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Procedure
{
    public partial class Form1 : Form
    {
        SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Programas\\Procedure\\DbProcedure.mdf;Integrated Security=True");

        public Form1()
        {
            InitializeComponent();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void CarregaDgv()
        {
            String sql = "SELECT * FROM Teste";
            SqlCommand cmd = new SqlCommand(sql, con);
            con.Open();
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable teste = new DataTable();
            da.Fill(teste);
            dgvProcedure.DataSource = teste;
            con.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CarregaDgv();
        }

        private void btnInserir_Click(object sender, EventArgs e)
        {
            try
            {
                decimal total = Convert.ToDecimal(txtValor.Text) * Convert.ToDecimal(txtQuantidade.Text);
                con.Open();
                SqlCommand cmd = new SqlCommand("Inserir", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@valor", SqlDbType.Decimal).Value = Convert.ToDecimal(txtValor.Text.Trim());
                cmd.Parameters.AddWithValue("@quantidade", SqlDbType.Int).Value = Convert.ToInt32(txtQuantidade.Text.Trim());
                cmd.Parameters.AddWithValue("@data", SqlDbType.Date).Value = DateTime.Now.Date;
                cmd.Parameters.AddWithValue("@total", SqlDbType.Decimal).Value = total;
                cmd.ExecuteNonQuery();
                MessageBox.Show("Registro inserido com sucesso!", "Inserir", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                CarregaDgv();
                txtValor.Text = "";
                txtQuantidade.Text = "";
                con.Close();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void dgvProcedure_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dgvProcedure.Rows[e.RowIndex];
                txtId.Text = row.Cells[0].Value.ToString();
                txtValor.Text = row.Cells[1].Value.ToString();
                txtQuantidade.Text = row.Cells[2].Value.ToString();
            }
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            try
            {
                decimal total = Convert.ToDecimal(txtValor.Text) * Convert.ToDecimal(txtQuantidade.Text);
                con.Open();
                SqlCommand cmd = new SqlCommand("Atualizar", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", SqlDbType.Int).Value = Convert.ToInt32(txtId.Text.Trim());
                cmd.Parameters.AddWithValue("@valor", SqlDbType.Decimal).Value = Convert.ToDecimal(txtValor.Text.Trim());
                cmd.Parameters.AddWithValue("@quantidade", SqlDbType.Int).Value = Convert.ToInt32(txtQuantidade.Text.Trim());
                cmd.Parameters.AddWithValue("@data", SqlDbType.Date).Value = DateTime.Now.Date;
                cmd.Parameters.AddWithValue("@total", SqlDbType.Decimal).Value = total;
                cmd.ExecuteNonQuery();
                MessageBox.Show("Registro atualizado com sucesso!", "Atualizar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                CarregaDgv();
                txtId.Text = "";
                txtValor.Text = "";
                txtQuantidade.Text = "";
                con.Close();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void btnApagar_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Apagar", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", SqlDbType.Int).Value = Convert.ToInt32(txtId.Text.Trim());
                cmd.ExecuteNonQuery();
                MessageBox.Show("Registro apagado com sucesso!", "Apagar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                CarregaDgv();
                txtId.Text = "";
                txtValor.Text = "";
                txtQuantidade.Text = "";
                con.Close();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }
    }
}
