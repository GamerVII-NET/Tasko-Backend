namespace Tasko.Domains.Models.Structural.Interfaces
{
    public interface ICountry
    {
        Guid Id { get; set; }
        string Name { get; set; }
        short PhoneCode { get; set; }
        string Alpha2 { get; set; }
        string Alpha3 { get; set; }
        short ISO { get; set; }
    }
}
