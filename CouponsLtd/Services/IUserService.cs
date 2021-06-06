using CouponsLtd.Data.Entities;
using CouponsLtd.UpsertModels;
using CouponsLtd.ViewModels;
using System;
using System.Threading.Tasks;

namespace CouponsLtd.Services
{
    public interface IUserService
    {
        Task<AuthenticateResponse> Authenticate(AuthenticateRequest model);
        Task<UserDAO> Create(UserUpsert user);
        UserDAO GetById(Guid id);
    }
}
