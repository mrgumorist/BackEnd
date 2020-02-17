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
            catch (InvalidOperationException ee)
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
            apiContext.SaveChanges();
        }
        public static void ChangePasswordById(int ID, string newpass)
        {
            apiContext.Users.First(x => x.ID == ID).Password = newpass;
            apiContext.SaveChanges();
        }
        public static void ChangeNameAndSurname(int ID, string name, string surname)
        {
            apiContext.Users.First(x => x.ID == ID).Name = name;
            apiContext.Users.First(x => x.ID == ID).Surname = surname;
            apiContext.SaveChanges();
        }
        public static List<ProductDto> GetProducts()
        {
            var list = apiContext.ProductsAvaliale.ToList();
            List<ProductDto> results = new List<ProductDto>();
            foreach (var item in list)
            {
                ProductDto productDto = new ProductDto();
                productDto.CameToTheStorage = item.CameToTheStorage;
                productDto.Count = item.Count;
                productDto.Description = item.Description;
                productDto.ID = item.ID;
                productDto.IsNumurable = item.IsNumurable;
                productDto.Massa = item.Massa;
                productDto.Name = item.Name;
                productDto.SpecialCode = item.SpecialCode;
                productDto.Price = item.Price;
                results.Add(productDto);
            }
            return results;
        }
        public static bool IsExists(string specialcode)
        {
            List<Product> results =  apiContext.ProductsAvaliale.Where(x => x.SpecialCode == specialcode).ToList();
            if(results.Count==0)
            {
                return false;
            }
            return true;
        }
        public static void AddProduct(ProductDto product)
        {
            Product product1 = new Product() { Name = product.Name, Count = product.Count, Description = product.Description, IsNumurable = product.IsNumurable, Massa = product.Massa, SpecialCode = product.SpecialCode, Price=product.Price};
            apiContext.ProductsAvaliale.Add(product1);
            apiContext.SaveChanges();
        }
        public static void ChangeProduct(ProductDto product)
        {
            apiContext.ProductsAvaliale.FirstOrDefault(x => x.ID == product.ID).Massa = product.Massa;
            apiContext.ProductsAvaliale.FirstOrDefault(x => x.ID == product.ID).Name = product.Name;
            apiContext.ProductsAvaliale.FirstOrDefault(x => x.ID == product.ID).SpecialCode = product.SpecialCode;
            apiContext.ProductsAvaliale.FirstOrDefault(x => x.ID == product.ID).Count = product.Count;
            apiContext.ProductsAvaliale.FirstOrDefault(x => x.ID == product.ID).Description = product.Description;
            apiContext.ProductsAvaliale.FirstOrDefault(x => x.ID == product.ID).Price = product.Price;
            apiContext.SaveChanges();
        }
        public static string GetPassByID(int ID)
        {
            return apiContext.Users.First(x => x.ID == ID).Password;
        }
        public static int GetIdOfCheck()
        {
            Check check = new Check
            {
                DateCreatingOfCheck = DateTime.Now,
                DateCloseOfCheck = DateTime.Now
            };
            apiContext.Checks.Add(check);
            apiContext.SaveChanges();
            return check.ID;
        }
        public static List<ProductDto> GetProductDtosByQuery(string Query)
        {
            List<ProductDto> products = new List<ProductDto>();
            foreach (var item in apiContext.ProductsAvaliale)
            {
                if(item.SpecialCode== Query)
                {
                    ProductDto productDto = new ProductDto();
                    productDto.CameToTheStorage = item.CameToTheStorage;
                    productDto.Count = item.Count;
                    productDto.Description = item.Description;
                    productDto.ID = item.ID;
                    productDto.IsNumurable = item.IsNumurable;
                    productDto.Massa = item.Massa;
                    productDto.Name = item.Name;
                    productDto.SpecialCode = item.SpecialCode;
                    productDto.Price = item.Price;
                    products.Add(productDto);
                }
            }
            return products;
        }
        public static List<ProductDto>  GetProductDtosByName(string Query)
        {
            List<ProductDto> products = new List<ProductDto>();
            foreach (var item in apiContext.ProductsAvaliale)
            {
                if (item.Name.ToLower().Contains(Query.ToLower()))
                {
                    ProductDto productDto = new ProductDto();
                    productDto.CameToTheStorage = item.CameToTheStorage;
                    productDto.Count = item.Count;
                    productDto.Description = item.Description;
                    productDto.ID = item.ID;
                    productDto.IsNumurable = item.IsNumurable;
                    productDto.Massa = item.Massa;
                    productDto.Name = item.Name;
                    productDto.SpecialCode = item.SpecialCode;
                    productDto.Price = item.Price;
                    products.Add(productDto);
                }
            }
            return products;
        }
    }
}