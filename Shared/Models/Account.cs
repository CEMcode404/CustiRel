
namespace crm.Shared.Models
{
    public class Account(string name, string industry, string type, string country, string city)
    {
        public System.Guid Id { get; init; } = Guid.NewGuid();
        public string Name { get; set; } = name;
        public string Industry { get; set; } = industry;
        public string Type { get; set; } = type;
        public string Country { get; set; } = country;
        public string City { get; set; } = city;
    }
}