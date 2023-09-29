using POC.DataAccess;

namespace POC.Repositories
{
    public interface ITaskRepository
    {
        public List<TaskDataAccess> GetTasks(int userId);

        public TaskDataAccess GetTask(int taskId);

        public bool SaveTask(TaskDataAccess task);

        public bool DeleteTask(int taskId);

        public bool CancelTask(int taskId);

    }
}
