using System;
using System.Collections.Generic;

namespace aspnetcore3_demo.Entities {
    public class Company {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Introduction { get; set; }
        public ICollection<Employee> Employees { get; set; }
    }
}