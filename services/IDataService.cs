using System;
using MauiCRUDSqlCE.Models;
using Newtonsoft.Json;
using System.Data.SqlServerCe;
using System.Security.Cryptography;
using System.Dynamic;

namespace MauiCRUDSqlCE.Services
{
    public interface IDataService
    {
        public IEnumerable<ApplicationUser> getAllUsers();
    }

    public class SqlCEDataService : IDataService
    {


        private IEnumerable<ApplicationUser> GenerateInfoFromJson()
        {

            //  var jsonResult = "\"[{\\\"id\\\":\\\"A1S2D3F4-0000-0000-0000-000000000000\\\",\\\"userName\\\":\\\"IsraelGA\\\",\\\"password\\\":\\\"A!1234\\\",\\\"isAdmin\\\":0,\\\"isLocked\\\":0,\\\"isService\\\":0,\\\"isInternal\\\":0,\\\"validFrom\\\":\\\"0001-01-01T00:00:00\\\",\\\"validTo\\\":null,\\\"email\\\":\\\"Israel@ga.com\\\",\\\"phone\\\":\\\"+972-87654302\\\",\\\"lastName\\\":\\\"Israeli\\\",\\\"firstName\\\":\\\"Israel\\\",\\\"company\\\":\\\"Gateways Architects\\\",\\\"userType\\\":null,\\\"ein\\\":null,\\\"salesGroup\\\":0,\\\"salesOrg\\\":null,\\\"distribution\\\":null},{\\\"id\\\":\\\"A1S2D3F4-G5H6-0000-0000-000000000000\\\",\\\"userName\\\":\\\"MosheGA\\\",\\\"password\\\":\\\"A!1234\\\",\\\"isAdmin\\\":0,\\\"isLocked\\\":0,\\\"isService\\\":0,\\\"isInternal\\\":0,\\\"validFrom\\\":\\\"0001-01-01T00:00:00\\\",\\\"validTo\\\":null,\\\"email\\\":\\\"Moshe@ga.com\\\",\\\"phone\\\":\\\"+972-87654302\\\",\\\"lastName\\\":\\\"Moshe\\\",\\\"firstName\\\":\\\"Cohen\\\",\\\"company\\\":\\\"Gateways Architects\\\",\\\"userType\\\":null,\\\"ein\\\":null,\\\"salesGroup\\\":0,\\\"salesOrg\\\":null,\\\"distribution\\\":null},{\\\"id\\\":\\\"A1S2D3F4-G5H6-J7K8-L9P0-000000000000\\\",\\\"userName\\\":\\\"AviEL\\\",\\\"password\\\":\\\"A!1234\\\",\\\"isAdmin\\\":0,\\\"isLocked\\\":0,\\\"isService\\\":0,\\\"isInternal\\\":0,\\\"validFrom\\\":\\\"0001-01-01T00:00:00\\\",\\\"validTo\\\":null,\\\"email\\\":\\\"Aviel@ga.com\\\",\\\"phone\\\":\\\"+972-87654302\\\",\\\"lastName\\\":\\\"Avi\\\",\\\"firstName\\\":\\\"Levi\\\",\\\"company\\\":\\\"Caffe\\\",\\\"userType\\\":null,\\\"ein\\\":null,\\\"salesGroup\\\":0,\\\"salesOrg\\\":null,\\\"distribution\\\":null},{\\\"id\\\":\\\"A1S2D3F4-G5H6-J7K8-0000-000000000000\\\",\\\"userName\\\":\\\"TestUser\\\",\\\"password\\\":\\\"A!1234\\\",\\\"isAdmin\\\":0,\\\"isLocked\\\":0,\\\"isService\\\":0,\\\"isInternal\\\":0,\\\"validFrom\\\":\\\"0001-01-01T00:00:00\\\",\\\"validTo\\\":null,\\\"email\\\":\\\"Israel@ga.com\\\",\\\"phone\\\":\\\"+972-87654302\\\",\\\"lastName\\\":\\\"Israeli\\\",\\\"firstName\\\":\\\"Israel\\\",\\\"company\\\":\\\"Gateways Architects\\\",\\\"userType\\\":null,\\\"ein\\\":null,\\\"salesGroup\\\":0,\\\"salesOrg\\\":null,\\\"distribution\\\":null}]\"";
            var jsonResult = "[{\"id\":\"afd15b54-babf-4812-8363-d0644b8cc5ce\",\"userName\":\"KfirL\",\"password\":\"A!1234\",\"isAdmin\":0,\"isLocked\":0,\"isService\":0,\"isInternal\":0,\"validFrom\":\"0001-01-01T00:00:00\",\"validTo\":null,\"email\":\"KfirL@ga.com\",\"phone\":\"+972-87654302\",\"lastName\":\"Levi\",\"firstName\":\"Kfir\",\"company\":\"Caffe Sonol\",\"userType\":null,\"ein\":null,\"salesGroup\":0,\"salesOrg\":null,\"distribution\":null},{\"id\":\"afd15b54-babf-4812-8363-d0644b8cc5ce\",\"userName\":\"mosheC\",\"password\":\"A!1234\",\"isAdmin\":0,\"isLocked\":0,\"isService\":0,\"isInternal\":0,\"validFrom\":\"0001-01-01T00:00:00\",\"validTo\":null,\"email\":\"moshec@sonol.com\",\"phone\":\"+972-9191345\",\"lastName\":\"Cohen\",\"firstName\":\"Moshe\",\"company\":\"Sonol\",\"userType\":null,\"ein\":null,\"salesGroup\":0,\"salesOrg\":null,\"distribution\":null},{\"id\":\"afd15b54-babf-4812-8363-d0644b8cc5ce\",\"userName\":\"IsraelGA\",\"password\":\"A!1234\",\"isAdmin\":0,\"isLocked\":0,\"isService\":0,\"isInternal\":0,\"validFrom\":\"0001-01-01T00:00:00\",\"validTo\":null,\"email\":\"Israel@ga.com\",\"phone\":\"+972-87654302\",\"lastName\":\"Israeli\",\"firstName\":\"Israel\",\"company\":\"Gateways Architects\",\"userType\":null,\"ein\":null,\"salesGroup\":0,\"salesOrg\":null,\"distribution\":null}]";

            return JsonConvert.DeserializeObject<List<ApplicationUser>>(jsonResult);
        }


