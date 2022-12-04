﻿namespace Tasko.Domains.Models.DTO.Interfaces
{
    public interface IUserCreate
    {
        string FirstName { get; set; }
        string LastName { get; set; }
        string Patronymic { get; set; }
        string Email { get; set; }
        string Login { get; set; }
        string Password { get; set; }
    }
}
