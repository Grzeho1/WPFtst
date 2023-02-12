using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFtest2
{
   
   public  class Uzivatel
    {
       public  string Login { get; set; }
        public string Heslo { get; set; }
        public string Role { get; set; }


       

        public bool IsAdmin()
        {
            return Role.Equals("admin");
        }
    }






}



