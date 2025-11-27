using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExoCommande.Dao
{
    internal class DataConnection
    {
        private static readonly string connectionString = "Data Source=(localdb)\\CoursSQL;Initial Catalog=CoursSQL;Integrated Security=True";

        public static SqlConnection GetConnection => new(connectionString);
    }
}
