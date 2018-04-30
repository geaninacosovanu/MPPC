using MPPLab3.model;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace MPPLab3.service
{
    [Serializable]
    public class ParticipantProbeDTO
    {
        public Participant Participant { get; set; }
        public List<Proba> Probe { get; set; }
        public string ParticipantId
        {
           get { return Participant.Id; }
        }
        public string ParticipantNume
        {
            get { return Participant.Nume; }
        }

        public int ParticipantVarsta
        {
            get { return Participant.Varsta; }
        }

        public string ProbeString
        {
            get
            {
                string all = "";
                foreach (Proba p in Probe)
                    all += p.ToString()+", \n";
                return all;
            }
        }



    }
}