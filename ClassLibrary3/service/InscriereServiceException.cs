using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPPLab3.service
{
    public class InscriereServiceException : Exception
    {
        public InscriereServiceException() : base() { }

        public InscriereServiceException(String msg) : base(msg) { }

        public InscriereServiceException(String msg, Exception ex) : base(msg, ex) { }

    }
}
