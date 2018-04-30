using MPPLab3.model;
using System;

namespace MPPLab3.service
{
    [Serializable]
    public class ProbaDTO
    {
        public Proba Proba { get; set; }
        public int NrParticipanti { get; set; }
        public int IdProba { get { return Proba.Id; } }
        public string NumeProba { get { return Proba.Nume; } }

        public float DistantaProba { get { return Proba.Distanta; } }
    }
    
}