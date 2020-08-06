using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostgreSql_Otomasyon
{
    
    class connect
    {
        public string connstring = String.Format("Server={0};Port={1};" +
           "User Id={2};Password={3};Database={4};",
           "localhost", 5432, "postgres", "123456", "DbOtomasyon");
        public NpgsqlConnection baglanti()
        {
            NpgsqlConnection baglan = new NpgsqlConnection(connstring);
            baglan.Open();
            return baglan;
        }
    }
}
