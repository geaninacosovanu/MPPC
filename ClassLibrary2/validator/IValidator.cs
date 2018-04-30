using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPPLab3.validator
{
    public interface IValidator<E>
    {
        void Validate(E entity);
    }
}
