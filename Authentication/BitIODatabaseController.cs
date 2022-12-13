using Firebase.Auth;
using Npgsql;
using System.Data;

namespace SEGP7.Firebase
{

    public class BitIODatabaseController
    {
        FirebaseAuthLink currentCredentials;
        String DBURL;
        public BitIODatabaseController()
        {
            DatabaseSetup();
            SubscribeAndSendMessages();
        }

        void WriteJournalEntry(String data)
        {
            try
            {
                using var con = new NpgsqlConnection(DBURL);
                con.Open();
                var sql = $"UPDATE userdata SET journal = '{data}' WHERE email = '{currentCredentials.User.Email}';";
                using var cmd = new NpgsqlCommand(sql, con);
                using NpgsqlDataReader reader = cmd.ExecuteReader();
                con.Close();
            }  catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
        }

        void WriteEntry(String data, String columnName)
        {
            try
            {
                using var con = new NpgsqlConnection(DBURL);
                con.Open();
                var sql = $"UPDATE userdata SET {columnName} = '{data}' WHERE email = '{currentCredentials.User.Email}';";
                using var cmd = new NpgsqlCommand(sql, con);
                using NpgsqlDataReader reader = cmd.ExecuteReader();
                con.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        String LoadEntry(String columnName)
        {
            try
            {
                using var con = new NpgsqlConnection(DBURL);
                con.Open();
                var sql = $"SELECT journal FROM {columnName} WHERE email = '{currentCredentials.User.Email}';";
                using var cmd = new NpgsqlCommand(sql, con);
                using NpgsqlDataReader reader = cmd.ExecuteReaderAsync().Result;
                reader.Read();
                String result = (String)reader[0];
                reader.Close();
                con.Close();
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return "{}";
            }
        }
        
        async void LoadJournalEntry()
        {
            try
            {
                using var con = new NpgsqlConnection(DBURL);
                con.Open();
                var sql = $"SELECT journal FROM userdata WHERE email = '{currentCredentials.User.Email}';";
                using var cmd = new NpgsqlCommand(sql, con);
                using NpgsqlDataReader reader = await cmd.ExecuteReaderAsync();
                reader.Read();
                Console.Write("Test");
                MessagingCenter.Send((String)reader[0], "SendLoadedJournal");
                reader.Close();
                con.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        
        void RegisterUser(String email)
        {
            //INSERT INTO userdata VALUES ('test@test.com', '{}', '{}','{}')
            try
            {
                using var con = new NpgsqlConnection(DBURL);
                con.Open();
                var sql = $"INSERT INTO userdata VALUES ('{email}', '{{}}', '{{}}','{{}}');";
                using var cmd = new NpgsqlCommand(sql, con);
                using NpgsqlDataReader reader = cmd.ExecuteReaderAsync().Result;
                reader.Close();
                con.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        void DeleteAccount(String email)
        {
            try
            {
                using var con = new NpgsqlConnection(DBURL);
                con.Open();
                var sql = $"DELETE FROM userdata WHERE email='{email}';";
                using var cmd = new NpgsqlCommand(sql, con);
                using NpgsqlDataReader reader = cmd.ExecuteReaderAsync().Result;
                reader.Close();
                con.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        void DatabaseSetup()
        {
            var bitHost = "db.bit.io";
            var bitUser = "Otteko";
            var bitDbName = "Otteko/SEGP";
            var bitApiKey = "v2_3wjDP_Zhz4GqYM4MZXpTv4vGVChtX";
            DBURL = $"Host={bitHost};Username={bitUser};Password={bitApiKey};Database={bitDbName}";
        }
    
         void SubscribeAndSendMessages()
        {
            //Subscribe to new credentials
            MessagingCenter.Subscribe<FirebaseAuthLink>(this, "GetCredentials", (newCredentials) =>
            {
                currentCredentials = newCredentials;
            });
            //Subscribe to FirebaseAuth new user
            MessagingCenter.Subscribe<String>(this, "NewUserBitIO", (email) =>
            {
                RegisterUser(email);
            });
            //Subscribe to Deleting current user
            MessagingCenter.Subscribe<String>(this, "DeleteUser", (email) =>
            {
                DeleteAccount(email);
            });

            //Subscribe to writing journal entries
            MessagingCenter.Subscribe<String>(this, "SaveJournal", (EntryObject) =>
            {
                WriteJournalEntry(EntryObject);
            });
            //Subscribe to loading journal requests
            MessagingCenter.Subscribe<String>(this, "LoadJournal", (EntryObject) =>
            {
                LoadJournalEntry();
            });
        }
    }
}