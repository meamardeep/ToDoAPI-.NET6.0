using POC.DataModel;

namespace POC.BusinessLogic.Interfaces
{
    public interface ITaskManagement
    {
        public List<TaskModel> GetTasks(int userId);

        public TaskModel GetTask(int taskId);

        public bool SaveTask(TaskModel model);

        public bool DeleteTask(int taskId);

        public bool CancelTask(int taskId);
    }
}
