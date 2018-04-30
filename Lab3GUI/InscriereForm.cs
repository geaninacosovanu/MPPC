
using MPPLab3.model;
using MPPLab3.service;
using MPPLab3.validator;
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
    public partial class InscriereForm : Form
    {

        private InscriereController controller;
        

        public InscriereForm(InscriereController controller)
        {
            InitializeComponent();
            this.controller = controller;
            PopulateGridViewProbe();
            PopulateGridViewProbeInscriere();
            controller.UpdateEvent += UserUpdate;

        }

        private void UserUpdate(object sender, InscriereUserEventArgs e)
        {
            if (e.UserEventType == InscriereUserEvent.NewInscriere)
            {
                //List<ProbaDTO> probeDTO = controller.GetAllProba();
                Console.WriteLine("[InscriereForm] Inscriere added ");
                dataGridProbe.BeginInvoke(new UpdateDataGridViewProbeCallback(this.UpdateGridViewProbe), new Object[] { dataGridProbe });
            }
        }

        private void PopulateGridViewProbeInscriere()
        {
            dataGridViewProbeInscriere.DataSource = controller.GetAllProba();
            dataGridViewProbeInscriere.Columns["IdProba"].Visible = false;
            dataGridViewProbeInscriere.Columns["Proba"].Visible = false;
            dataGridViewProbeInscriere.Columns["NrParticipanti"].Visible = false;
        }

        private void PopulateGridViewProbe()
        {
            List<ProbaDTO> all = controller.GetAllProba();
            
            dataGridProbe.DataSource = all;
            dataGridProbe.Columns["NrParticipanti"].DisplayIndex = 4;
            dataGridProbe.Columns["IdProba"].Visible = false;
            dataGridProbe.Columns["Proba"].Visible = false;
        

        }

        public delegate void UpdateDataGridViewProbeCallback(DataGridView dataGridViewProbe);
        public void UpdateGridViewProbe(DataGridView dataGridViewProbe)
        {
            dataGridViewProbe.DataSource = null;
            List<ProbaDTO> all = controller.GetAllProba();
            PopulateGridViewProbe();
        }

        private void buttonInscriere_Click(object sender, EventArgs e)
        {
            string nume=null;
            int varsta=0;
            List<Proba> probe = new List<Proba>();

            try
            {
                nume = textBoxNume.Text;
                varsta = int.Parse(textBoxVarsta.Text);
                for (int i = 0; i < dataGridViewProbeInscriere.SelectedRows.Count; i++)
                {
                    int id = int.Parse(dataGridViewProbeInscriere.SelectedRows[i].Cells["IdProba"].Value.ToString());
                    string n = dataGridViewProbeInscriere.SelectedRows[i].Cells["NumeProba"].Value.ToString();
                    float dist = float.Parse(dataGridViewProbeInscriere.SelectedRows[i].Cells["DistantaProba"].Value.ToString());
                    probe.Add(new Proba { Id = id, Nume = n, Distanta = dist });
                }
            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            try
            {
                controller.SaveInscriere(nume, varsta, probe, checkBoxParticipantExistent.Checked);
               
                string pr = "";
                probe.ForEach(p => pr = pr + p.ToString() + "\n");
                MessageBox.Show("Participantul a fost inscris la probele: \n" + pr);
                dataGridViewProbeInscriere.ClearSelection();
                textBoxNume.Clear();
                textBoxVarsta.Clear();
                checkBoxParticipantExistent.Checked = false;
            }
            catch(RepositoryException err)
            {
                MessageBox.Show(err.Message);
            }
            catch (ValidationException err)
            {
                MessageBox.Show(err.Message);
            }
            catch (InscriereServiceException err)
            {
                MessageBox.Show(err.Message);
            }

        }

        private void buttonLogout_Click(object sender, EventArgs e)
        {
            controller.Logout();
            controller.UpdateEvent -= UserUpdate;
            Login login = new Login (this.controller);
            this.Visible = false;
            login.ShowDialog();
        }

        private void dataGridProbe_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int idProba = int.Parse(dataGridProbe.SelectedRows[0].Cells["IdProba"].Value.ToString());
            List<ParticipantProbeDTO> all = controller.GetParticipanti(idProba);
            dataGridParticipanti.DataSource = all;
            dataGridParticipanti.Columns["ParticipantId"].Visible = false;
            dataGridParticipanti.Columns["Participant"].Visible = false;
            dataGridParticipanti.Columns["ProbeString"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            


        }


        private void InscriereForm_Load(object sender, EventArgs e)
        {
            
        }
    }
}
