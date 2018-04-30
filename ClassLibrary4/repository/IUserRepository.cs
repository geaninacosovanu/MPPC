using MPPLab3.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPPLab3.repository
{
    public interface IUserRepository:IRepository<string,User>
    {
         bool Exists(User u);
    }
}
