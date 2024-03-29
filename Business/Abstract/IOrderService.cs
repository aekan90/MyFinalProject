﻿using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IOrderService
    {
        List<Order> GetAll();
        List<Order> GetAllByEmployeeId(int id);
        List<Order> GetByDate(DateTime startDate, DateTime endDate);
    }
}
