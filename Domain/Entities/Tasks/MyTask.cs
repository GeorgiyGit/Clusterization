﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Tasks
{
    public class MyTask
    {
        public int Id { get; set; }

        public DateTime StartTime { get; set; } = DateTime.UtcNow;
        public DateTime? EndTime { get; set; }

        public string Title { get; set; }

        public float Percent { get; set; }

        public MyTaskState State { get; set; }
        public string StateId { get; set; }
    }
}
