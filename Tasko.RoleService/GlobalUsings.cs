#region Microsoft
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.OpenApi.Models;
#endregion

#region Tasko.Domains
global using Tasko.Domains.Models.Structural.Providers;
global using Tasko.Domains.Models.DTO.Role;
global using Tasko.Domains.Models.DTO.User;
#endregion

#region Tasko.General
global using Tasko.General.Commands;
global using Tasko.General.Extensions;
global using Tasko.General.Models;
global using Tasko.General.Interfaces;
global using Tasko.General.Abstracts;
global using Tasko.General.Models.RequestResponses;
global using Tasko.General.Extensions.Jwt;
global using Tasko.General.Validations;
#endregion

global using System.Reflection;
global using System.Runtime.InteropServices;
global using MongoDB.Driver;
global using AutoMapper;
global using FluentValidation.AspNetCore;
global using FluentValidation;
global using FluentValidation.Results;