using CouponsLtd.Data.Entities;
using CouponsLtd.UpsertModels;
using CouponsLtd.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CouponsLtd.Services
{
    public interface IUserService
    {
        Task<AuthenticateResponse> Authenticate(AuthenticateRequest model);
        Task<bool> CreateUsers(List<UserUpsert> users,bool usePrefilledData);
        UserDAO GetById(Guid id);
    }
}