        public dynamic GetUsers() {

            dynamic users = new ExpandoObject();
            try
            {
                //var sql = "SELECT U.Id,'Sales' as UserApplication, COALESCE(U.UserName,'') as UserName, COALESCE(SC.UserName,'') SAPUserId, COALESCE(CONVERT(nvarchar(200), SC.Password, 0),'')  SAPPassword,COALESCE(U.LastName,'') LastName, COALESCE(U.FirstName,'') FirstName, COALESCE(U.FirstName,'')+' '+COALESCE(U.LastName,'') FullName, COALESCE(U.Phone,'') Phone, COALESCE(U.Email,'') Email, U.IsAdmin, U.IsLocked, U.IsInternal, CASE U.IsLocked WHEN 1 THEN 'Not Active' ELSE 'Active' END UserStatus, CONVERT(nvarchar(10), U.Validto, 121) as UserExpirationDate, UT.UserTypeEng as UserType, UT.UserTypeEng as  DispUserType from Users";
                var sql = "select * from Users";

                using (SqlCeConnection connection = GetConnection())
                {
                    using (SqlCeCommand command = connection.CreateCommand())
                    {
                        command.CommandText = sql;

                        //command.ExecuteNonQuery();

                        //command.CommandText = "SELECT * FROM Blogs";

                        users = command.ExecuteReader().GetEnumerator();
                    }
                    //var SonolUsersList = connection.BeginTransactionAsync(sql).Result.ToList();
                    //users.SonolUsersList = SonolUsersList;
                    //users.success = true;
                }
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "[Catch Exception]");
                //users.success = false;
               // users.Message = ex.StackTrace;
                return (Newtonsoft.Json.JsonConvert.SerializeObject(users));
                //throw;
            }
            return (Newtonsoft.Json.JsonConvert.SerializeObject(users));
        }

        #region GetConnection
        public static SqlCeConnection GetConnection()
        {
            string _connectionString = "Data Source=App_Data/MeisterCOTSIdentity.sdf";
            /*
            #if DEV
                        string _connectionString = "Data Source=App_Data/Dev/MeisterCOTSIdentity.sdf";
            #endif
            #if TEST
                        string _connectionString = "Data Source=App_Data/Test/MeisterCOTSIdentity.sdf";
            #endif
            #if PROD
                        string _connectionString = "Data Source=App_Data/Prod/MeisterCOTSIdentity.sdf";
            #endif
            */
            SqlCeConnection _connection = new SqlCeConnection(_connectionString);
            _connection.Open();
            return _connection;
        }
        #endregion

        #region EncryptString
        public static string EncryptString(string plainInput, string key)
        {
            byte[] iv = new byte[16];
            byte[] array;
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;
                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                        {
                            streamWriter.Write(plainInput);
                        }

                        array = memoryStream.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(array).Replace('/', '_');
        }
        #endregion

        #region DecryptString
        public static string DecryptString(string cipherText, string key)
        {
            byte[] iv = new byte[16];
            byte[] buffer = Convert.FromBase64String(cipherText);
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                using (MemoryStream memoryStream = new MemoryStream(buffer))
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }
        #endregion




        public IEnumerable<ApplicationUser> getAllUsers()
        {
            //return GenerateInfoFromJson();
            return GetUsers();
        }
    }    
    
}

