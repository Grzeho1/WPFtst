using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFtest2.Class;

namespace WPFtest2
{

    public class Uzivatel
    {
        private List<Uzivatel> uzivatels = new List<Uzivatel>();
        public string Login { get; set; }
        public string Pass { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public int Unread { get; set; }
        public string EmailPass { get; set; }


        // zjisteni jestli je uzivatel admin
        public bool IsAdmin(Uzivatel uzivatel)
        {
            if (uzivatel.Role == "admin")
            {
                return true;
            }
            else
            {
                return false;
            }
        }



        // List pro nacteni vsech zaregistrovanych uzivatelu 
        public List<Uzivatel> GetAll()
        {
            try
            {
                String connectionString = "Data Source = TOMAS; Initial Catalog = Projekt; Integrated Security = True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM Login";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Uzivatel uzivatel = new Uzivatel();
                                uzivatel.Login = reader.GetString(0) ?? "";
                                uzivatel.Pass = reader.GetString(1) ?? "";
                                uzivatel.Email = reader.GetString(2) ?? "";
                                uzivatel.Role = reader.IsDBNull(3) ? "" :reader.GetString(3) ;
                                uzivatel.EmailPass = reader.GetString(4) ?? "";
                                uzivatels.Add(uzivatel);

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return new List<Uzivatel>(uzivatels);

        }
        
    }






}



