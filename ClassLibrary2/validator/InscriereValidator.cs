using MPPLab3.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPPLab3.validator
{
    public class InscriereValidator : IValidator<Inscriere>
    {
        public void Validate(Inscriere entity)
        {
            String exc = "";
            if (entity.IdParticipant== null || entity.IdParticipant.Length == 0)
                exc += "Id participant lipsa";
            if (entity.IdProba <0 )
                exc += "Id proba lipsa";
            if (exc.Length!=0)
                throw new ValidationException(exc);
        }
    }
}
