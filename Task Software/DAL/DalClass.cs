using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENTITY;
using System.Data;
namespace DAL
{
    public class DalClass
    {
        public Queue<string> notification_queue = new Queue<string>();

        public String mainname;
        public String admin_or_member;
        public String temp;
        MySqlConnection conn;
        
        EntityClass entitylayer;
        // EntityClass entitylayer = new EntityClass("198.72.115.129", "3306", "rebelfre_task", "new1234@#", "rebelfre_task_management", "login", "member", "task_info");

        public DalClass(EntityClass e)
        {
            entitylayer = e;
        }

        // Database connector

        public string dbconnect()
        {
            string message = null;
            string connstring = "SERVER =" + entitylayer.server + ";PORT = " + entitylayer.port + ";USERID = " + entitylayer.userid + ";PASSWORD = " + entitylayer.password;
            try
            {

                conn = new MySqlConnection();
                conn.ConnectionString = connstring;
                conn.Open();
                message = "Connect";

            }
            catch (Exception ex)
            {

                message = ex.ToString();
                Console.WriteLine(message);
            }
            return message;
        }

        // Schema creation
        public string create_schema()
        {


            string message = dbconnect();
            if (message == "Can't connect")
            {
                return message;
            }
            else
            {
                try
                {
                    string query = "CREATE SCHEMA `" + entitylayer.schema + "` ;";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataReader reader;
                    cmd.ExecuteReader();
                    message = "Schema created";
                }
                catch (Exception e)
                {
                    message = "Schema can't created";
                }
                return message;
            }


        }

        //Login table creation

