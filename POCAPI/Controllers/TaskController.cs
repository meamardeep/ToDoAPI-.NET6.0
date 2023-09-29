using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using POC.BusinessLogic;
using POC.BusinessLogic.Interfaces;
using POC.DataModel;

namespace ToDoAPI.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskManagement _taskManagement;
        //private readonly IConfiguration _configuration;
 
        public TaskController( ITaskManagement taskManagement)
        {
            //_configuration = configuration;
            _taskManagement = taskManagement;
        }

        [HttpGet]
        [Route("api/gettasks/{userId}")]
        public ActionResult GetTasks(string userId)
        {
            List<TaskModel> tasks = new List<TaskModel>();
            tasks = _taskManagement.GetTasks(Convert.ToInt32(userId));
            return  Ok(tasks);
        }

        [HttpGet]
        [Route("api/gettask/{taskId}")]
        public ActionResult GetTask(int taskId)
        {
            TaskModel tasks = new TaskModel();
            tasks = _taskManagement.GetTask(taskId);
            return Ok(tasks);
        }

        [HttpPost]
        [Route("api/savetask")]
        public ActionResult SaveTask(TaskModel model)
        {
            bool tasks = false;
            tasks = _taskManagement.SaveTask(model);
            return Ok(tasks);
        }

        [HttpDelete]
        [Route("api/deletetask")]
        public ActionResult DeleteTask(int taskId)
        {
            bool tasks = false;
            tasks = _taskManagement.DeleteTask(taskId);
            return Ok(tasks);
        }

        [HttpDelete]
        [Route("api/canceltask")]
        public ActionResult CancelTask(int taskId)
        {
            bool tasks = false;
            tasks = _taskManagement.CancelTask(taskId);
            return Ok(tasks);
        }
    }
}
