using MPPLab3.service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab3GUI
{
    public partial class Login : Form
    {
        private InscriereController controller;
        public Login(InscriereController ctrl)
        {
            
            InitializeComponent();
            this.controller = ctrl;
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            string user = textBoxUsername.Text;
            string parola = textBoxParola.Text;

            try
            {
                if (controller.Login(user, parola))
                {
                    InscriereForm inscriereForm = new InscriereForm(this.controller);
                    //InscriereService.AddObserver(inscriereForm);

                    this.Visible = false;
                    inscriereForm.ShowDialog();

                }

                else
                {
                    MessageBox.Show("Username sau parola incorecte!");
                }
            }
            catch(InscriereServiceException )
            {
                MessageBox.Show("Eroare la conectarea la server!");
            }

        }

     
    }
}
