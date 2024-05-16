using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using BCrypt.Net;
using static BCrypt.Net.BCrypt;
using Org.BouncyCastle.Crypto.Generators;
using System.Security.Cryptography;

namespace AppBooklyMiniTCC
{
    public partial class addUser : Form
    {
        private MySqlConnection objCnx = new MySqlConnection();
        private MySqlCommand objCmd = new MySqlCommand();
        private MySqlDataReader objDados;
        string hashedText = string.Empty;
        public addUser()
        {
            InitializeComponent();
        }

        private void addUser_Load(object sender, EventArgs e)
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

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            hashedText = BCrypt.Net.BCrypt.HashPassword(txtSenha.Text);
            MessageBox.Show("A hash da sua senha é: " + hashedText);
            
            try
            {
                string strSql = "SELECT * FROM leitor WHERE matricula_leitor = " + txtMatricula.Text;
                objCmd.Connection = objCnx;
                objCmd.CommandText = strSql;
                objDados = objCmd.ExecuteReader();
                if (objDados.HasRows)
                {
                    MessageBox.Show("Registro já existente!", "ADO.NET", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    if (!objDados.IsClosed) { objDados.Close(); }
                    DateTime dataNascLeitor = Convert.ToDateTime(dtNascUser.Text);
                    strSql = "INSERT INTO leitor (matricula_leitor, nome_leitor, tel_leitor, email_leitor, endereco_leitor, senha_leitor, dataNasc_leitor) VALUES (";
                    strSql += "'" + txtMatricula.Text + "',";
                    strSql += "'" + txtNome.Text + "',";
                    strSql += "'" + txtTel.Text + "',";
                    strSql += "'" + txtEmail.Text + "',";
                    strSql += "'" + txtEndereco.Text + "',";
                    strSql += "'" + BCrypt.Net.BCrypt.HashPassword(txtSenha.Text) + "',";
                    strSql += "'" + dataNascLeitor.ToString("yyyy-MM-dd") + "')";
                    objCmd.Connection = objCnx;
                    objCmd.CommandText = strSql;
                    objCmd.ExecuteNonQuery();
                    MessageBox.Show("Registro inserido com sucesso!", "ADO.NET", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            catch (Exception Erro)
            {
                MessageBox.Show("Erro ==> " + Erro.Message, "ADO.NET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            addUserAvisos voltarParaAvisos = new addUserAvisos();
            voltarParaAvisos.Show();
        }

        private void btnMostrarSenha_Click(object sender, EventArgs e)
        {
           if (txtSenha.UseSystemPasswordChar == true)
            {
                txtSenha.UseSystemPasswordChar = false;
            }

           else if (txtSenha.UseSystemPasswordChar == false)
            {
                txtSenha.UseSystemPasswordChar = true;
            }
        }

        private void txtSenha_TextChanged(object sender, EventArgs e)
        {
            txtSenha.UseSystemPasswordChar = true;
        }
    }
}
