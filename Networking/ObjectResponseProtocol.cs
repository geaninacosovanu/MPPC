using MPPLab3.model;
using MPPLab3.service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Networking
{
    public interface Response
    {
    }



    [Serializable]
    public class OkResponse : Response
    {

    }
    [Serializable]
    public class ErrorResponse : Response
    {
        private string message;

        public ErrorResponse(string message)
        {
            this.message = message;
        }

        public virtual string Message
        {
            get
            {
                return message;
            }
        }
    }
    [Serializable]
    public class GetAllProbaResponse : Response
    {
        private List<ProbaDTO> probe;

        public GetAllProbaResponse(List<ProbaDTO> probe)
        {
            this.probe = probe;
        }

        public virtual List<ProbaDTO> Probe
        {
            get
            {
                return probe;
            }
        }
    }

    [Serializable]
    public class GetAllParticipantiResponse : Response
    {
        private List<ParticipantProbeDTO> participanti;

        public GetAllParticipantiResponse(List<ParticipantProbeDTO> participanti)
        {
            this.participanti = participanti;
        }

        public virtual List<ParticipantProbeDTO> ParticipantiProbe
        {
            get
            {
                return participanti;
            }
        }
    }
    [Serializable]
    public class GetParticipantResponse : Response
    {
        private Participant participant;

        public GetParticipantResponse(Participant participant)
        {
            this.participant = participant;
        }


        public virtual Participant Participant
        {
            get
            {
                return participant;
            }
        }
    }

    public interface UpdateResponse:Response{

    }

    [Serializable]
    public class NewInscriereResponse : UpdateResponse
    {

    }
}
