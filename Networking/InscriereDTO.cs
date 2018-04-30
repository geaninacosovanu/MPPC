using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPPLab3.model;

namespace Networking
{
    [Serializable]

    public class InscriereDTO
    {
        public bool Existent { get; set; }
        public List<Proba> Probe { get; set; }
        public string Nume { get;  set; }
        public int Varsta { get; set; }
    }
}
