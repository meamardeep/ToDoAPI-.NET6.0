using Castle.Core.Configuration;
using Moq;
using POC.BusinessLogic.Interfaces;
using POC.DataModel;
using ToDoAPI.Controllers;

namespace POCAPI.Tests
{
    [TestClass]
    public class TaskControllerTests
    {
        private readonly TaskController _controller;
        private readonly Mock<IConfiguration> _configuration = new Mock<IConfiguration>();
        private readonly Mock<ITaskManagement> _taskManagementMock = new Mock<ITaskManagement>();
        public TaskControllerTests() 
        {
            _controller = new TaskController(_taskManagementMock.Object);
          
        }  
        
        [TestMethod]
        public void GetTask_Should_Return_Task_When_TaskId_Not_Null()
        {
            //Arrange
            int taskId = 1;
            TaskModel taskModel = new TaskModel()
            {
                TaskId = taskId,
                Title = "Title",
                Description = "Description",
                DueDate = "",
                AssignTo = 1,
                CreatedBy = 1,
                CreatedByName = "Amar",
                AssignToName = "Name",
                Priority = "High",
            };

            //Act
            _taskManagementMock.Setup(x => x.GetTask(taskId)).Returns(taskModel);           
            var task = _controller.GetTask(taskId);

            //Assert
            Assert.IsNotNull(task);
            
        }

        [TestMethod]
        public void GetTask_Should_Throw_Exceptio_When_TaskId_Not_Valid() { }

        [TestMethod]
        public void GetTaskList_With_UserId() { }

        [TestMethod]
        public void UpdateTask_With_TaskId() { }

        [TestMethod]
        public void DeleteTask_With_TaskId() { }

    }
}