using Microsoft.VisualBasic;
using POC.BusinessLogic.Interfaces;
using POC.DataAccess;
using POC.DataModel;
using POC.Repositories;

namespace POC.BusinessLogic
{
    public class TaskManagement : ITaskManagement
    {
        private readonly ITaskRepository _taskRepository;
        //private readonly IConfiguration _configuration;

        public TaskManagement(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public bool DeleteTask(int taskId)
        {
            return _taskRepository.DeleteTask(taskId);
        }

        public bool CancelTask(int taskId)
        {
            return _taskRepository.CancelTask(taskId);
        }

        public TaskModel GetTask(int taskId)
        {
            TaskModel taskModel = new TaskModel();
            TaskDataAccess task = new TaskDataAccess();
            task = _taskRepository.GetTask(taskId);
            taskModel.TaskId = task.TaskId;
            taskModel.Title = task.Title;
            taskModel.Description = task.Description;
            taskModel.DueDate = task.DueDate;
            taskModel.Priority = task.Priority;
            taskModel.AssignTo = task.AssignTo;
            //taskModel.AssignToName = task.AssignToName;
            return taskModel;
        }

        public List<TaskModel> GetTasks(int userId)
        {
            List<TaskDataAccess> list = new List<TaskDataAccess>();
            List<TaskModel> result = new List<TaskModel>();

            list = _taskRepository.GetTasks(userId);
            foreach (var taskDataAccess in list) 
            {
                result.Add(new TaskModel()
                {
                    TaskId = taskDataAccess.TaskId,
                    Title = taskDataAccess.Title,
                    Description = taskDataAccess.Description,
                    DueDate = taskDataAccess.DueDate.Split(" ")[0],
                    Priority = taskDataAccess.Priority,
                    AssignTo = taskDataAccess.AssignTo,
                    AssignToName = taskDataAccess.AssignToName,
                    CreatedBy = taskDataAccess.CreatedBy,
                    CreatedByName = taskDataAccess.CreatedByName,
                });
            }
            return result;
        }

        public bool SaveTask(TaskModel model)
        {
            if (model != null)
            {
                TaskDataAccess task = new TaskDataAccess()
                {
                    TaskId = model.TaskId, 
                    Title = model.Title,
                    Description = model.Description,
                    DueDate = model.DueDate,
                    Priority = model.Priority,
                    AssignTo = model.AssignTo,
                    CreatedBy= model.CreatedBy,
                };
                _taskRepository.SaveTask(task);
                return true;
            }
            return false;
        }
    }
}
