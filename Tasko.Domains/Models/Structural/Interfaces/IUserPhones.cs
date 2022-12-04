namespace Tasko.Domains.Models.Structural.Interfaces
{
    public interface IUserPhone
    {
        Guid Id { get; set; }
        Guid? CountryId { get; set; }
        string Number { get; set; }
        string FullNumber { get; set; }
    }
}
