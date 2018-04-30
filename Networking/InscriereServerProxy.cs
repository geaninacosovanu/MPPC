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
    public class InscriereServerProxy : IInscriereService
    {
        private string host;
        private int port;

        private IInscriereObserver client;

        private NetworkStream stream;

        private IFormatter formatter;
        private TcpClient connection;

        private Queue<Response> responses;
        private volatile bool finished;
        private EventWaitHandle _waitHandle;

        public InscriereServerProxy(string host, int port)
        {
            this.host = host;
            this.port = port;
            responses = new Queue<Response>();
        }

        public virtual List<ProbaDTO> GetAllProba()
        {
            SendRequest(new GetAllProbaRequest());
            Response response = ReadResponse();
            List<ProbaDTO> probe=new List<ProbaDTO>();
            if (response is ErrorResponse)
            {
                ErrorResponse err = (ErrorResponse)response;
                throw new InscriereServiceException(err.Message);
            }
            else if (response is GetAllProbaResponse) {
                probe = ((GetAllProbaResponse)response).Probe;
            }
            return probe;
        }

        public virtual Participant GetParticipant(string nume, int varsta)
        {
            SendRequest(new GetParticipantRequest(new ParticipantDTO { Nume = nume, Varsta = varsta }));
            Response response = ReadResponse();
            Participant part=null;
            if (response is ErrorResponse)
            {
                ErrorResponse err = (ErrorResponse)response;
                throw new InscriereServiceException(err.Message);
            }
            else if (response is GetParticipantResponse)
            {
                part = ((GetParticipantResponse)response).Participant;
            }
            return part;

        }

        public virtual List<ParticipantProbeDTO> GetParticipanti(int idProba)
        {
            SendRequest(new GetAllParticipantiRequest(idProba));
            Response response = ReadResponse();
            List<ParticipantProbeDTO> part = new List<ParticipantProbeDTO>();
            if (response is ErrorResponse)
            {
                ErrorResponse err = (ErrorResponse)response;
                throw new InscriereServiceException(err.Message);
            }
            else if (response is GetAllParticipantiResponse)
            {
                part = ((GetAllParticipantiResponse)response).ParticipantiProbe;
            }
            return part;
        }

        public virtual bool Login(string username, string parola,IInscriereObserver client)
        {
            InitializeConnection();
            bool exists=false;
            User user = new User { Id = username, Parola = parola };
            SendRequest(new LoginRequest(user));
            Response response = ReadResponse();
            if (response is OkResponse)
            {
                this.client = client;
                exists= true;
            }
            else if (response is ErrorResponse err)
            {
                CloseConnection();
                exists=false;
            }
            return exists;
        }

        public virtual void Logout(User user, IInscriereObserver client)
        {
            
            SendRequest(new LogoutRequest(user));
            Response response = ReadResponse();
            CloseConnection();
            if (response is ErrorResponse)
            {
                ErrorResponse err = (ErrorResponse)response;
                throw new InscriereServiceException(err.Message);
            }
        }

        public virtual void SaveInscriere(string nume, int varsta, List<Proba> probe, bool existent)
        {
            SendRequest(new AddInscriereRequest(new InscriereDTO { Varsta = varsta, Nume = nume, Probe = probe, Existent = existent }));
            Response response = ReadResponse();
            if (response is ErrorResponse)
            {
                ErrorResponse err = (ErrorResponse)response;
                throw new InscriereServiceException(err.Message);
            }
        }



        private void CloseConnection()
        {
            finished = true;
            try
            {
                stream.Close();
                //output.close();
                connection.Close();
                _waitHandle.Close();
                client = null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }

        }

        private void SendRequest(Request request)
        {
            try
            {
                formatter.Serialize(stream, request);
                stream.Flush();
            }
            catch (Exception e)
            {
                throw new InscriereServiceException("Error sending object " + e);
            }

        }

        private Response ReadResponse()
        {
            Response response = null;
            try
            {
                _waitHandle.WaitOne();
                lock (responses)
                {
                    //Monitor.Wait(responses); 
                    response = responses.Dequeue();

                }


            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
            return response;
        }
        private void InitializeConnection()
        {
            try
            {
                connection = new TcpClient(host, port);
                stream = connection.GetStream();
                formatter = new BinaryFormatter();
                finished = false;
                _waitHandle = new AutoResetEvent(false);
                StartReader();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }
        private void StartReader()
        {
            Thread tw = new Thread(Run);
            tw.Start();
        }


        private void HandleUpdate(UpdateResponse update){
            if (update is NewInscriereResponse) {
                Console.WriteLine("NewInscriereResponse...");
                try
                {
                    client.InscriereAdded();
                }
                catch(InscriereServiceException e)
                {
                    Console.WriteLine(e.StackTrace);
                }
                }
            }
        
        public virtual void Run()
        {
            while (!finished)
            {
                try
                {
                    object response = formatter.Deserialize(stream);
                    Console.WriteLine("response received " + response);
                    if (response is UpdateResponse)
                    {
                        HandleUpdate((UpdateResponse)response);
                    }
                    else
                    {
                        lock (responses)
                        {
                            responses.Enqueue((Response)response);

                        }
                        _waitHandle.Set();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Reading error " + e);
                }

            }
    
    }
}
}
