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
            MessagingCenter.Subscribe<FirebaseAuthLink>(this, "GetCredentials", (newCredentials) =>
            {
                currentCredentials = newCredentials;
            });
            MessagingCenter.Send("", "SendCredentials");
            //Subscribe to writing journal entries
            MessagingCenter.Subscribe<String>(this, "SaveJournal", (EntryObject) =>
            {
                WriteJournalEntry(EntryObject);
            });
            //async next to (EntryObject) may be unnecessary
            MessagingCenter.Subscribe<String>(this, "LoadJournal", async (EntryObject) => 
            {
                //Create an asynchronous task
                Task task = Task.Run(() => LoadJournalEntry());

            });
            //Subscribe to loading journal entries

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

        async void LoadJournalEntry()
        {
            Console.WriteLine($"Loading from database: {DateTimeOffset.Now.ToUnixTimeMilliseconds()}");
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
                Console.WriteLine($"Got from database: {DateTimeOffset.Now.ToUnixTimeMilliseconds()}");
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
    
        
    }
}