using MPPLab3.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPPLab3.validator
{
    public class ProbaValidator : IValidator<Proba>
    {
        public void Validate(Proba entity)
        {
            string exc = "";
            if ( entity.Id < 0)
                exc += "Id incorect";
            if (entity.Nume == null || entity.Nume.Length == 0)
                exc += "Nume lipsa";
            if (entity.Distanta <= 0)
                exc += "Distanta incorecta";
            if (exc.Length != 0)
                throw new ValidationException(exc);
        }
    }
}
