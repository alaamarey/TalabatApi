using System;
using System.Collections.Generic;
using System.Text;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications.EmployeeSpecification
{
    public class EmployeeWithDepartmentWithSpecification : BaseSpecifications<Employee>
    {

        public EmployeeWithDepartmentWithSpecification() : base()
        {
            ApplyInclude();
        }


        public EmployeeWithDepartmentWithSpecification(int id) : base(E => E.Id == id)
        {
            ApplyInclude();
        }

        private void ApplyInclude() => Includes.Add(E => E.Department);





    }
}
