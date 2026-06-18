

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
 
namespace CybersecurityChatbot
{
    internal class DatabaseHelper
    {//start of class

        string connection = @"Data Source=(localdb)\task_demo;Database=prog_tasks;Integrated Security=True;";


        // adds a new task to the database
        public void AddTask(string name, string desc, string dueDate, string status)
        {//start of AddTask

            using (SqlConnection connect = new SqlConnection(connection))
            {
                connect.Open();

                string query = "INSERT INTO demo_tasks (task_name, task_description, task_dueDate, task_status) " +
                               "VALUES (@name, @desc, @due, @status)";

                SqlCommand cmd = new SqlCommand(query, connect);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@desc", desc);
                cmd.Parameters.AddWithValue("@due", dueDate);
                cmd.Parameters.AddWithValue("@status", status);

                cmd.ExecuteNonQuery();
            }

        }//end of AddTask


        public void LoadTasks(ListView list)
        {//start of LoadTasks

            list.Items.Clear();

            using (SqlConnection connect = new SqlConnection(connection))
            {
                connect.Open();

                string query = "SELECT task_id, task_name, task_description, task_dueDate, task_status FROM demo_tasks";

                SqlCommand cmd = new SqlCommand(query, connect);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    // build a display string for each row
                    string row = "#" + reader[0] + " | " + reader[1] + " | Due: " + reader[3] + " | " + reader[4];
                    list.Items.Add(row);
                }
            }

        }//end of LoadTasks


        // returns all tasks 
        public List<TaskItem> GetAllTasks()
        {//start of GetAllTasks

            List<TaskItem> tasks = new List<TaskItem>();

            using (SqlConnection connect = new SqlConnection(connection))
            {
                connect.Open();

                string query = "SELECT task_id, task_name, task_description, task_dueDate, task_status FROM demo_tasks";


               // string cmd = "SELECT task_id, task_name, task_description, task_dueDate, task_status FROM demo_tasks";
                SqlCommand cmd = new SqlCommand(query, connect);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    TaskItem t = new TaskItem();
                    t.Id = reader.GetInt32(0);
                    t.Name = reader.GetString(1);
                    t.Desc = reader.GetString(2);
                    t.Due = reader.GetString(3);
                    t.Status = reader.GetString(4);

                    tasks.Add(t);
                }
            }

            return tasks;

        }//end of GetAllTasks

       // private void UpdateTaskStatus(int id, string status)
        //{//start of UpdateTaskStatus
           // using (SqlConnection connect = new SqlConnection(connection))
            //{
              //  connect.Open();
                //string query = "UPDATE demo_tasks SET task_status = @status WHERE task_id = @id";
                //lCommand cmd = new SqlCommand(query, connect);
                //cmd.Parameters.AddWithValue("@status", status);
               // cmd.Parameters.AddWithValue("@id", id);
               // cmd.ExecuteNonQuery();
           // }
       // }//end of UpdateTaskStatus
        public void MarkComplete(int id)
        {//start of MarkComplete

            using (SqlConnection connect = new SqlConnection(connection))
            {
                connect.Open();

                string query = "UPDATE demo_tasks SET task_status = 'completed' WHERE task_id = @id";

                SqlCommand cmd = new SqlCommand(query, connect);
                cmd.Parameters.AddWithValue("@id", id);

                cmd.ExecuteNonQuery();
            }

        }//end of MarkComplete

        //p
        public void DeleteTask(int id)
        {//start of DeleteTask

            using (SqlConnection connect = new SqlConnection(connection))
            {
                connect.Open();

                string query = "DELETE FROM demo_tasks WHERE task_id = @id";

                SqlCommand cmd = new SqlCommand(query, connect);
                cmd.Parameters.AddWithValue("@id", id);

                cmd.ExecuteNonQuery();
            }

        }//end of DeleteTask


    }//end of class


    internal class TaskItem
    {//start of class

        public int Id { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public string Due { get; set; }
        public string Status { get; set; }

    }//end of class

}//end of namespace