using employees_mangment.Models;

namespace employees_mangment.Models
{
    public class EmployeePropertyValue
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public virtual employees Employee { get; set; }
        public int PropertyDefinitionId { get; set; }
        public virtual PropertyDefinition PropertyDefinition { get; set; }
        public string Value { get; set; }
    }
}