using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY
{
    public class Person
    {
        public string name { get; set; }
        public string full_name { get; set; }
        public string designation { get; set; }
        public string contact_no { get; set; }
        public string email { get; set; }
        public string adress { get; set; }
        public string room_no { get; set; }
        public string type { get; set; }

        public Person(string n, string f, string d, string c, string e, string a, string r)
        {
            name = n;
            full_name = f;
            designation = d;
            contact_no = c;
            email = e;
            adress = a;
            room_no = r;

        }
        
    }
}
