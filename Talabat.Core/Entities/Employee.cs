using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Talabat.Core.Entities
{
    public  class Employee :BaseEntity
    {

        public string Name { get; set; }
        public decimal Salary  { get; set; }
        public int Age { get; set; }


        //[ForeignKey(nameof(Employee.Department))]
        public int DepartmentId{ get; set; }

        public Department  Department { get; set; }
    }
}
