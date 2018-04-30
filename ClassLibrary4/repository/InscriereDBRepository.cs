using MPPLab3.model;
using MPPLab3.validator;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPPLab3.repository
{
    public class InscriereDBRepository : IInscriereRepository
    {
        private IDictionary<String, string> props;
        private IValidator<Inscriere> validator;

        public InscriereDBRepository(IDictionary<string, string> props, IValidator<Inscriere> validator)
        {
            this.props = props;
            this.validator = validator;
        }

        public void Delete(Tuple<string, int> id)
        {
            IDbConnection connection = DBUtils.GetConnection(props);
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "delete from Inscriere where idParticipant=@idP AND idProba=@pr";
                IDbDataParameter dataParameter = command.CreateParameter();
                dataParameter.ParameterName = "@idP";
                dataParameter.Value = id.Item1;

                IDbDataParameter dataParameter2 = command.CreateParameter();
                dataParameter2.ParameterName = "@pr";
                dataParameter2.Value = id.Item2;
                command.Parameters.Add(dataParameter);
                command.Parameters.Add(dataParameter2);

                var dataReader = command.ExecuteNonQuery();
                if (dataReader == 0)
                    throw new RepositoryException("Inscrierea nu exista!");
            }
        }

        public IEnumerable<Inscriere> FindAll()
        {
            IDbConnection connection = DBUtils.GetConnection(props);
            IList<Inscriere> all = new List<Inscriere>();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "select * from Inscriere";
                using (var dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        string idPart = dataReader.GetString(0);
                        int idPr = dataReader.GetInt16(1);
                        Inscriere ins = new Inscriere { IdParticipant = idPart, IdProba = idPr };
                        all.Add(ins);
                    }
                }
            }
            return all;

        }

        public Inscriere FindOne(Tuple<string, int> id)
        {
            IDbConnection connection = DBUtils.GetConnection(props);
            Inscriere ins = null;
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "select * from Inscriere where idParticipant=@idPart AND idProba=@idPr";
                IDbDataParameter paramId = command.CreateParameter();
                paramId.ParameterName = "@idPart";
                paramId.Value = id.Item1;
                command.Parameters.Add(paramId);
                IDbDataParameter paramId2 = command.CreateParameter();
                paramId2.ParameterName = "@idPr";
                paramId2.Value = id.Item2;
                command.Parameters.Add(paramId);
                command.Parameters.Add(paramId2);
                using (var dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        string idPart = dataReader.GetString(0);
                        int idPr = dataReader.GetInt16(1);
                        ins = new Inscriere { IdParticipant = idPart, IdProba = idPr };

                    }
                }
            }
            if (ins == null)
                throw new RepositoryException("Inscrierea nu exista!");
            return ins;
        }

        public void Save(Inscriere entity)
        {
            validator.Validate(entity);

            IDbConnection connection = DBUtils.GetConnection(props);
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "insert into Inscriere values(@idPart,@idPr)";
                IDbDataParameter paramId = command.CreateParameter();
                paramId.ParameterName = "@idPart";
                paramId.Value = entity.IdParticipant;

                IDbDataParameter paramId2 = command.CreateParameter();
                paramId2.ParameterName = "@idPr";
                paramId2.Value = entity.IdProba;

                command.Parameters.Add(paramId);
                command.Parameters.Add(paramId2);

                try { var result = command.ExecuteNonQuery(); }
                catch (SQLiteException)
                {
                    throw new RepositoryException("Inscrierea nu a fost adaugata!");
                }
            }
        }

        public int Size()
        {
            IDbConnection connection = DBUtils.GetConnection(props);
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "select count(*) from Inscriere";
                var count = command.ExecuteScalar();

                return int.Parse(count.ToString());
            }
        }

        public void Update(Tuple<string, int> id, Inscriere entity)
        {
            validator.Validate(entity);

            IDbConnection connection = DBUtils.GetConnection(props);
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "update Inscriere set idParticipant=@part, idProba=@pr WHERE idParticipant=@p1 AND idProba=@p2";
                IDbDataParameter paramId = command.CreateParameter();
                paramId.ParameterName = "@part";
                paramId.Value = entity.IdParticipant;

                IDbDataParameter paramId1 = command.CreateParameter();
                paramId1.ParameterName = "@pr";
                paramId1.Value = entity.IdProba;

                IDbDataParameter paramId2 = command.CreateParameter();
                paramId2.ParameterName = "@p1";
                paramId2.Value = id.Item1;

                IDbDataParameter paramId3 = command.CreateParameter();
                paramId3.ParameterName = "@p2";
                paramId3.Value = id.Item2;

                command.Parameters.Add(paramId);
                command.Parameters.Add(paramId1);
                command.Parameters.Add(paramId2);
                command.Parameters.Add(paramId3);

                var result = command.ExecuteNonQuery();
                if (result == 0)
                    throw new RepositoryException("Inscrierea nu a fost modificata!");
            }
        }
        public int GetNrParticipantiProba(int idProba)
        {
            int size = 0;
            IEnumerable<Participant> all = GetParticipantiPtProba(idProba);
            
            foreach(var p in all)
                size++;
            return size;
        }
        public IEnumerable<Participant> GetParticipantiPtProba(int idProba) {
            IList<Participant> all = new List<Participant>();
            IDbConnection connection = DBUtils.GetConnection(props);
            using(var commnad = connection.CreateCommand())
            {
                commnad.CommandText = "SELECT P2.id,P2.nume,P2.varsta FROM  Inscriere I INNER JOIN Participant P2 ON I.idParticipant = P2.id WHERE I.idProba=@id";
                IDbDataParameter parameter = commnad.CreateParameter();
                parameter.ParameterName = "@id";
                parameter.Value = idProba;
                commnad.Parameters.Add(parameter);
                using (var dataReader = commnad.ExecuteReader()) {
                    while (dataReader.Read())
                    {
                        string id = dataReader.GetString(0);
                        string nume = dataReader.GetString(1);
                        int varsta = dataReader.GetInt16(2);
                        Participant p = new Participant { Id = id, Nume = nume, Varsta = varsta };
                        all.Add(p);
                    }
                }
            
            }
            return all;
        }
        public IEnumerable<Proba> GetProbePtParticipant(string idParticipant)
        {
            IList<Proba> all = new List<Proba>();
            IDbConnection connection = DBUtils.GetConnection(props);
            using (var commnad = connection.CreateCommand())
            {
                commnad.CommandText = "SELECT P2.id,P2.nume,P2.distanta  FROM  Inscriere I INNER JOIN main.Proba P2 ON I.idProba = P2.id WHERE I.idParticipant=@id";
                IDbDataParameter parameter = commnad.CreateParameter();
                parameter.ParameterName = "@id";
                parameter.Value = idParticipant;
                commnad.Parameters.Add(parameter);
                using (var dataReader = commnad.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        int id = dataReader.GetInt16(0);
                        string nume = dataReader.GetString(1);
                        float distanta = dataReader.GetFloat(2);
                        Proba proba = new Proba { Id = id, Nume = nume, Distanta = distanta };
                        all.Add(proba);
                    }
                }

            }
            return all;
        }

    }
}
