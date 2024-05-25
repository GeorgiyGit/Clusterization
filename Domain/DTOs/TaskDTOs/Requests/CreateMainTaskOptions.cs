﻿using Domain.Entities.Customers;
using Domain.Entities.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.TaskDTOs.Requests
{
    public class CreateMainTaskOptions : CreateBaseTaskOptions
    {
        public bool IsGroupTask { get; set; }
    }
}
