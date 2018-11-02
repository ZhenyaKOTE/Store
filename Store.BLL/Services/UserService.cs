using Microsoft.AspNet.Identity;
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

        public async Task<string> GetUserNameByEmail(string Email)
        {
            Debug.Write("Hello" + "Email" + "\n\n\n\n\\n\n\n");
            string UserName = await DBContext.ClientManager.GetUserNameByEmail(Email);
            return UserName;  
        }

        public async Task<ClaimsIdentity> Authenticate(UserDTO userDto)
        {
            ClaimsIdentity claim = null;
            // находим пользователя
            ApplicationUser user = await DBContext.UserManager.FindAsync(userDto.Email, userDto.Password);
            //ApplicationUser user = await DBContext.UserManager.FindByEmailAsync(userDto.Email);

            // авторизуем его и возвращаем объект ClaimsIdentity
            if (user != null)
                claim = await DBContext.UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);

            return claim;
        }

        public async Task<OperationDetails> CreateAsync(UserDTO userDto)
        {
            ApplicationUser user = await DBContext.UserManager.FindByEmailAsync(userDto.Email);
            if (user == null)
            {
                user = new ApplicationUser { Email = userDto.Email, UserName = userDto.Email };
                var Result = await DBContext.UserManager.CreateAsync(user, userDto.Password);

                if (Result.Errors.Count() > 0)
                    return new OperationDetails(false, Result.Errors.FirstOrDefault(), "");

                if (await DBContext.RoleManager.RoleExistsAsync(userDto.Role) == false) //Якщо ролі не існує взагалі, то створити
                    await DBContext.RoleManager.CreateAsync(new ApplicationRole { Name = userDto.Role });

                await DBContext.UserManager.AddToRoleAsync(user.Id, userDto.Role);

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
    }
}
