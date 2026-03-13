using System.Collections.Generic;

namespace employees_mangment.Models
{
    public class employees
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public virtual ICollection<EmployeePropertyValue> PropertyValues { get; set; }

        public employees()
        {
            PropertyValues = new HashSet<EmployeePropertyValue>();
        }
    }
}