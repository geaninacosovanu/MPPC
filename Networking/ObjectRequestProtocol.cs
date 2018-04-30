using MPPLab3.model;
using MPPLab3.service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Networking
{
    
    public interface Request
    {
    }
    [Serializable]
    public class LoginRequest : Request
    {
        private User user;

        public LoginRequest(User user)
        {
            this.user = user;
        }

        public virtual User User
        {
            get
            {
                return user;
            }
        }
    }

    [Serializable]
    public class LogoutRequest : Request
    {
        private User user;

        public LogoutRequest(User user)
        {
            this.user = user;
        }

        public virtual User User
        {
            get
            {
                return user;
            }
        }
    }
    [Serializable]
    public class GetAllProbaRequest : Request
    {
        public GetAllProbaRequest()
        {
            
        }
     
    }

    [Serializable]
    public class GetAllParticipantiRequest : Request
    {
        private int idProba;
        public GetAllParticipantiRequest(int idProba)
        {
            this.idProba = idProba;
        }
        public virtual int IdProba { get { return idProba; } }

    }
    [Serializable]
    public class GetParticipantRequest : Request
    {
        private ParticipantDTO participant;

        public GetParticipantRequest(ParticipantDTO participant)
        {
            this.participant = participant;
        }

        public virtual ParticipantDTO ParticipantDTO { get { return participant; } }
      

    }
    [Serializable]
    public class AddInscriereRequest : Request
    {
        private InscriereDTO inscriere;

        public AddInscriereRequest(InscriereDTO inscriere)
        {
            this.inscriere = inscriere;
        }

        public virtual InscriereDTO InscriereDTO{ get { return inscriere; } }
       

    }
}
