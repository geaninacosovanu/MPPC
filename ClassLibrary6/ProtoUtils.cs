using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPPLab3.model;
using MPPLab3.service;

namespace ClassLibrary6
{
    public static class ProtoUtils
    {
        public static String GetError(InscriereResponse response)
        {
            String errorMessage = response.Error;
            return errorMessage;
        }

        public static MPPLab3.model.User GetUser(InscriereRequest request)
        {
            MPPLab3.model.User user = new MPPLab3.model.User { Id = request.User.Username, Parola = request.User.Password };
            return user;
        }

        public static InscriereResponse CreateOkResponse()
        {
            InscriereResponse response = new InscriereResponse { Type = InscriereResponse.Types.Type.Ok };
            return response;
        }

        public static InscriereResponse CreateErrorResponse(string message)
        {
            InscriereResponse response = new InscriereResponse
            {
                Type = InscriereResponse.Types.Type.Error,
                Error = message
            };
            return response;
        }

        public static InscriereResponse CreateNewInscriereResponse()
        {
            InscriereResponse response = new InscriereResponse
            {
                Type = InscriereResponse.Types.Type.NewInscriere
            };
            return response;
        }

        public static InscriereResponse CreateGetAllProbeResponse(List<MPPLab3.service.ProbaDTO> probe) {
            InscriereResponse response = new InscriereResponse
            {
                Type = InscriereResponse.Types.Type.GetAllProbeDto
            };
            foreach (MPPLab3.service.ProbaDTO p in probe)
            {
                ProbaDTO dto = new ProbaDTO {Proba=new Proba { Id = p.IdProba, Nume = p.NumeProba, Distanta = p.DistantaProba },NrParticipanti=p.NrParticipanti };
                response.ProbeDto.Add(dto);
            }

            return response;

        }

        public static InscriereResponse CreateGetAllParticipantiResponse(List<MPPLab3.service.ParticipantProbeDTO> part)
        {
            InscriereResponse response = new InscriereResponse
            {
                Type = InscriereResponse.Types.Type.GetParticipantiDto
            };
            foreach (MPPLab3.service.ParticipantProbeDTO p in part)
            {
                List<Proba> probe = new List<Proba>();
                foreach (MPPLab3.model.Proba pr in p.Probe)
                    probe.Add(new Proba { Id = pr.Id, Nume = pr.Nume, Distanta = pr.Distanta });

                ParticipantProbeDTO dto = new ParticipantProbeDTO { Participant = new Participant { Id = p.ParticipantId, Nume = p.ParticipantNume, Varsta = p.ParticipantVarsta } };
                dto.Probe.AddRange(probe);
                response.ParticipantProbeDto.Add(dto);
            }

            return response;
        }

        public static InscriereResponse GetParticipantResponse(MPPLab3.model.Participant p)
        {
            InscriereResponse response = new InscriereResponse
            {
                Type = InscriereResponse.Types.Type.GetParticipant
            };
            response.Participant = new Participant { Id = p.Id, Nume = p.Nume, Varsta = p.Varsta };
            return response;
        }
    }
}
