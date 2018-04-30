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
    public class ProbaDBRepository :IProbaRepository
    {
        private IDictionary<String, string> props;
        private IValidator<Proba> validator;

        public ProbaDBRepository(IDictionary<string, string> props, IValidator<Proba> validator)
        {
            this.props = props;
            this.validator = validator;
        }

        public void Delete(int id)
        {
            IDbConnection connection = DBUtils.GetConnection(props);
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "delete from Proba where id=@id";
                IDbDataParameter dataParameter = command.CreateParameter();
                dataParameter.ParameterName = "@id";
                dataParameter.Value = id;
                command.Parameters.Add(dataParameter);
                var dataReader = command.ExecuteNonQuery();
                if (dataReader == 0)
                    throw new RepositoryException("Proba nu exista!");
            }
        }

        public IEnumerable<Proba> FindAll() 
        {
            IDbConnection connection = DBUtils.GetConnection(props);
            IList<Proba> all = new List<Proba>();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "select * from Proba";
                using (var dataReader = command.ExecuteReader())
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

        public Proba FindOne(int id)
        {
            IDbConnection connection = DBUtils.GetConnection(props);
            Proba proba = null;
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "select * from Proba where id=@id";
                IDbDataParameter paramId = command.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = id;
                command.Parameters.Add(paramId);
                using (var dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        int idR = dataReader.GetInt16(0);
                        string nume = dataReader.GetString(1);
                        float distanta = dataReader.GetFloat(2);
                        proba = new Proba { Id = idR, Nume = nume, Distanta = distanta };

                    }
                }
            }
            if (proba == null)
                throw new RepositoryException("Proba nu exista!");
            return proba;
        }

        public void Save(Proba entity)
        {
            validator.Validate(entity);
            IDbConnection connection = DBUtils.GetConnection(props);
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "insert into Proba values(@id,@nume,@distanta)";
                IDbDataParameter paramId = command.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = entity.Id;

                IDbDataParameter paramNume = command.CreateParameter();
                paramNume.ParameterName = "@nume";
                paramNume.Value = entity.Nume;

                IDbDataParameter paramDistanta = command.CreateParameter();
                paramDistanta.ParameterName = "@distanta";
                paramDistanta.Value = entity.Distanta;

                command.Parameters.Add(paramId);
                command.Parameters.Add(paramNume);
                command.Parameters.Add(paramDistanta);

                try { var result = command.ExecuteNonQuery(); }
                catch (SQLiteException)
                {
                    throw new RepositoryException("Proba nu a fost adaugata!");
                }
            }
        }

        public int Size()
        {
            IDbConnection connection = DBUtils.GetConnection(props);
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "select count(*) from Proba";
                var count = command.ExecuteScalar();
               
                return int.Parse(count.ToString());
            }
        }
                
        public void Update(int id, Proba entity)
        {
            validator.Validate(entity);
            IDbConnection connection = DBUtils.GetConnection(props);
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "update Proba set nume=@n,distanta=@d where id=@id";
                IDbDataParameter paramId = command.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = entity.Id;

                IDbDataParameter paramNume = command.CreateParameter();
                paramNume.ParameterName = "@n";
                paramNume.Value = entity.Nume;

                IDbDataParameter paramDist = command.CreateParameter();
                paramDist.ParameterName = "@d";
                paramDist.Value = entity.Distanta;

                command.Parameters.Add(paramNume);
                command.Parameters.Add(paramDist);

                command.Parameters.Add(paramId);

                var result = command.ExecuteNonQuery();
                if (result == 0)
                    throw new RepositoryException("Proba nu a fost modificata!");
            }
        }
    }
}
