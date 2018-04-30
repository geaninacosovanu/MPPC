
using MPPLab3.model;
using MPPLab3.repository;
using MPPLab3.service;
using MPPLab3.utils;
using MPPLab3.validator;
using Networking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab3GUI
{
    class StartClient
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            IInscriereService service = new InscriereServerProxy("127.0.0.1", 55555);
            InscriereController controller = new InscriereController(service);
            Login login = new Login(controller);
            try
            {
                Application.Run(login);
            }
            catch(InscriereServiceException e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
