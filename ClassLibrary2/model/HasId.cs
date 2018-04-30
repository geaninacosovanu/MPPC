using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPPLab3.model
{
    public interface IHasId<ID>
    {
        ID Id { get; set; }
    }
}
