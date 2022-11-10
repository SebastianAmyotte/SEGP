using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEGP.Pages.Database
{
    internal class DatabaseController
    {
        String connectionString; //TO USE

        DatabaseController()
        {
            InitializeConnectionString();
        }
        public String InitializeConnectionString()
        {
            var bitHost = "db.bit.io";
            var bitApiKey = "v2_3vSkE_AmARGQVzJdVy4XGwYabsdPy"; // from the "Password" field of the "Connect" menu

            var bitUser = "Otteko";
            var bitDbName = "Otteko/lab3";

            return connectionString = $"Host={bitHost};Username={bitUser};Password={bitApiKey};Database={bitDbName}";
        }
    }
}
