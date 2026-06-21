using System.Reflection.PortableExecutable;

namespace Talabat.Core.Entities
{
    public class Department :BaseEntity
    {
        public string Name { get; set; }

        public DateOnly DateOfCreation  { get; set; }


        //public ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();


    }
}