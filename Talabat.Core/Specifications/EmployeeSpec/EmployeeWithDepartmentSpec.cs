using Talabat.Core.Entities;

namespace Talabat.Core.Specifications.EmployeeSpec
{
    public class EmployeeWithDepartmentSpec : BaseSpecifications<Employee>
    {
        public EmployeeWithDepartmentSpec() : base()
        {
            Includes.Add(E => E.Department);
        }

        public EmployeeWithDepartmentSpec(int id) : base(e => e.Id == id)
        {
            Includes.Add(E => E.Department);
        }
    }
}
