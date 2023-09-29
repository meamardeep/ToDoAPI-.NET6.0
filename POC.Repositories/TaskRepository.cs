using POC.DataAccess;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace POC.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        //private readonly POCDataContext connectionString;

        //public TaskRepository(IOptions<POCDataContext> dbContext)
        //{
        //    connectionString = dbContext.Value;
        //}

        private string conectionString = "Server=SPARTAN\\SQLSERVER2017;Database=POC_DB;Integrated Security=true";
        public TaskRepository() { }
        public bool DeleteTask(int taskId)
        {
            var result = 0;
            using (var connect = new SqlConnection(conectionString))
            {
                connect.Open();
                SqlCommand cmd = new SqlCommand("DeleteTask", connect);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("TaskId", taskId));
                result = cmd.ExecuteNonQuery();
            }
            if (result > 0)
            {
                return true;
            }
            return false;
        }

        public bool CancelTask(int taskId)
        {
            var result = 0;
            using (var connect = new SqlConnection(conectionString))
            {
                connect.Open();
                SqlCommand cmd = new SqlCommand("CancelTask", connect);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("TaskId", taskId));
                result = cmd.ExecuteNonQuery();
            }
            if (result > 0)
            {
                return true;
            }
            return false;
        }

        public TaskDataAccess GetTask(int taskId)
        {
            string sqlQuery = "select * from Tasks t inner join Users u on t.AssignTo = u.UserId where TaskId ="+ taskId;
            TaskDataAccess taskDataAccess = new TaskDataAccess();
            using (var connect = new SqlConnection(conectionString))
            {
                connect.Open();
                SqlCommand cmd = new SqlCommand(sqlQuery, connect);
                cmd.CommandType = CommandType.Text;
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        taskDataAccess.TaskId = Convert.ToInt32(reader["TaskId"]);
                        taskDataAccess.Title = reader["Title"].ToString();
                        taskDataAccess.Description = reader["TaskDescription"].ToString();
                        taskDataAccess.DueDate = reader["DueDate"].ToString();
                        taskDataAccess.Priority = reader["Priority"].ToString();
                        taskDataAccess.AssignTo = Convert.ToInt32(reader["AssignTo"]);
                        taskDataAccess.AssignToName = reader["FullName"].ToString();
                    }
                };
            }

            return taskDataAccess;
        }

        public List<TaskDataAccess> GetTasks(int userId)
        {
            List<TaskDataAccess> tasks = new List<TaskDataAccess>();
            string sqlQuery = "select t.TaskId, t.AssignTo, t.Title, t.TaskDescription, t.DueDate, " +
                "t.Priority, t.CreatedBy, u.FullName as AssignToName, uu.FullName as CreatedByName " +
                "from Tasks t inner join Users u on t.AssignTo = u.UserId " +
                "inner join Users uu on t.CreatedBy = uu.UserId "+
                "where t.AssignTo = " + userId+" or t.CreatedBy ="+ userId;
            using (var connect = new SqlConnection(conectionString))
            {
                connect.Open();
                SqlCommand cmd = new SqlCommand(sqlQuery, connect);
                cmd.CommandType = CommandType.Text;
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tasks.Add(new TaskDataAccess()
                        {
                            TaskId = Convert.ToInt32(reader["TaskId"]),
                            Title = reader["Title"].ToString(),
                            Description = reader["TaskDescription"].ToString(),
                            DueDate = reader["DueDate"].ToString(),
                            Priority = reader["Priority"].ToString(),
                            AssignTo = Convert.ToInt32(reader["AssignTo"]),
                            AssignToName = reader["AssignToName"].ToString(),
                            CreatedBy = Convert.ToInt32(reader["CreatedBy"]),
                            CreatedByName = reader["CreatedByName"].ToString()

                        });
                    }
                };    
            }

            return tasks;
        }

        public bool SaveTask(TaskDataAccess task)
        {
            var result = 0;
            using (var connect = new SqlConnection(conectionString))
            {
                connect.Open();
                SqlCommand cmd = new SqlCommand("dbo.UpdateTask", connect);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("TaskId", task.TaskId));
                cmd.Parameters.Add(new SqlParameter("Title", task.Title));
                cmd.Parameters.Add(new SqlParameter("AssignTo", task.AssignTo));
                cmd.Parameters.Add(new SqlParameter("TaskDescription", task.Description));
                cmd.Parameters.Add(new SqlParameter("DueDate", task.DueDate));
                cmd.Parameters.Add(new SqlParameter("Priority", task.Priority));
                cmd.Parameters.Add(new SqlParameter("CreatedBy", task.CreatedBy));

                result = cmd.ExecuteNonQuery();
                connect.Close();
            }
            if (result > 0)
                return true;

            return false;
        }
    }
}