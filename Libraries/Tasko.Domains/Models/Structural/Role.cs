﻿namespace Tasko.Domains.Models.Structural;
public interface IRole
{
    Guid Id { get; set; }
    string Name { get; set; }
    string Description { get; set; }
    bool IsDeleted { get; set; }
    List<Guid> PermissionGuids { get; set; }
    DateTime CreatedAt { get; set; }
    DateTime UpdatedAt { get; set; }
    DateTime DeletedAt { get; set; }
}

public class Role : IRole
{
    [BsonId]
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsDeleted { get; set; }
    public List<Guid> PermissionGuids { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime DeletedAt { get; set; }
}