        public string create_login_table()
        {

            string message = dbconnect();
            if (message == "Can't connect")
            {
                return message;
            }
            else
            {
                try
                {
                    string query = "CREATE TABLE `" + entitylayer.schema + "`.`" + entitylayer.login_table + "` (`name` VARCHAR(40) NOT NULL,`password` VARCHAR(45) NOT NULL,`type` VARCHAR(45) NOT NULL, PRIMARY KEY (`name`));";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataReader reader;
                    cmd.ExecuteReader();
                    message = "login created";
                }
                catch (Exception e)
                {
                    message = "login can't created";
                }
                return message;
            }
        }
        public void dummy_login()
        {

            string message = dbconnect();
            if (message == "Can't connect")
            {
                
            }
            else
            {
                try
                {
                    string query = "INSERT INTO `" + entitylayer.schema + "`.`" + entitylayer.login_table + "` (`name`, `password`, `type`) VALUES ('admin', 'admin', 'Admin');";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataReader reader;
                    cmd.ExecuteReader();
                    add_member("admin", "ADMIN", "Professor", "01676111111", "admin@gmail.com", "1212", "Dhaka", "admin", "Admin");
                    message = "member saved";
                }
                catch (Exception e)
                {
                    message = "member can't saved";
                }
                

            }


        }
        public Boolean change_password(string username, string new_password1, string new_password2)
        {
            Boolean f = false;
            string message = dbconnect();
              try
                {
                    string query = "UPDATE `" + entitylayer.schema + "`.`" + entitylayer.login_table + "` SET `password`='" + new_password1 + "' WHERE `name`='" + username + "'; ";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataReader reader;
                    reader = cmd.ExecuteReader();

                    if (reader.RecordsAffected == 1)
                    {
                    notification_queue.Enqueue("Changed successfully");
                    f = true;
                    }
                    
                }
                catch (Exception e)
                {

                notification_queue.Enqueue("Cannot change. Try again.");
            }

            return f;
        }
        public string sign_in(string username, string password)
        {
            
            string message = dbconnect();
            if (message == "Can't connect")
            {
                return message;
            }
            else
            {
                string query = "SELECT * FROM " + entitylayer.schema + "." + entitylayer.login_table + " WHERE Name = '" + username + "' AND Password = '" + password + "';";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                int count = 0;
                while (reader.Read())
                {
                    count += 1;
                }
                conn.Close();
                if (count != 0)
                {
                    DataSet ds;
                    ds = get_login_type(username);
                    DataTable dt = new DataTable();
                    dt = ds.Tables["login"];
                    string type;
                    type = dt.Rows[0]["type"].ToString();


                    if (type == "Admin")
                    {
                        message = "Admin";

                    }
                    else if (type == "Member")
                    {
                        message = "Member";

                    }
                    count = 0;

                }
                else
                {
                    message = "Wrong Combination";
                }

                return message;

            }

            // string ff = dbconnect(server, port, userid, password);

        }
        public DataSet get_login_type(string username)
        {
            DataSet ds = new DataSet();
            try
            {

                MySqlDataAdapter msdp = new MySqlDataAdapter();
                DataTable dt = new DataTable();
                string query = "SELECT * FROM " + entitylayer.schema + "." + entitylayer.login_table + " where name = '" + username + "';";
                dbconnect();
                MySqlCommand cmd = new MySqlCommand(query, conn);

                msdp.SelectCommand = cmd;
                msdp.Fill(ds, "login");
                dt = ds.Tables["login"];

            }
            catch (Exception e)
            {

                return null;
            }
            return ds;

        }
        public string insert_login(string name, string password, string type)
        {
            string message = dbconnect();

            try
            {
                string query = "INSERT INTO `" + entitylayer.schema + "`.`" + entitylayer.login_table + "` (`name`, `password`, `type`) VALUES ('" + name + "', '" + password + "','" + type + "');";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader reader;
                cmd.ExecuteReader();

                message = "member saved in login";
            }
            catch (Exception e)
            {

                message = "Didn't save";
            }

            return message;
        }
        public bool delete_login(string username)
        {

            dbconnect();
            Boolean f = true;
            try
            {
                string query = "DELETE FROM `" + entitylayer.schema + "`.`" + entitylayer.login_table + "` WHERE `name`='" + username + "';";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader reader;
                reader = cmd.ExecuteReader();

                if (reader.RecordsAffected == 1)
                {
                    f = true;
                }
                else if (reader.RecordsAffected == 0)
                {
                    f = false;
                }
            }
            catch (Exception e)
            {

                f = false;
            }
            return f;
        }
        public DataSet get_profile(string username)
        {
            try
            {
                DataSet ds = new DataSet();
                MySqlDataAdapter msdp = new MySqlDataAdapter();
                DataTable dt = new DataTable();
                string query = "SELECT * FROM " + entitylayer.schema + "." + entitylayer.member_table + " where name = '" + username + "';";
                dbconnect();
                MySqlCommand cmd = new MySqlCommand(query, conn);

                msdp.SelectCommand = cmd;
                msdp.Fill(ds, "member");
                dt = ds.Tables["member"];

                return ds;

            }
            catch (Exception e)
            {

                return null;
            }

        }
        public Boolean save_edit_profile(string full_name, string designation, string contact, string email, string address, string room_no, string user)
        {
            dbconnect();
            Boolean f = false;
            try
            {
                string query = "UPDATE `" + entitylayer.schema + "`.`" + entitylayer.member_table + "` SET `full_name`='" + full_name + "',`designation`='" + designation + "', `contact_no`='" + contact + "', `email`='" + email + "', `address`='" + address + "', `room_no`='" + room_no + "' WHERE `name`='" + user + "';";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader reader;
                reader = cmd.ExecuteReader();

                if (reader.RecordsAffected == 1)
                {
                    notification_queue.Enqueue("Changed successfully!");
                    f = true;
                }
                else if (reader.RecordsAffected == 0)
                {
                    notification_queue.Enqueue("Cannot change. Try again please.");
                }
            }
            catch (Exception e)
            {


                notification_queue.Enqueue("Cannot change. Try again please.");
            }
            return f;
            
        }

        //Member 

