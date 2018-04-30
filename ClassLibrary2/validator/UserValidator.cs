using MPPLab3.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPPLab3.validator
{
    public class UserValidator : IValidator<User>
    {
        public void Validate(User entity)
        {
            string exc = "";
            if (entity.Id == null || entity.Id.Length == 0)
                exc += "Id lipsa";

            if (entity.Parola == null || entity.Parola.Length == 0)
                exc += "Parola lipsa";
            if (exc.Length!=0)
                throw new ValidationException(exc);
        }
    }
}
