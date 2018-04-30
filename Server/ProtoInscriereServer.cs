using ClassLibrary6;
using MPPLab3.service;
using Networking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    public class ProtoInscriereServer : ConcurrentServer
    {
        private IInscriereService service;
        private ProtoInscriereWorker worker;
        public ProtoInscriereServer(string host, int port, IInscriereService service)
            : base(host, port)
        {
            this.service=service;
            Console.WriteLine("ProtoInscriereServer...");
        }
        protected override Thread CreateWorker(TcpClient client)
        {
            worker = new ProtoInscriereWorker(service, client);
            return new Thread(new ThreadStart(worker.Run));
        }
    }

}