        public string create_member_table()
        {
            string message = dbconnect();
            if (message == "Can't connect")
            {
                return message;
            }
            else
            {
                try
                {
                    string query = "CREATE TABLE `" + entitylayer.schema + "`.`" + entitylayer.member_table + "` (`name` VARCHAR(40) NOT NULL,`full_name` VARCHAR(45) NULL,`designation` VARCHAR(45) NULL,`contact_no` VARCHAR(45) NULL,`email` VARCHAR(45) NULL, `address` VARCHAR(45) NULL,`type` VARCHAR(45) NULL, `room_no` VARCHAR(45) NULL, `temp_password` VARCHAR(45) NULL,PRIMARY KEY (`name`));";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataReader reader;
                    cmd.ExecuteReader();
                    message = "member created";
                }
                catch (Exception e)
                {
                    message = "member can't created";
                }
                return message;
            }


        }
        public Boolean add_member(string name, string full_name, string designation, string phone, string email, string room, string address, string temp_password, string type)
        {
            Boolean b=false;
            string message = dbconnect();
            try
            {
                string query = "INSERT INTO `" + entitylayer.schema + "`.`" + entitylayer.member_table + "` (`name`,`full_name`, `designation`, `contact_no`, `email`, `address`, `type`, `room_no`, `temp_password`) VALUES ('" + name + "','" + full_name + "', '" + designation + "', '" + phone + "', '" + email + "', '" + address + "', '" + type + "', '" + room + "', '" + temp_password + "'); ";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader reader;
                reader = cmd.ExecuteReader();
                //return reader.RecordsAffected.ToString();
                if (reader.RecordsAffected == 1)
                {
                    b = true;
                    notification_queue.Enqueue( "Member added successfully");
                    insert_login(name, temp_password, type);
                }
                else if (reader.RecordsAffected == 0)
                {
                    b = false;
                    notification_queue.Enqueue("Same user name exist");
                }
            }
            catch (Exception e)
            {
                b = false;
                notification_queue.Enqueue("Same user name exist");
            }



            return b;
        }
        public DataSet view_memberinfo(string username)
        {
            DataSet ds = new DataSet();
            try
            {

                MySqlDataAdapter msdp = new MySqlDataAdapter();
                DataTable dt = new DataTable();
                string query = "SELECT * FROM " + entitylayer.schema + "." + entitylayer.member_table + " where name = '" + username + "';";
                dbconnect();
                MySqlCommand cmd = new MySqlCommand(query, conn);

                msdp.SelectCommand = cmd;
                msdp.Fill(ds, "member");
                dt = ds.Tables["member"];

            }
            catch (Exception e)
            {

                return null;
            }
            return ds;

        }
        public Boolean save_edit_member(string full_name, string designation, string contact, string email, string address, string room_no, string user)
        {
            dbconnect();
            Boolean f = true;
            try
            {
                string query = "UPDATE `" + entitylayer.schema + "`.`" + entitylayer.member_table + "` SET `full_name`='" + full_name + "',`designation`='" + designation + "', `contact_no`='" + contact + "', `email`='" + email + "', `address`='" + address + "', `room_no`='" + room_no + "' WHERE `name`='" + user + "';";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader reader;
                reader = cmd.ExecuteReader();

                if (reader.RecordsAffected == 1)
                {
                    f = true;
                    notification_queue.Enqueue("Changed successfully!");
                    insert_notification("Your profile information has been updated", user, DateTime.Now.TimeOfDay.ToString());
                }
                else if (reader.RecordsAffected == 0)
                {
                    f = false;

                    notification_queue.Enqueue("Cannot change. Try again please.");
                }
            }
            catch (Exception e)
            {

                notification_queue.Enqueue("Cannot change. Try again please.");
                f = false;
            }
            return f;
        }
        public bool delete_member(string username)
        {
            delete_login(username);

            dbconnect();
            Boolean f = true;
            try
            {
                string query = "DELETE FROM `" + entitylayer.schema + "`.`" + entitylayer.member_table + "` WHERE `name`='" + username + "';";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader reader;
                reader = cmd.ExecuteReader();

                if (reader.RecordsAffected == 1)
                {
                    f = true;
                    notification_queue.Enqueue("Deleted successfully!");
                }
                else if (reader.RecordsAffected == 0)
                {
                    f = false;

                    notification_queue.Enqueue("Cannot Deleted. Try again please.");
                }
            }
            catch (Exception e)
            {

                notification_queue.Enqueue("Cannot Deleted. Try again please.");

                f = false;
            }
            return f;
        }
        public long count_member()
        {
            dbconnect();
            string query = "SELECT count(*) FROM " + entitylayer.schema + "." + entitylayer.member_table + ";";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            long t = (long)cmd.ExecuteScalar();
            return t;
        }
        public DataSet view_member()
        {

            try
            {
                DataSet ds = new DataSet();
                MySqlDataAdapter msdp = new MySqlDataAdapter();
                DataTable dt = new DataTable();
                string query = "SELECT * FROM " + entitylayer.schema + "." + entitylayer.member_table + " ;";
                dbconnect();
                MySqlCommand cmd = new MySqlCommand(query, conn);

                msdp.SelectCommand = cmd;
                msdp.Fill(ds, "member");
                dt = ds.Tables["member"];

                return ds;

            }
            catch (Exception e)
            {

                return null;
            }

        }



