using MPPLab3.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPPLab3.validator
{
    public class ParticipantValidator : IValidator<Participant>
    {
        public void Validate(Participant entity)
        {
            string exc = "";
            if (entity.Id==null || entity.Id.Length==0)
                exc += "Id incorect";
            if (entity.Nume == null || entity.Nume.Length == 0)
                exc += "Nume lipsa";
            if (entity.Varsta <= 0)
                exc += "Varsta incorecta";
            if (exc.Length != 0)
                throw new ValidationException(exc);
        }
    }
}
