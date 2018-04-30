using MPPLab3.model;
using MPPLab3.service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab3GUI
{
    public class InscriereController:IInscriereObserver
    {
        public event EventHandler<InscriereUserEventArgs> UpdateEvent;
        private readonly IInscriereService service;
        private User currentUser;
        public InscriereController(IInscriereService service)
        {
            this.service = service;
            currentUser = null;
        }

        public bool Login(string user, string parola)
        {
            bool exist = service.Login(user, parola,this);
            if (exist)
            {
                currentUser = new User { Id = user, Parola = parola };
            }
            return exist;
        }
        public List<ProbaDTO> GetAllProba()
        {
            return service.GetAllProba();
        }
        public void Logout() {
            Console.WriteLine("Ctrl logout");
            service.Logout(currentUser, this);
            currentUser = null;
        }

        public List<ParticipantProbeDTO> GetParticipanti(int idProba) { return service.GetParticipanti(idProba); }
        public void SaveInscriere(string nume, int varsta, List<Proba> probe, bool existent) { service.SaveInscriere(nume, varsta, probe, existent); }
        public Participant GetParticipant(String nume, int varsta) { return null; }

    
        public void InscriereAdded()
        {
            InscriereUserEventArgs userArgs = new InscriereUserEventArgs(InscriereUserEvent.NewInscriere,null);
            Console.WriteLine("NotificareInscriere");
            OnUserEvent(userArgs);
        }

        private void OnUserEvent(InscriereUserEventArgs userArgs)
        {
            if (UpdateEvent == null) return;
            UpdateEvent(this, userArgs);
            Console.WriteLine("Update Event called");
        }
    }
}
