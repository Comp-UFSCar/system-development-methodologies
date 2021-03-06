﻿using Gamedalf.Core.Models;
using System;
using System.Collections.Generic;

namespace Gamedalf.Core.Migrations
{
    class EmployeesSeedData
    {
        public ICollection<Employee> Data { get; set; }

        public EmployeesSeedData()
        {
            Data = new List<Employee>
            {
                new Employee 
                {
                    Email    = "lucasolivdavid@gmail.com",
                    UserName = "lucasolivdavid@gmail.com",
                    SSN      = "111-111-1111",
                    DateCreated = DateTime.Now,
                },
                new Employee
                {
                    Email    = "jvbrandaom@gmail.com",
                    UserName = "jvbrandaom@gmail.com",
                    SSN      = "222-222-2222",
                    DateCreated = DateTime.Now,
                },
                new Employee
                {
                    Email    = "daniel.nobusada@gmail.com",
                    UserName = "daniel.nobusada@gmail.com",
                    SSN      = "333-333-3333",
                    DateCreated = DateTime.Now,
                },
                new Employee
                {
                    Email    = "fabiogunkel@gmail.com",
                    UserName = "fabiogunkel@gmail.com",
                    SSN      = "444-444-4444",
                    DateCreated = DateTime.Now,
                }
            };
        }
    }
}
