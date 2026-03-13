namespace employees_mangment.Models
{
    public class PropertyDefinition
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public bool IsRequired { get; set; }
        public string DropdownOptions { get; set; }
    }
}