using MPPLab3.model;
using MPPLab3.validator;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Security.Cryptography;
using System.Text;

namespace MPPLab3.repository
{
    public class UserDBRepository : IUserRepository
    {   
        private IDictionary<String, string> props;
        private IValidator<User> validator;

        public UserDBRepository(IDictionary<string, string> props, IValidator<User> validator)
        {
            this.props = props;
            this.validator = validator;
        }

        public void Delete(string id)
        {
            IDbConnection connection = DBUtils.GetConnection(props);
            using(var command = connection.CreateCommand())
            {
                command.CommandText = "delete from User where userId=@id";
                IDbDataParameter dataParameter = command.CreateParameter();
                dataParameter.ParameterName = "@id";
                dataParameter.Value = id;
                command.Parameters.Add(dataParameter);
                var dataReader = command.ExecuteNonQuery();
                if (dataReader == 0)
                    throw new RepositoryException("Nu exista userul!");
            }
        }

        public IEnumerable<User> FindAll()
        {
            IDbConnection connection = DBUtils.GetConnection(props);
            IList<User> all = new List<User>();
            using (var command = connection.CreateCommand()) {
                command.CommandText = "select * from User";
                using (var dataReader = command.ExecuteReader()) {
                    while (dataReader.Read()) {
                        string id = dataReader.GetString(0);
                        string parola = dataReader.GetString(1);
                        User user = new User { Id = id, Parola = parola };
                        all.Add(user);
                    }
                }
            }
            return all;

        }

        public User FindOne(string id)
        {
            IDbConnection connection = DBUtils.GetConnection(props);
            User user=null;
            using (var command = connection.CreateCommand())
            {
                IDbDataParameter paramId = command.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = id;
                command.CommandText = "select * from User where userId=@id";
                command.Parameters.Add(paramId);
                using (var dataReader = command.ExecuteReader())
                {

                    while (dataReader.Read())
                    {
                        string idR = dataReader.GetString(0);
                        string parola = dataReader.GetString(1);
                        
                        user = new User { Id = idR, Parola = parola };

                    }
                }
            }
            if (user == null)
                throw new RepositoryException("Nu exista user-ul cu id ul dat!");
            return user;
        }

        private string EncryptParola(string parola)
        {
            
            MD5 md5 = MD5.Create();
            byte[] data = md5.ComputeHash(Encoding.UTF8.GetBytes(parola));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }
        public void Save(User entity)
        {
            validator.Validate(entity);

            IDbConnection connection = DBUtils.GetConnection(props);
            using(var command = connection.CreateCommand())
            {
                command.CommandText = "insert into User(userId,parola) values (@id,@parola)";
                IDbDataParameter paramId = command.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = entity.Id;

                IDbDataParameter paramParola = command.CreateParameter();
                paramParola.ParameterName = "@parola";
                paramParola.Value = EncryptParola(entity.Parola);


                command.Parameters.Add(paramId);
                command.Parameters.Add(paramParola);
                try { var result = command.ExecuteNonQuery(); }
                catch (SQLiteException)
                {
                     throw new RepositoryException("Userul nu a fost adaugat!");
                }
            }
        }

        public int Size()
        {
            IDbConnection connection = DBUtils.GetConnection(props);
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "select count(*) as nr from User";
                using (var dataReader = command.ExecuteReader())
                    if (dataReader.Read())
                    {
                        var size = dataReader.GetInt16(0);
                        return size;
                    }
            }
            return default(int);

        }
        public bool Exists(User u)
        {
            string generatedPassword = EncryptParola(u.Parola);
            IDbConnection connection = DBUtils.GetConnection(props);
            string parola=null;
            using (var command = connection.CreateCommand())
            {
                IDbDataParameter paramId = command.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = u.Id;
                command.CommandText = "select parola from User where userId=@id";
                command.Parameters.Add(paramId);
                using (var dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        parola = dataReader.GetString(0);
                    }
                }
            }
            if (parola==null || !parola.Equals(generatedPassword))
                return false;
            return true;

        }

        public void Update(string id, User entity)
        {
            validator.Validate(entity);
            IDbConnection connection = DBUtils.GetConnection(props);
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "update User set parola=@p where userId=@id";
                IDbDataParameter paramId = command.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = id;

                IDbDataParameter paramParola = command.CreateParameter();
                paramParola.ParameterName = "@p";
                paramParola.Value = EncryptParola(entity.Parola);

                command.Parameters.Add(paramParola);

                command.Parameters.Add(paramId);

                var result = command.ExecuteNonQuery();
                if (result == 0)
                    throw new RepositoryException("Userul nu a fost modificat!");
            }
        }
    }
}
