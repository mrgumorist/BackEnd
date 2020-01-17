using BackEnd.DB;
using BackEnd.Entities;
using BackEnd.ModelsDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace BackEnd.Services
{
    public static class WorkService
    {
        static ApiContext apiContext = new ApiContext();
        public static int Login(string login, string password)
        {
            try
            {
                int ID = apiContext.Users.First(x => x.Login == login && x.Password == password).ID;
                return ID;
            }
            catch (InvalidOperationException e)
            {
                return -1;
            }
        }
        public static string GetTypeById(int ID)
        {
            return apiContext.Users.First(x => x.ID == ID).UsersType.Name;
        }
        public static List<UserDto> GetUsers()
        {
            List<UserDto> users = new List<UserDto>();
            List<User> userss = apiContext.Users.ToList();
           foreach (var item in userss)
            {
                var lol = item;
                UserDto user = new UserDto() { ID = lol.ID, LastLogin = lol.LastLogin, Login = lol.Login, Name = lol.Name, Password = lol.Password, Surname = lol.Surname, UsersType = lol.UsersType.Name };
                users.Add(user);
            }
            return users;
        }
        public static void DeleteById(int ID)
        {
            apiContext.Users.Remove(apiContext.Users.First(x => x.ID == ID));
            apiContext.SaveChanges();
        }
        public static void AddUser(UserDto user)
        {
            int id = int.Parse(user.UsersType);
            var UsersType = apiContext.UsersTypes.First(x => x.Id == id);
            var kek = new User() { Name = user.Name, Login = user.Login, LastLogin = user.LastLogin, Password = user.Password, Surname = user.Surname, UsersType = UsersType };
            apiContext.Users.Add(kek);
            apiContext.SaveChanges();
        }
        public static UserDto GetUserByID(int ID)
        {
            User lol = apiContext.Users.First(x => x.ID == ID);
            UserDto user = new UserDto() { ID = lol.ID, LastLogin = lol.LastLogin, Login = lol.Login, Name = lol.Name, Password = lol.Password, Surname = lol.Surname, UsersType = lol.UsersType.Name };
            return user;
        }
        public static void UpdateUser(UserDto user)
        {
            int id = int.Parse(user.UsersType);
            var UsersType = apiContext.UsersTypes.First(x => x.Id == id);
            var kek = new User() { Name = user.Name, Login = user.Login, LastLogin = user.LastLogin, Password = user.Password, Surname = user.Surname, UsersType = UsersType, ID=user.ID };
            apiContext.Users.First(x => x.ID == user.ID).LastLogin = kek.LastLogin;
            apiContext.Users.First(x => x.ID == user.ID).Login = kek.Login;
            apiContext.Users.First(x => x.ID == user.ID).Password = kek.Password;
            apiContext.Users.First(x => x.ID == user.ID).Name = kek.Name;
            apiContext.Users.First(x => x.ID == user.ID).Surname = kek.Surname;
            apiContext.Users.First(x => x.ID == user.ID).UsersType = kek.UsersType;
            apiContext.SaveChanges();
        }
        public static void SetLastLogin(int ID)
        {
            apiContext.Users.First(x => x.ID ==ID).LastLogin = DateTime.Now;
        }
        
    }
}