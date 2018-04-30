using Lab3GUI;
using MPPLab3.model;
using MPPLab3.repository;
using MPPLab3.service;
using MPPLab3.utils;
using MPPLab3.validator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class StartServer
    {
        static void Main(string[] args)
        {
            IDictionary<string, string> properties = new SortedList<string, string>
            {
                { "ConnectionString", ConnectionStringUtils.GetConnectionStringByName("inscriereDB") }
            };
            IValidator<MPPLab3.model.User> userValidator = new UserValidator();
            IUserRepository userRepository = new UserDBRepository(properties, userValidator);
            IValidator<MPPLab3.model.Proba> probaValidator = new ProbaValidator();
            IProbaRepository probaRepository = new ProbaDBRepository(properties, probaValidator);
            IValidator<MPPLab3.model.Participant> participantValidator = new ParticipantValidator();
            IParticipantRepository participantRepository = new ParticipantDBRepository(properties, participantValidator);
            IValidator<Inscriere> inscriereValidator = new InscriereValidator();
            IInscriereRepository inscriereRepository = new InscriereDBRepository(properties, inscriereValidator);
            IInscriereService inscriereService = new InscriereService(participantRepository, probaRepository, inscriereRepository, userRepository);
            IInscriereService service= new InscriereService(participantRepository, probaRepository, inscriereRepository, userRepository);
            ProtoInscriereServer server = new ProtoInscriereServer("127.0.0.1", 55555, service);
           // Console.WriteLine(inscriereService.Login("u1", "parola1"));
            server.Start();
            Console.WriteLine("Server started ...");
          
            Console.ReadLine();
        }
    }
}
