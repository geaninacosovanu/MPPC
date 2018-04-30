using MPPLab3.model;
using MPPLab3.service;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Networking
{
    public class InscriereClientWorker:IInscriereObserver
    {
        private IInscriereService service;
        private TcpClient connection;
        private NetworkStream stream;
        private IFormatter formatter;
        private volatile bool connected;
        public InscriereClientWorker(IInscriereService service, TcpClient client)
        {
            this.service = service;
            this.connection = client;
            try
            {
                stream = connection.GetStream();
                formatter = new BinaryFormatter();
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
                SendResponse(new NewInscriereResponse());
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
                    object request = formatter.Deserialize(stream);
                    object response = HandleRequest((Request)request);
                    if (response != null)
                    {
                        SendResponse((Response)response);
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

        private void SendResponse(Response response)
        {
            Console.WriteLine("sending response " + response);
            formatter.Serialize(stream, response);
            stream.Flush();

        }

        private Response HandleRequest(Request request)
        {
            Response response = null;
            if (request is LoginRequest)
            {
                Console.WriteLine("Login request ...");
                LoginRequest logReq = (LoginRequest)request;
                User user = logReq.User;
                try
                {
                    bool ok=false;
                    lock (service)
                    {
                        ok=service.Login(user.Id,user.Parola,this);
                    }
                    if(ok)
                        return new OkResponse();
                    else
                        return new ErrorResponse("User inexistent");
                
                }
                catch (InscriereServiceException e)
                {
                    connected = false;
                    return new ErrorResponse(e.Message);
                }
            }
            if (request is GetAllProbaRequest)
            {
                Console.WriteLine("GetAllProba request ...");
                List<ProbaDTO> probe = new List<ProbaDTO>();
                try
                {
                    lock (service)
                    {
                        probe = service.GetAllProba();
                    }
                    return new GetAllProbaResponse(probe);
                }
                catch (InscriereServiceException e)
                {
                    return new ErrorResponse(e.Message);
                }
            }
            if (request is GetAllParticipantiRequest)
            {
                Console.WriteLine("GetAllParticipanti request ...");
                List<ParticipantProbeDTO> part = new List<ParticipantProbeDTO>();
                try
                {
                    lock (service)
                    {
                        part = service.GetParticipanti(((GetAllParticipantiRequest)request).IdProba);
                    }
                    return new GetAllParticipantiResponse(part);
                }
                catch (InscriereServiceException e)
                {
                    return new ErrorResponse(e.Message);
                }
            }
            if (request is LogoutRequest)
            {
                Console.WriteLine("Logout request");
                LogoutRequest logReq = (LogoutRequest)request;
                User user = logReq.User;
                try
                {
                    lock (service)
                    {

                        service.Logout(user, this);
                    }
                    connected = false;
                    return new OkResponse();

                }
                catch (InscriereServiceException e)
                {
                    return new ErrorResponse(e.Message);
                }
            }
            if (request is GetParticipantRequest)
            {
                Console.WriteLine("GetParticipant request ...");
                GetParticipantRequest req = (GetParticipantRequest)request;
                ParticipantDTO participant = req.ParticipantDTO;
                try
                {
                    Participant p=null;
                    lock (service)
                    {
                        p = service.GetParticipant(participant.Nume,participant.Varsta);
                    }
                    
                    return new GetParticipantResponse(p);
                   

                }
                catch (InscriereServiceException e)
                {
                    return new ErrorResponse(e.Message);
                }
            }
            if (request is AddInscriereRequest)
            {
                Console.WriteLine("AddInscriere request");
                AddInscriereRequest req = (AddInscriereRequest)request;
                InscriereDTO inscriere = req.InscriereDTO;
                try
                {
                    lock (service)
                    {

                        service.SaveInscriere(inscriere.Nume, inscriere.Varsta, inscriere.Probe, inscriere.Existent);
                    }
                    return new OkResponse();

                }
                catch (InscriereServiceException e)
                {
                    return new ErrorResponse(e.Message);
                }
                catch (MPPLab3.validator.ValidationException e) {
                    return new ErrorResponse(e.Message);
                }
            }
            return response;
        }
    }
}
