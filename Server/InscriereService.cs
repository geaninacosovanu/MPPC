


using MPPLab3.model;
using MPPLab3.repository;
using MPPLab3.validator;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MPPLab3.service
{
    public class InscriereService : IInscriereService
    {
        private IParticipantRepository participantRepository;
        private IProbaRepository probaRepository;
        private IInscriereRepository inscriereRepository;
        private IUserRepository userRepository;
        private readonly IDictionary<String, IInscriereObserver> loggedClients;
        public InscriereService(IParticipantRepository participantRepository, IProbaRepository probaRepository, IInscriereRepository inscriereRepository, IUserRepository userRepository)
        {
            this.participantRepository = participantRepository;
            this.probaRepository = probaRepository;
            this.inscriereRepository = inscriereRepository;
            this.userRepository = userRepository;
            loggedClients = new Dictionary<String, IInscriereObserver>();

        }


        public bool Login(string username, string parola,IInscriereObserver client)
        {
            MPPLab3.model.User user = new MPPLab3.model.User { Id = username, Parola = parola };
            if (userRepository.Exists(user))
            {
                loggedClients[user.Id] = client;
                return true;
            }
            return false;
        }

        public List<ParticipantProbeDTO> GetParticipanti(int idProba)
        {
            List<ParticipantProbeDTO> all = new List<ParticipantProbeDTO>();
            List<MPPLab3.model.Proba> probe;

            foreach (MPPLab3.model.Participant e in inscriereRepository.GetParticipantiPtProba(idProba))
            {
                probe = new List<MPPLab3.model.Proba>();
                foreach (MPPLab3.model.Proba p in inscriereRepository.GetProbePtParticipant(e.Id))
                    probe.Add(p);
                all.Add(new ParticipantProbeDTO { Participant = e, Probe = probe });
            }
            return all;

        }

        public List<ProbaDTO> GetAllProba()
        {
            List<ProbaDTO> all = new List<ProbaDTO>();
            foreach (MPPLab3.model.Proba p in probaRepository.FindAll())
                all.Add(new ProbaDTO { Proba = p, NrParticipanti = inscriereRepository.GetNrParticipantiProba(p.Id) });

            return all;
        }

      

        public MPPLab3.model.Participant GetParticipant(string nume, int varsta)
        {
            return participantRepository.GetParticipant(nume, varsta);
        }

       

        public void SaveInscriere(string nume, int varsta, List<MPPLab3.model.Proba> probe, bool existent)
        {
            MPPLab3.model.Participant p = null;
            if (existent == false)
            {
                int id;
                Random rand = new Random();
                do
                {
                    id = rand.Next(200) + 1;
                } while (participantRepository.FindOne(id.ToString()) != null);
                p = new MPPLab3.model.Participant { Id = id.ToString(), Nume = nume, Varsta = varsta };
                
                    participantRepository.Save(p);
                
                
            }
            else if (existent == true && GetParticipant(nume, varsta) != null)
                p = GetParticipant(nume, varsta);
            else if (existent == true && GetParticipant(nume, varsta) == null)
                throw new RepositoryException("Participantul nu exista!");
            NotifyInscriereAdded();

            foreach (MPPLab3.model.Proba pr in probe)
            {
                inscriereRepository.Save(new Inscriere { IdParticipant = p.Id, IdProba = pr.Id });

            }

        }

        private void NotifyInscriereAdded()
        {
            foreach (IInscriereObserver client in loggedClients.Values)
            {
                Task.Run(() => client.InscriereAdded());
            }
        }

        public void Logout(MPPLab3.model.User user, IInscriereObserver client)
        {
            IInscriereObserver localClient = loggedClients[user.Id];
            loggedClients.Remove(user.Id);
        }
    }
}
