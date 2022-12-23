﻿using AutoMapper;
using FluentValidation;
using Tasko.AuthService.Infrastructure.Repositories;
using Tasko.Domains.Models.DTO.User;
using Tasko.General.Models;

namespace Tasko.AuthService.Infrastructure.Functions
{
    public class AuthFunctions
    {
        public static Func<IAuthRepository, UserAuth, IMapper, IValidator<IUserAuth>, Task<IResult>> BearerAuthorization(JwtValidationParameter jwtValidationParmeter)
        {
            return async (IAuthRepository authRepository, UserAuth userAuth, IMapper mapper, IValidator<IUserAuth> validator) =>
                   await authRepository.AuthorizationAsync(userAuth, jwtValidationParmeter, mapper, validator);
        }
    }
}
