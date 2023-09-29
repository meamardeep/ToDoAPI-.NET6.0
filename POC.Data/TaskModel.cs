using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POC.DataModel
{
    public class TaskModel
    {
        public int TaskId { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public string? DueDate { get; set; }

        public string? Priority { get; set;}

        public int AssignTo { get; set; }

        public string? AssignToName { get; set; }

        public int CreatedBy { get; set; }

        public string? CreatedByName { get; set; }

    }
}
