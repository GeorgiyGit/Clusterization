using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Tasks
{
    public class MySubTask : MyBaseTask
    {
        public int Position { get; set; }
        public MyMainTask GroupTask { get; set; }
        public string GroupTaskId { get; set; }
    }
}
