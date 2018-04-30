using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPPLab3.model
{
    [Serializable]
    public class User:IHasId<string>
    {
        private string _Id;

       
        public string Parola { get; set; }

        public string Id { get => _Id; set => _Id = value; }
    }
}