        // Task database layer

        public string create_task_info()
        {
            string message = dbconnect();
            if (message == "Can't connect")
            {
                return message;
            }
            else
            {
                try
                {
                    string query = "CREATE TABLE `" + entitylayer.schema + "`.`" + entitylayer.task_table + "` (  `id` INT NOT NULL auto_increment ,  `title` VARCHAR(45) NULL,  `description` VARCHAR(45) NULL,  `given_date` VARCHAR(45) NULL,  `submission_date` VARCHAR(45) NULL,  `status` VARCHAR(45) NULL,  `assign_to` VARCHAR(45) NULL,  `folder` VARCHAR(45) NULL, `show` VARCHAR(1) NOT NULL DEFAULT 'Y', PRIMARY KEY (`id`));";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataReader reader;
                    cmd.ExecuteReader();
                    message = "Task table created";
                }
                catch (Exception e)
                {
                    message = "Task table can't created";
                }
                return message;
            }
        }
        public long count_task_info()
        {
            dbconnect();
            string query = "SELECT count(*) FROM " + entitylayer.schema + "." + entitylayer.task_table + ";";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            long t = (long)cmd.ExecuteScalar();
            return t;
        }
        public Boolean insert_task_info(string title, string description, string given_date, string submission_date, string assigned_members, string status, string folder)
        {
            Boolean f = false;
            string message = dbconnect();
            try
            {

                string query = "INSERT INTO `" + entitylayer.schema + "`.`" + entitylayer.task_table + "` (`title`, `description`, `given_date`, `submission_date`, `status`, `assign_to`, `folder`) VALUES ('" + title + "', '" + description + "', '" + given_date + "', '" + submission_date + "', '" + status + "', '" + assigned_members + "', '" + folder + "');";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader reader;
                reader = cmd.ExecuteReader();
                //return reader.RecordsAffected.ToString();
                if (reader.RecordsAffected == 1)
                {
                    message = assigned_members + " have been assigned to '" + title + "' by you.";
                    f = true;

                }
                else if (reader.RecordsAffected == 0)
                {
                    message = "Task could not been assigned.";
                    f = false;
                }
            }
            catch (Exception e)
            {
                message = "Task could not been assigned.";
                
            }

            notification_queue.Enqueue(message);

            return f;
        }
        public DataSet select_task_info(int id)
        {
            DataSet ds = new DataSet();
            MySqlDataAdapter msdp = new MySqlDataAdapter();
            DataTable dt = new DataTable();
            string query = "SELECT * FROM " + entitylayer.schema + "." + entitylayer.task_table + " where id=" + id + ";";
            dbconnect();
            MySqlCommand cmd = new MySqlCommand(query, conn);

            msdp.SelectCommand = cmd;
            msdp.Fill(ds, "task_info");
            dt = ds.Tables["task_info"];

            return ds;
        }
        public DataSet tasks_info()
        {
            int num = 1;

            DataSet ds = new DataSet();
            MySqlDataAdapter msdp = new MySqlDataAdapter();
            DataTable dt = new DataTable();
            string query = "SELECT * FROM " + entitylayer.schema + "." + entitylayer.task_table + ";";
            dbconnect();
            MySqlCommand cmd = new MySqlCommand(query, conn);

            msdp.SelectCommand = cmd;
            msdp.Fill(ds, "task_info");
            dt = ds.Tables["task_info"];

            return ds;
        }
        public DataSet task_info_by_name(string name)
        {
            dbconnect();
            DataSet ds = new DataSet();
            MySqlDataAdapter msdp = new MySqlDataAdapter();
            DataTable dt = new DataTable();
            string query = "SELECT * FROM " + entitylayer.schema + "." + entitylayer.task_table + " where assign_to = " + name + ";";

            MySqlCommand cmd = new MySqlCommand(query, conn);

            msdp.SelectCommand = cmd;
            msdp.Fill(ds, "task_info");
            dt = ds.Tables["task_info"];

            return ds;
        }
        public Boolean task_delete(int id)
        {
            dbconnect();
            Boolean f = true;
            try
            {
                string query = "DELETE FROM `" + entitylayer.schema + "`.`" + entitylayer.task_table + "` WHERE `id`='" + id + "';";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader reader;
                reader = cmd.ExecuteReader();

                if (reader.RecordsAffected == 1)
                {
                    notification_queue.Enqueue("Task deleted successfully.");
                    f = true;
                }
                else if (reader.RecordsAffected == 0)
                {
                    notification_queue.Enqueue("Error");
                    f = false;
                }
            }
            catch (Exception e)
            {
                notification_queue.Enqueue("Error");
                f = false;
            }
            return f;
        }
        public Boolean task_complete(int id, string status)
        {
            dbconnect();
            Boolean f = true;
            try
            {
                string query = "update `" + entitylayer.schema + "`.`" + entitylayer.task_table + "` set `status` = '" + status + "' where `id` = '" + id + "';";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader reader;
                reader = cmd.ExecuteReader();

                if (reader.RecordsAffected == 1)
                {
                    f = true;
                    notification_queue.Enqueue("Task status has been updated successfully.");
                    
                }
                else if (reader.RecordsAffected == 0)
                {
                    notification_queue.Enqueue("Task status could not been updated.");
                    f = false;
                }
            }
            catch (Exception e)
            {
                notification_queue.Enqueue("Task status could not been updated.");

                f = false;
            }
            return f;
        }
        public Boolean edit_task_info_date(int id, string lastdate)
        {
            dbconnect();
            Boolean f = true;
            try
            {
                string query = "UPDATE `" + entitylayer.schema + "`.`" + entitylayer.task_table + "` SET `submission_date`='" + lastdate + "' WHERE `id`='" + id + "';";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader reader;
                reader = cmd.ExecuteReader();

                if (reader.RecordsAffected == 1)
                {
                    f = true;
                    notification_queue.Enqueue("Task Submission date has been updated successfully.");

                }
                else if (reader.RecordsAffected == 0)
                {
                    notification_queue.Enqueue("Task Submission date could not been updated.");
                    f = false;
                }
            }
            catch (Exception e)
            {
                notification_queue.Enqueue("Task Submission date could not been updated.");
                f = false;
            }
            return f;
        }
        public Boolean edit_task_info_description(int id, string description)
        {
            dbconnect();
            Boolean f = true;
            try
            {
                string query = "UPDATE `" + entitylayer.schema + "`.`" + entitylayer.task_table + "` SET `description`='" + description + "' WHERE `id`='" + id + "';";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader reader;
                reader = cmd.ExecuteReader();

                if (reader.RecordsAffected == 1)
                {
                    f = true;
                    notification_queue.Enqueue("Task description has been updated successfully.");

                }
                else if (reader.RecordsAffected == 0)
                {
                    notification_queue.Enqueue("Task description date could not been updated.");
                    f = false;
                }
            }
            catch (Exception e)
            {
                notification_queue.Enqueue("Task description date could not been updated.");
                f = false;
            }
            return f;
        }

