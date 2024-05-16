//To login the user will need their hashed password.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace AppBooklyMiniTCC
{
    public partial class userLog : Form
    {
        private MySqlConnection objCnx = new MySqlConnection();
        private MySqlCommand objCmd = new MySqlCommand();
        private MySqlDataReader objDados;
        string hashedText = string.Empty;
        public userLog()
        {
            InitializeComponent();
        }

        private void userLog_Load(object sender, EventArgs e)
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

        private void btnEsqueci_Click(object sender, EventArgs e)
        {
            try
            {
                string strSql = "SELECT * FROM leitor WHERE matricula_leitor = " + txtMatricula.Text;
                objCmd.Connection = objCnx;
                objCmd.CommandText = strSql;
                objDados = objCmd.ExecuteReader();

                if (!objDados.HasRows) // ! significa negação
                {
                    MessageBox.Show(
                        "Leitor não existe ou não cadastrado",
                        "SISTEMA BOOKLY",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                        );
                }
                else
                {
                    objDados.Read();
                    txtSenha.Text = objDados["senha_leitor"].ToString();
                }
                if (!objDados.IsClosed) { objDados.Close(); }
            }
            catch (Exception Erro)
            {
                MessageBox.Show(
                    "Erro ===> " + Erro.Message, "SISTEMA BOOKLY",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                    );
            }
        }

        private void btnMudarSenha_Click(object sender, EventArgs e)
        {
            try
            {
                string strSql = "SELECT * FROM leitor WHERE matricula_leitor = " + txtMatricula.Text;
                objCmd.Connection = objCnx;
                objCmd.CommandText = strSql;
                objDados = objCmd.ExecuteReader();
                if (!objDados.HasRows)
                {
                    MessageBox.Show("Matrícula inexistente!", "ADO.NET", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtMatricula.Focus();
                    if (!objDados.IsClosed) { objDados.Close(); }
                }
                else
                {
                    if (!objDados.IsClosed) { objDados.Close(); }
                    strSql = "UPDATE leitor SET ";    
                    strSql += "senhaLeitor = '" + txtSenha.Text + "' ";
                    strSql += "WHERE matriculaLeitor = '" + txtMatricula.Text + "'";
                    objCmd.Connection = objCnx;
                    objCmd.CommandText = strSql;
                    objCmd.ExecuteNonQuery();
                    MessageBox.Show("Senha atualizado com sucesso!", "ADO.NET", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                if (!objDados.IsClosed) { objDados.Close(); }
            }
            catch (Exception Erro)
            {
                MessageBox.Show("Erro ==> " + Erro.Message, "ADO.NET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAcessar_Click(object sender, EventArgs e)
        {
            try
            {
                string strSql = "SELECT * FROM leitor WHERE matricula_leitor = '" + txtMatricula.Text + "' AND senha_leitor = '" + txtSenha.Text + "'";
                objCmd.Connection = objCnx;
                objCmd.CommandText = strSql;
                objDados = objCmd.ExecuteReader();
                hashedText = "leitor.senha_leitor";
                BCrypt.Net.BCrypt.Verify(txtSenha.Text, hashedText);
                if (objDados.HasRows)
                {
                    userExplorer acessoBookly = new userExplorer();
                    this.Hide();
                    acessoBookly.Show();
                }
                else
                {
                    MessageBox.Show("Usuário e/ou senha não existe(m) ou está(ão) incorreto(s).", "ADO.NET", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtMatricula.Focus();
                    txtMatricula.Text = "";
                    txtSenha.Text = "";
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
