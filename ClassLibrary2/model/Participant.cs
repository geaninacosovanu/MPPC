using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPPLab3.model
{
    [Serializable]
    public class Participant : IHasId<string>
    {
        private string _Id;
        public string Nume { get; set; }
        public int Varsta { get; set; }

        public string Id { get => _Id; set => _Id = value; }
    }
}
