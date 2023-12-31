﻿using HR.LeaveManagement.Application.DTOs.LeaveType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.DTOs.LeaveAllocation
{
    public interface ILeaveAllocationDTO
    {
        public int NumberOfDays { get; set; }
        public LeaveTypeDTO? LeaveType { get; set; }
        public int Period { get; set; }
    }
}
