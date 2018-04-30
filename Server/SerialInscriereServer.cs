using MPPLab3.service;
using Networking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lab3GUI
{
    public class SerialInscriereServer:ConcurrentServer
    {
        private IInscriereService service;
        private InscriereClientWorker worker;
        public SerialInscriereServer(string host, int port, IInscriereService service) : base(host, port)
        {
            this.service = service;
            Console.WriteLine("SerialChatServer...");
        }
        protected override Thread CreateWorker(TcpClient client)
        {
            worker = new InscriereClientWorker(service, client);
            return new Thread(new ThreadStart(worker.Run));
        }
    }
}
