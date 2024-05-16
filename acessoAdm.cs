using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppBooklyMiniTCC
{
    public partial class acessoAdm : Form
    {
        private MySqlConnection objCnx = new MySqlConnection();
        private MySqlCommand objCmd = new MySqlCommand();
        private MySqlDataReader objDados;
        public acessoAdm()
        {
            InitializeComponent();
        }

        private void acessoAdm_Load(object sender, EventArgs e)
        {
            try
            {
                objCnx.ConnectionString = "Server="server_name";Database="database_name";User="user_name";Pwd="the_password";
                objCnx.Open();
            }
            catch (Exception Erro)
            {
                MessageBox.Show("Erro ===> " + Erro.Message, "ADO.NET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnOut_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAcessoAdm_Click(object sender, EventArgs e)
        {
            try
            {
                string strSql = "SELECT * FROM adm WHERE cod_adm = '" + txtCodAdm.Text + "' AND senha_adm = '" + txtSenhaAdm.Text + "' AND cod_biblio = '" + txtCodBiblio.Text + "'";
                objCmd.Connection = objCnx;
                objCmd.CommandText = strSql;
                objDados = objCmd.ExecuteReader();
                if (objDados.HasRows)
                {
                    servidorAdm entrarAdm = new servidorAdm();
                    this.Hide();
                    entrarAdm.Show();
                }
                else
                {
                    MessageBox.Show("Usuário não existe ou informações incorretas.", "ADO.NET", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCodAdm.Focus();
                    txtCodAdm.Text = "";
                    txtSenhaAdm.Text = "";
                    if (!objDados.IsClosed) { objDados.Close(); }
                }
            }
            catch (Exception Erro)
            {
                MessageBox.Show("Erro ==> " + Erro.Message, "ADO.NET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            objCnx.Close();
        }
    }
    }
