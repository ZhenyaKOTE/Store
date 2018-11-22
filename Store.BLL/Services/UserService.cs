using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Store.BLL.DTO;
using Store.BLL.Infrastructure;
using Store.BLL.Interfaces;
using Store.DAL.Entities;
using Store.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Store.BLL.Services
{
    public class UserService : IUserService
    {
        private IUnitOfWork DBContext;

        public UserService(IUnitOfWork UOW)
        {
            DBContext = UOW;
        }

        public async Task<ClaimsIdentity> Authenticate(UserDTO userDto)
        {
            ClaimsIdentity claimIdentity = null;
            
            // находим пользователя
            ApplicationUser user = await DBContext.UserManager.FindAsync(userDto.Email, userDto.Password);
            
            // авторизуем его и возвращаем объект ClaimsIdentity
            if (user != null)
            {
                claimIdentity = await DBContext.UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
                Claim UserNameClaim = new Claim("UserName", user.ClientProfile.Name);
                foreach (string role in await GetRoles(user.Id))
                {
                    Claim RolesClaim = new Claim("Roles", role);
                }
                
                claimIdentity.AddClaim(UserNameClaim);
            }
            return claimIdentity;
        }

        //public async Task<OperationDetails> CreateRoleOrDefault()

        public async Task<OperationDetails> CreateAsync(UserDTO userDto)
        {
            ApplicationUser user = await DBContext.UserManager.FindByEmailAsync(userDto.Email);
            if (user == null)
            {
                user = new ApplicationUser { Email = userDto.Email, UserName = userDto.Email };
                var Result = await DBContext.UserManager.CreateAsync(user, userDto.Password);

                if (Result.Errors.Count() > 0)
                    return new OperationDetails(false, Result.Errors.FirstOrDefault(), "");

                foreach (string role in userDto.Roles)
                {
                    if (await DBContext.RoleManager.RoleExistsAsync(role) == false) //Якщо ролі не існує взагалі, то створити
                        await DBContext.RoleManager.CreateAsync(new ApplicationRole { Name = role });

                    await DBContext.UserManager.AddToRoleAsync(user.Id, role);
                }

                ClientProfile clientProfile = new ClientProfile { Id = user.Id, Name = userDto.Name }; // создаем профиль клиента
                DBContext.ClientManager.Create(clientProfile);

                await DBContext.SaveAsync();
                return new OperationDetails(true, "Регистрация успешно пройдена", "");
            }
            else
                return new OperationDetails(false, "Пользователь с таким Email уже существует", "Email");
        }

        public async Task SetInitialData(UserDTO adminDto, List<string> roles)
        {
            foreach (string roleName in roles)
            {
                var role = await DBContext.RoleManager.FindByNameAsync(roleName);
                if (role == null)
                {
                    role = new ApplicationRole { Name = roleName };
                    await DBContext.RoleManager.CreateAsync(role);
                }
            }
            await CreateAsync(adminDto);
        }

        public void Dispose()
        {
            DBContext.Dispose();
        }

        public async Task<string[]> GetRoles(string id)
        {
            return await Task.Run(() => { return DBContext.UserManager.GetRoles(id).ToArray(); });
        }


    }
}
