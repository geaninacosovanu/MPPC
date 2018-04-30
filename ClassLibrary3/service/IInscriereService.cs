using MPPLab3.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPPLab3.service
{
    public interface IInscriereService
    {
        bool Login(string username, string parola,IInscriereObserver client);

        void Logout(User user, IInscriereObserver client);
        List<ProbaDTO> GetAllProba();

        List<ParticipantProbeDTO> GetParticipanti(int idProba);
      
        void SaveInscriere(string nume, int varsta, List<Proba> probe, bool existent);

        Participant GetParticipant(string nume, int varsta);
    }
}
