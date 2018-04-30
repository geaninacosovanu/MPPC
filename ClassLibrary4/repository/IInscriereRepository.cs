using MPPLab3.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPPLab3.repository
{
    public interface IInscriereRepository:IRepository<Tuple<string,int>,Inscriere>
    {
       int GetNrParticipantiProba(int idProba);
       IEnumerable<Participant> GetParticipantiPtProba(int idProba);
       IEnumerable<Proba> GetProbePtParticipant(string idParticipant);
    }
}
