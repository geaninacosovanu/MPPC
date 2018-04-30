using MPPLab3.service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using Google.Protobuf;
using System.Threading.Tasks;
using System.Threading;
using Networking;

namespace ClassLibrary6
{
    public class ProtoInscriereWorker : IInscriereObserver
    {
        private IInscriereService service;
        private TcpClient connection;
        private NetworkStream stream;

        private volatile bool connected;
        public ProtoInscriereWorker(IInscriereService service, TcpClient client)
        {
            this.service = service;
            this.connection = client;
            try
            {
                stream = connection.GetStream();
                connected = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        public void InscriereAdded()
        {
            try
            {
                SendResponse(ProtoUtils.CreateNewInscriereResponse());
            }
            catch (Exception e)
            {
                throw new InscriereServiceException("Sending error: " + e);
            }
        }

        public void Run()
        {
            while (connected)
            {
                try
                {
                    InscriereRequest request = InscriereRequest.Parser.ParseDelimitedFrom(stream);
                    InscriereResponse response = HandleRequest(request);
                    if (response != null)
                    {
                        SendResponse(response);
                    }
                }
                catch (Exception e)
                {

                    Console.WriteLine(e.StackTrace);
                    //throw new InscriereServiceException(e.Message);
                }

                try
                {
                    Thread.Sleep(1000);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }
            }
            try
            {
                stream.Close();
                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error " + e);
            }
        }

        private void SendResponse(InscriereResponse response)
        {
            //Console.WriteLine("sending response " + response);
            lock (stream)
            {
                response.WriteDelimitedTo(stream);
                stream.Flush();
            }

        }

        private InscriereResponse HandleRequest(InscriereRequest request)
        {
            InscriereResponse response = null;
            InscriereRequest.Types.Type reqType = request.Type;
            if (reqType.Equals(InscriereRequest.Types.Type.Login))
            {
                Console.WriteLine("Login request ...");

                MPPLab3.model.User user = ProtoUtils.GetUser(request);
                try
                {
                    bool ok = false;
                    lock (service)
                    {
                        ok = service.Login(user.Id, user.Parola, this);
                    }
                    return ProtoUtils.CreateOkResponse();
                }
                catch (InscriereServiceException e)
                {
                    connected = false;
                    return ProtoUtils.CreateErrorResponse(e.Message);
                }
             
            }
            if (reqType.Equals(InscriereRequest.Types.Type.GetAllProbeDto))
            {
                Console.WriteLine("GetAllProba request ...");
                List<MPPLab3.service.ProbaDTO> probe = new List<MPPLab3.service.ProbaDTO>();
                try
                {
                    lock (service)
                    {
                        probe = service.GetAllProba();
                    }
                    return ProtoUtils.CreateGetAllProbeResponse(probe);
                }
                catch (InscriereServiceException e)
                {
                    return ProtoUtils.CreateErrorResponse(e.Message);
                }
            }
            if (reqType.Equals(InscriereRequest.Types.Type.GetParticipantiDto))
            {
                Console.WriteLine("GetAllParticipanti request ...");
                List<MPPLab3.service.ParticipantProbeDTO> part = new List<MPPLab3.service.ParticipantProbeDTO>();
                try
                {
                    lock (service)
                    {
                        part = service.GetParticipanti(request.IdProba);
                    }
                    return ProtoUtils.CreateGetAllParticipantiResponse(part);
                }
                catch (InscriereServiceException e)
                {
                    return ProtoUtils.CreateErrorResponse(e.Message);
                }
            }
            if (reqType.Equals(InscriereRequest.Types.Type.Logout))
            {
                Console.WriteLine("Logout request");
                MPPLab3.model.User user = new MPPLab3.model.User { Id=request.User.Username,Parola=request.User.Password };
                try
                {
                    lock (service)
                    {

                        service.Logout(user, this);
                    }
                    connected = false;
                    return ProtoUtils.CreateOkResponse();

                }
                catch (InscriereServiceException e)
                {
                    return ProtoUtils.CreateErrorResponse(e.Message);
                }
            }
            if (reqType.Equals(InscriereRequest.Types.Type.GetParticipant))
            {
                Console.WriteLine("GetParticipant request ...");
           
                ParticipantDTO participant = new ParticipantDTO { Nume = request.Participant.Nume, Varsta = request.Participant.Varsta };
                try
                {
                    MPPLab3.model.Participant p = null;
                    lock (service)
                    {
                        p = service.GetParticipant(participant.Nume, participant.Varsta);
                    }

                    return ProtoUtils.GetParticipantResponse(p);


                }
                catch (InscriereServiceException e)
                {
                    return ProtoUtils.CreateErrorResponse(e.Message);
                }
            }
            if (reqType.Equals(InscriereRequest.Types.Type.AddInscriere))
            {
                Console.WriteLine("AddInscriere request");
                List<MPPLab3.model.Proba> probe = new List<MPPLab3.model.Proba>();
                foreach (Proba p in request.Inscriere.Probe)
                    probe.Add(new MPPLab3.model.Proba { Id = p.Id, Nume = p.Nume, Distanta = p.Distanta });
               Networking.InscriereDTO inscriere = new Networking.InscriereDTO { Existent = request.Inscriere.Existent, Nume = request.Inscriere.Nume, Varsta = request.Inscriere.Varsta, Probe = probe };
                try
                {
                    lock (service)
                    {

                        service.SaveInscriere(inscriere.Nume, inscriere.Varsta, inscriere.Probe, inscriere.Existent);
                    }
                    return ProtoUtils.CreateOkResponse();

                }
                catch (InscriereServiceException e)
                {
                    return ProtoUtils.CreateErrorResponse(e.Message);
                }
                catch (MPPLab3.validator.ValidationException e)
                {
                    return ProtoUtils.CreateErrorResponse(e.Message);
                }
            }
            return response;
        }
    }
}

