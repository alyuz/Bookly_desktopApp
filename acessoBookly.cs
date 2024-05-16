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
    public partial class acessoBookly : Form
    {
        public acessoBookly()
        {
            InitializeComponent();
        }


        private void btnAcessar_MouseClick(object sender, MouseEventArgs e)
        {
            userLog acessarContaDeUsuario = new userLog();
            acessarContaDeUsuario.Show();
        }

        private void btnRegistro_MouseClick(object sender, MouseEventArgs e)
        {
            addUserAvisos adicionarContaDeUsuarioAvisos = new addUserAvisos();
            adicionarContaDeUsuarioAvisos.Show();
        }

        private void btnConvidado_MouseClick(object sender, MouseEventArgs e)
        {
            userExplorer UsuarioExplorador = new userExplorer();
            UsuarioExplorador.Show();
        }

        private void btnAdm_CheckedChanged(object sender, EventArgs e)
        {
            acessoAdm acessoAdministrador = new acessoAdm();
            acessoAdministrador.Show();
        }

        private void acessoBookly_Load(object sender, EventArgs e)
        {

        }
    }
}
