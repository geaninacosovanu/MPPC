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
    public class ParticipantDBRepository : IParticipantRepository
    {
        private IDictionary<String, string> props;
        private IValidator<Participant> validator;

        public ParticipantDBRepository(IDictionary<string, string> props, IValidator<Participant> validator)
        {
            this.props = props;
            this.validator = validator;
        }

        public void Delete(string id)
        {
            IDbConnection connection = DBUtils.GetConnection(props);
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "delete from Participant where id=@id";
                IDbDataParameter dataParameter = command.CreateParameter();
                dataParameter.ParameterName = "@id";
                dataParameter.Value = id;
                command.Parameters.Add(dataParameter);
                var dataReader = command.ExecuteNonQuery();
                if (dataReader == 0)
                    throw new RepositoryException("Proba nu exista!");
            }
        }

        public IEnumerable<Participant> FindAll()
        {
            IDbConnection connection = DBUtils.GetConnection(props);
            IList<Participant> all = new List<Participant>();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "select * from Participant";
                using (var dataReader = command.ExecuteReader())
                {
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

        public Participant FindOne(string id)
        {
            IDbConnection connection = DBUtils.GetConnection(props);
            Participant p= null;
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "select * from Participant where id=@id";
                IDbDataParameter paramId = command.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = id;
                command.Parameters.Add(paramId);
                using (var dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        string idR = dataReader.GetString(0);
                        string nume = dataReader.GetString(1);
                        int varsta = dataReader.GetInt16(2);
                        p = new Participant { Id = idR, Nume = nume, Varsta = varsta };

                    }
                }
            }
            return p;
        }

        public Participant GetParticipant(string nume, int varsta)
        {
            IDbConnection connection = DBUtils.GetConnection(props);
            Participant p = null;
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "select * from Participant where nume=@nume AND varsta=@varsta";
                IDbDataParameter paramNume = command.CreateParameter();
                paramNume.ParameterName = "@nume";
                paramNume.Value = nume;
                IDbDataParameter paramVarsta = command.CreateParameter();
                paramVarsta.ParameterName = "@varsta";
                paramVarsta.Value = varsta;
                command.Parameters.Add(paramNume);
                command.Parameters.Add(paramVarsta);

                using (var dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        string idR = dataReader.GetString(0);
                        string numeR = dataReader.GetString(1);
                        int varstaR = dataReader.GetInt16(2);
                        p = new Participant { Id = idR, Nume = numeR, Varsta = varstaR };

                    }
                }
            }
            return p;
        }

        public void Save(Participant entity)
        {
            validator.Validate(entity);

            IDbConnection connection = DBUtils.GetConnection(props);
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "insert into Participant values(@id,@nume,@varsta)";
                IDbDataParameter paramId = command.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = entity.Id;

                IDbDataParameter paramNume = command.CreateParameter();
                paramNume.ParameterName = "@nume";
                paramNume.Value = entity.Nume;

                IDbDataParameter paramVarsta = command.CreateParameter();
                paramVarsta.ParameterName = "@varsta";
                paramVarsta.Value = entity.Varsta;

                command.Parameters.Add(paramId);
                command.Parameters.Add(paramNume);
                command.Parameters.Add(paramVarsta);

                try { var result = command.ExecuteNonQuery(); }
                catch (SQLiteException e)
                {
                    var x = e.Message;
                    throw new RepositoryException("Participantul nu a fost adaugat!");
                }
            }
        }

        public int Size()
        {
            IDbConnection connection = DBUtils.GetConnection(props);
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "select count(*) from Participant";
                var count = command.ExecuteScalar();

                return int.Parse(count.ToString());
            }
        }

        public void Update(string id, Participant entity)
        {
            validator.Validate(entity);

            IDbConnection connection = DBUtils.GetConnection(props);
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "update Participant set nume=@n,varsta=@v where id=@id";
                IDbDataParameter paramId = command.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = entity.Id;

                IDbDataParameter paramNume = command.CreateParameter();
                paramNume.ParameterName = "@n";
                paramNume.Value = entity.Nume;

                IDbDataParameter paramVarsta = command.CreateParameter();
                paramVarsta.ParameterName = "@v";
                paramVarsta.Value = entity.Varsta;

                command.Parameters.Add(paramNume);
                command.Parameters.Add(paramVarsta);

                command.Parameters.Add(paramId);

                var result = command.ExecuteNonQuery();
                if (result == 0)
                    throw new RepositoryException("Participantul nu a fost modificat!");
            }
        }
    }
}
