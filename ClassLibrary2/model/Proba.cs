using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPPLab3.model
{
    [Serializable]
    public class Proba : IHasId<int>
    {
        private int _Id;
        public string Nume { get; set; }
        public float Distanta{ get; set; }
        public int Id { get => _Id; set => _Id = value; }

        public override string ToString()
        {
            return Nume+" "+Distanta;
        }
    }
}
