using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY
{
    public class EntityClass
    {
        public string server = null;
        public string port = null;
        public string userid = null;
        public string password = null;
        public string schema = null;
        public string login_table = null;
        public string member_table = null;
        public string task_table = null;

        public string ftpusername = null;
        public string ftppassword = null;
        public string ftpserver = null;
         


        public EntityClass(string s, string p, string uid, string pass, string t, string logtable, string memtable, string tasktable, string fu, string fp, string fs)
        {
            server = s;
            port = p;
            userid = uid;
            password = pass;
            schema = t;
            login_table = logtable;
            member_table = memtable;
            task_table = tasktable;

            ftpusername = fu;
            ftppassword = fp;
            ftpserver = fs;
        }
    }
}