        //Notification

        public string create_notification()
        {

            string message = dbconnect();
            if (message == "Can't connect")
            {
                return message;
            }
            else
            {
                try
                {
                    string query = "CREATE TABLE `" + entitylayer.schema + "`.`notification` (  `id` INT NOT NULL AUTO_INCREMENT,  `description` VARCHAR(500) NULL,  `member` VARCHAR(45) NULL,  `time` VARCHAR(45) NULL, `has_shown` INT DEFAULT 1,  PRIMARY KEY (`id`));";
                    //string query = "CREATE TABLE `" + entitylayer.schema + "`.`notification` (  `id` INT NOT NULL AUTO_INCREMENT,  `description` VARCHAR(45) NULL,  `member` VARCHAR(45) NULL, `time` VARCHAR(45) NULL , `show` VARCHAR(1) NOT NULL DEFAULT 'Y' , PRIMARY KEY(`id`));";
                    MySqlCommand cmd;
                    cmd = new MySqlCommand(query, conn);
                    MySqlDataReader reader;
                    cmd.ExecuteReader();
                    message = "Notification table created";
                }
                catch (Exception e)
                {
                    message = "Notification table can't created";
                }
                return message;
            }
            return message;

        }
        public Boolean insert_notification(string description, string member, string time)
        {
            Boolean f = false;
            string message = dbconnect();
            try
            {

                string query = "INSERT INTO `" + entitylayer.schema + "`.`notification` (`description`, `member`, `time`) VALUES ('" + description + "', '" + member + "', '" + time + "');";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader reader;
                reader = cmd.ExecuteReader();
                //return reader.RecordsAffected.ToString();
                if (reader.RecordsAffected == 1)
                {
                    f = true;
                }
                else if (reader.RecordsAffected == 0)
                {
                    f = false;
                }
            }
            catch (Exception e)
            {
                
            }



            return f;
        }
        public Boolean notificationhasshown(string id)
        {
            dbconnect();
            Boolean f = true;
            try
            {
                string query = "UPDATE `" + entitylayer.schema + "`.`notification` SET `has_shown`='0' WHERE `id`='" + id + "';";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader reader;
                reader = cmd.ExecuteReader();

                if (reader.RecordsAffected == 1)
                {
                    f = true;
                }
                else if (reader.RecordsAffected == 0)
                {
                    f = false;
                }
            }
            catch (Exception e)
            {

                f = false;
            }
            return f;
        }
        public Boolean notification_delete(string id)
        {
            dbconnect();
            Boolean f = true;
            try
            {
                string query = "DELETE FROM `" + entitylayer.schema + "`.`notification` WHERE `id`='" + id + "';";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader reader;
                reader = cmd.ExecuteReader();

                if (reader.RecordsAffected == 1)
                {
                    f = true;
                }
                else if (reader.RecordsAffected == 0)
                {
                    f = false;
                }
            }
            catch (Exception e)
            {

                f = false;
            }
            return f;
        }
        public DataSet notification_by_name(string name)
        {
            DataSet ds = new DataSet();
            MySqlDataAdapter msdp = new MySqlDataAdapter();
            DataTable dt = new DataTable();
            try
            {
                string query = "SELECT * FROM `" + entitylayer.schema + "`.`notification` where member ='" + name + "';";
                dbconnect();
                MySqlCommand cmd = new MySqlCommand(query, conn);

                msdp.SelectCommand = cmd;
                msdp.Fill(ds, "notification");
                dt = ds.Tables["notification"];
            }
            catch (Exception)
            {

            }

            return ds;
        }
        public Boolean unseen_notification_by_name(string name)
        {
            Boolean f = false;

            DataSet ds = new DataSet();
            MySqlDataAdapter msdp = new MySqlDataAdapter();
            DataTable dt = new DataTable();
            string query = "SELECT * FROM `" + entitylayer.schema + "`.`notification` where member ='" + name + "' AND has_shown = 1;";
            dbconnect();
            MySqlCommand cmd = new MySqlCommand(query, conn);

            msdp.SelectCommand = cmd;
            msdp.Fill(ds, "notification");
            dt = ds.Tables["notification"];
            int size = dt.Rows.Count;



            //notification_queue.Enqueue(size.ToString());
            
            if (size>0)
            {
                f = true;
                for (int i = size - 1; i >= 0; i--)
                {
                    string s = dt.Rows[i]["description"].ToString() + "   -    " + dt.Rows[i]["time"];
                    notification_queue.Enqueue(s);
                    notificationhasshown(dt.Rows[i]["id"].ToString());
                }
            }
            
            return f;
        }


        
        

    }


}
