using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPPLab3.model
{
    [Serializable]
    public class Inscriere : IHasId<Tuple<string, int>>
    {
        public string IdParticipant { get; set; }
        public int IdProba { get; set; }

        public Tuple<string, int> Id
        {
            get => new Tuple<string, int>(IdParticipant, IdProba);
            set
            {
                IdParticipant = value.Item1;
                IdProba = value.Item2;
            }
        }
    }
}
