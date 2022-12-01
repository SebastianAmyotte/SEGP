//Author: Sebastian Amyotte
//Reviewer: Sebastian Amyotte
//This may be replaced in lieu of a Firebase database

namespace SEGP.Pages.Database
{
    internal class DatabaseController
    {
        String connectionString; //TO USE

        DatabaseController()
        {
            InitializeConnectionString();
        }
        public void InitializeConnectionString()
        {
            var bitHost = "db.bit.io";
            var bitApiKey = "v2_3vSkE_AmARGQVzJdVy4XGwYabsdPy"; // from the "Password" field of the "Connect" menu

            var bitUser = "Otteko";
            var bitDbName = "Otteko/lab3";

            connectionString = $"Host={bitHost};Username={bitUser};Password={bitApiKey};Database={bitDbName}";
        }
    }
}