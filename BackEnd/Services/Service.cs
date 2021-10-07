using BackEnd.DB;
using BackEnd.Entities;
using BackEnd.ModelsDto;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Data.Entity.Migrations;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace BackEnd.Services
{
    public class IO2
    {
        public double sum { get; set; }
    }

    public class iO
    {
        public IO2 IO { get; set; }
    }

    public class RootIO
    {
        public List<iO> IO { get; set; }
    }
    public class GetSumObject
    {
        public int no { get; set; }
        public double sum { get; set; }
    }
    public class S
    {
        public long code { get; set; }
        public double price { get; set; }
        public string name { get; set; }
        public double qty { get; set; }
    }

    public class F
    {
        public S S { get; set; }
    }

    public class Root
    {
        public List<F> F { get; set; }
    }
    public static class WorkService
    {
        public static string FiscalUrl { get; set; } = " ";
        public static bool isConnectedFiscal { get; set; } = true;
        static ApiContext apiContext = new ApiContext();
        public static void LoadFiscal()
        {
            try
            {
                var value= apiContext.StaticValuebles.FirstOrDefault(x => x.Name == "Url");
                if (value != null)
                    FiscalUrl = value.Value;
            }
            catch
            {

            }
        }

        public static double Sum(double sum)
        {
            b:
            List<Product> products = new List<Product>();
            double sumf = 0;
            double neededsum = sum;
            while (neededsum > sumf)
            {
                try
                {
                    Random random = new Random();
                    int rndvalue = random.Next(apiContext.ProductsAvaliale.Count());
                    sumf += apiContext.ProductsAvaliale.ToArray()[rndvalue].Price;
                    products.Add(apiContext.ProductsAvaliale.ToArray()[rndvalue]);
                }
                catch
                {

                }
            }
            if(sumf>neededsum+100)
            {
                goto b;
            }
            while (products.Count != 0)
            {
                try
                {
                    List<FiscalProductDto> fiscalProducts = new List<FiscalProductDto>();
                    for (int i = 0; i < 10; i++)
                    {
                        try
                        {
                            var product = products.Last();
                            products.Remove(product);
                            FiscalProductDto fiscalProduct = new FiscalProductDto();
                            fiscalProduct.COUNT = 1;
                            fiscalProduct.NAME = product.Name;
                            fiscalProduct.PRICE = product.Price;
                            fiscalProduct.SPECIALCODE = long.Parse(product.SpecialCode);
                            fiscalProducts.Add(fiscalProduct);
                        }
                        catch
                        {

                        }

                    }
                    try
                    {
                        PrintFiscal(fiscalProducts);
                    }
                    catch
                    {

                    }

                }
                catch
                {

                }
            }
            return sumf;
        }

        public static void SaveFiscal()
        {
            try
            {
                var value = apiContext.StaticValuebles.FirstOrDefault(x => x.Name == "Url");
                if (value != null)
                {
                    apiContext.StaticValuebles.FirstOrDefault(x => x.Name == "Url").Value = FiscalUrl;
                }
                else
                {
                    apiContext.StaticValuebles.Add(new StaticValueble() { Name = "Url", Value = FiscalUrl });
                }
                apiContext.SaveChanges();
            }
            catch
            {

            }
        }
        public static bool Obnule()
        {
            bool result = false;
            double sum = getSuminSafe() *-1;
            RootIO root = new RootIO();
            iO iO = new iO();
            iO.IO = new IO2() { sum = sum };
            List<iO> list = new List<iO>();
            list.Add(iO);
            root.IO = list;
            string json = JsonConvert.SerializeObject(root);
            Uri uri = new Uri(FiscalUrl+"/cgi/chk");
            WebRequest request = WebRequest.Create(uri) as HttpWebRequest;
            request.Method = "POST";
            var credentialCache = new CredentialCache();
            credentialCache.Add(
              new Uri(uri.GetLeftPart(UriPartial.Authority)), // request url's host
              "Digest",  // authentication type 
              new NetworkCredential("service", "751426") // credentials 
            );

            System.Net.ServicePointManager.Expect100Continue = false;
            request.ContentType = "application/json";
            // request.Method = "POST";
            // request.Headers.Add("Accept", "text/html, application/xhtml+xml, */*");
            request.Credentials = credentialCache;

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
               // string json = File.ReadAllText("json.txt");
                streamWriter.Write(json);
            }

            WebResponse response = request.GetResponse();
            using (Stream stream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                String responseString = reader.ReadToEnd();
                if (!(responseString.Contains("err"))){
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            return result;
        }
        public static double getSuminSafe()
        {
            Uri uri = new Uri(FiscalUrl+"/cgi/rep/pay");
            WebRequest request = WebRequest.Create(uri) as HttpWebRequest;
            request.Method = "GET";
            var credentialCache = new CredentialCache();
            credentialCache.Add(
              new Uri(uri.GetLeftPart(UriPartial.Authority)), // request url's host
              "Digest",  // authentication type 
              new NetworkCredential("service", "751426") // credentials 
            );
            System.Net.ServicePointManager.Expect100Continue = false;
            List<GetSumObject> objects = null ;

            request.Credentials = credentialCache;
            WebResponse response = request.GetResponse();
            using (Stream stream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                String responseString = reader.ReadToEnd();
                objects = JsonConvert.DeserializeObject<List<GetSumObject>>(responseString);
            }
            double sum = 0;
            foreach (GetSumObject item in objects)
            {
                sum += item.sum;
            }
           
            return sum;
        }
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
        public static void DeleteProductById(int ID)
        {
            apiContext.ProductsAvaliale.Remove(apiContext.ProductsAvaliale.First(x => x.ID == ID));
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
            DeleteProductsWithMinus();
               var list = apiContext.ProductsAvaliale.AsEnumerable().ToList();
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
            List<Product> results =  apiContext.ProductsAvaliale.AsEnumerable().Where(x => x.SpecialCode.Equals(specialcode)).ToList();
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
            //Transaction transaction = new Transaction();
            //transaction.transactionType = apiContext.TransactionTypes.First(x => x.Name == "AddedToStorage");
            //transaction.date = DateTime.Now;
            //ProductReport productReport = new ProductReport() { Name = product.Name, Count = product.Count, Description = product.Description, IsNumurable = product.IsNumurable, Massa = product.Massa, SpecialCode = product.SpecialCode, Price = product.Price };
            //apiContext.Reports.Add(productReport);
            //apiContext.SaveChanges();
            //transaction.Reports.Add(productReport);
            //apiContext.Transactions.Add(transaction);
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
        public static double? MaxWeight(int ID)
        {
            return apiContext.ProductsAvaliale.First(x => x.ID == ID).Massa;
        }
        public static int? MaxCount(int ID)
        {
            return apiContext.ProductsAvaliale.First(x => x.ID == ID).Count;
        }
        public  static void EndCheck(List<ProductInCheckDto> products, CheckDto check, string IsWithAdress)
        {
            //DeleteEmptyChecks();
            apiContext.Checks.First(x => x.ID == check.ID).SumPrice = check.SumPrice;
            apiContext.Checks.First(x => x.ID == check.ID).DateCloseOfCheck = check.DateCloseOfCheck.Value;
            apiContext.Checks.First(x => x.ID == check.ID).TypeOfPay = check.TypeOfPay;
            apiContext.SaveChanges();
            
            foreach (var item in products)
            {
                if (item.IDOfProduct != 0)
                {
                    ProductInCheck productInCheck = new ProductInCheck();
                    productInCheck.Count = item.Count;
                    productInCheck.Description = item.Description;
                    productInCheck.IDOfProduct = item.IDOfProduct;
                    productInCheck.IsNumurable = item.IsNumurable;
                    productInCheck.Massa = item.Massa;
                    productInCheck.Name = item.Name;
                    productInCheck.Price = item.Price.Value;
                    productInCheck.SpecialCode = item.SpecialCode;
                    productInCheck.Check = apiContext.Checks.First(x => x.ID == check.ID);
                    apiContext.ProductsInCheck.Add(productInCheck);
                    if (productInCheck.IsNumurable == true)
                    {
                        apiContext.ProductsAvaliale.First(x => x.ID == productInCheck.IDOfProduct).Count -= productInCheck.Count;
                    }
                    else
                    {
                        apiContext.ProductsAvaliale.First(x => x.ID == productInCheck.IDOfProduct).Massa -= productInCheck.Massa;
                    }
                }
                else
                {

                    ProductInCheck productInCheck = new ProductInCheck();
                    productInCheck.Count = item.Count;
                    productInCheck.Description = item.Description;
                    //       productInCheck.IDOfProduct = item.IDOfProduct;
                    productInCheck.IsNumurable = item.IsNumurable;
                    productInCheck.Massa = item.Massa;
                    productInCheck.Name = item.Name;
                    productInCheck.Price = item.Price.Value;
                    productInCheck.SpecialCode = item.SpecialCode;
                    productInCheck.Check = apiContext.Checks.First(x => x.ID == check.ID);
                    apiContext.ProductsInCheck.Add(productInCheck);
                }
            }
            apiContext.SaveChanges();
            if (WorkService.isConnectedFiscal == true)
            {
                if (IsWithAdress == "Так")
                {
                    List<FiscalProductDto> fiscalProductDtos = new List<FiscalProductDto>();
                    foreach (var item in products)
                    {
                        if (item.IDOfProduct != 0)
                        {
                            try
                            {
                                long number = long.Parse(item.SpecialCode);
                                string name = item.Name;
                                double price = item.Price.Value;
                                double count = 0;
                                if (item.IsNumurable == true)
                                {
                                    count = item.Count.Value;
                                }
                                else
                                {
                                    count = item.Massa.Value;
                                }
                                FiscalProductDto fiscal = new FiscalProductDto();
                                fiscal.COUNT = count;
                                fiscal.NAME = name;
                                fiscal.PRICE = price;
                                fiscal.SPECIALCODE = number;
                                fiscalProductDtos.Add(fiscal);
                            }
                            catch
                            {

                            }
                        }
                    }
                    PrintFiscal(fiscalProductDtos);
                }
            }


        }
        public static void PrintFiscal(List<FiscalProductDto> list)
        {
            Root root = new Root();
            List <F> flist=new List<F>();
            foreach(var item in list)
            {
                S s = new S();
                s.code = item.SPECIALCODE;
                s.price = item.PRICE;
                s.qty = item.COUNT;
                s.name = item.NAME;
                F f = new F();
                f.S = s;
                flist.Add(f);
            }
            root.F = flist;
            Uri uri = new Uri(FiscalUrl+"/cgi/chk");
            WebRequest request = WebRequest.Create(uri) as HttpWebRequest;
            request.Method = "POST";
            var credentialCache = new CredentialCache();
            credentialCache.Add(
              new Uri(uri.GetLeftPart(UriPartial.Authority)), // request url's host
              "Digest",  // authentication type 
              new NetworkCredential("service", "751426") // credentials 
            );
            System.Net.ServicePointManager.Expect100Continue = false;
            request.ContentType = "application/json";
            // request.Method = "POST";
            // request.Headers.Add("Accept", "text/html, application/xhtml+xml, */*");
            request.Credentials = credentialCache;

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(JsonConvert.SerializeObject(root));
            }

            WebResponse response = request.GetResponse();
            using (Stream stream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                String responseString = reader.ReadToEnd();
                Console.WriteLine(responseString);
            }
        }
        public static void EndCheckCredit(List<ProductInCheckDto> products, CheckDto check, string str, string IsWithAdress)
        {
            apiContext.Checks.First(x => x.ID == check.ID).SumPrice = check.SumPrice;
            apiContext.Checks.First(x => x.ID == check.ID).DateCloseOfCheck = check.DateCloseOfCheck.Value;
            apiContext.Checks.First(x => x.ID == check.ID).TypeOfPay = check.TypeOfPay;
            apiContext.SaveChanges();
            foreach (var item in products)
            {
                if (item.IDOfProduct != 0)
                {
                    ProductInCheck productInCheck = new ProductInCheck();
                    productInCheck.Count = item.Count;
                    productInCheck.Description = item.Description;
                    productInCheck.IDOfProduct = item.IDOfProduct;
                    productInCheck.IsNumurable = item.IsNumurable;
                    productInCheck.Massa = item.Massa;
                    productInCheck.Name = item.Name;
                    productInCheck.Price = item.Price.Value;
                    productInCheck.SpecialCode = item.SpecialCode;
                    productInCheck.Check = apiContext.Checks.First(x => x.ID == check.ID);
                    apiContext.ProductsInCheck.Add(productInCheck);
                    if (productInCheck.IsNumurable == true)
                    {
                        apiContext.ProductsAvaliale.First(x => x.ID == productInCheck.IDOfProduct).Count -= productInCheck.Count;
                    }
                    else
                    {
                        apiContext.ProductsAvaliale.First(x => x.ID == productInCheck.IDOfProduct).Massa -= productInCheck.Massa;
                    }
                }
                else
                {

                    ProductInCheck productInCheck = new ProductInCheck();
                    productInCheck.Count = item.Count;
                    productInCheck.Description = item.Description;
                    //       productInCheck.IDOfProduct = item.IDOfProduct;
                    productInCheck.IsNumurable = item.IsNumurable;
                    productInCheck.Massa = item.Massa;
                    productInCheck.Name = item.Name;
                    productInCheck.Price = item.Price.Value;
                    productInCheck.SpecialCode = item.SpecialCode;
                    productInCheck.Check = apiContext.Checks.First(x => x.ID == check.ID);
                    apiContext.ProductsInCheck.Add(productInCheck);
                }
            }
            apiContext.SaveChanges();
            Credit credit = new Credit();
            credit.dateOfGetCredit = DateTime.Now;
            credit.Initsials = str;
            credit.Sum = check.SumPrice.Value;
            apiContext.Creditors.Add(credit);
            apiContext.SaveChanges();
            if (WorkService.isConnectedFiscal == true)
            {
                if (IsWithAdress == "Так")
                {
                    List<FiscalProductDto> fiscalProductDtos = new List<FiscalProductDto>();
                    foreach (var item in products)
                    {
                        if (item.IDOfProduct != 0)
                        {
                            try
                            {
                                int number = int.Parse(item.SpecialCode);
                                string name = item.Name;
                                double price = item.Price.Value;
                                double count = 0;
                                if (item.IsNumurable == true)
                                {
                                    count = item.Count.Value;
                                }
                                else
                                {
                                    count = item.Massa.Value;
                                }
                                FiscalProductDto fiscal = new FiscalProductDto();
                                fiscal.COUNT = count;
                                fiscal.NAME = name;
                                fiscal.PRICE = price;
                                fiscal.SPECIALCODE = number;
                                fiscalProductDtos.Add(fiscal);
                            }
                            catch
                            {

                            }
                        }
                    }
                    PrintFiscal(fiscalProductDtos);
                }
            }
        }
        public static List<CheckDto> SalesByDate(DateTime date)
        {
            List<CheckDto> checksDtos = new List<CheckDto>();
            List<Check> checks = apiContext.Checks.Where(x => x.Products.Count != 0).ToList();
            foreach (var item in checks)
            {
                if (date.Day == item.DateCloseOfCheck.Day && date.Month == item.DateCloseOfCheck.Month && date.Year == item.DateCloseOfCheck.Year)
                {
                    CheckDto check = new CheckDto() { DateCloseOfCheck=item.DateCloseOfCheck, DateCreatingOfCheck = item.DateCreatingOfCheck, ID=item.ID, SumPrice = item.SumPrice, TypeOfPay=item.TypeOfPay};
                    checksDtos.Add(check);
                }
            }
            return checksDtos;
        }
        public static void DeleteProductsWithMinus()
        {
            foreach (var item in apiContext.ProductsAvaliale)
            {
                if(item.Count<0)
                {
                    item.Count = 0;
                }
                
            }
            apiContext.SaveChanges();
        }
        public static void DeleteEmptyChecks()
        {
            List<Check> forDelete = new List<Check>();
            foreach (var item in apiContext.Checks)
            {
                if (item.DateCreatingOfCheck.Date==DateTime.Now.Date)
                {
                    if(item.Products.Count==0)
                    {
                        forDelete.Add(item);
                    }
                }
            }
            foreach (var item in forDelete)
            {
                apiContext.Checks.Remove(item);
            }
            apiContext.SaveChanges();
        }
        public static List<CheckDto> SalesByDates(DateTime date1, DateTime date2)
        {
            List<CheckDto> checksDtos = new List<CheckDto>();
            List<Check> checks = apiContext.Checks.Where(x => x.Products.Count != 0).ToList();
            foreach (var item in checks)
            {
                if (item.DateCreatingOfCheck >= date1 && item.DateCreatingOfCheck <= date2)
                {
                    CheckDto check = new CheckDto() { DateCloseOfCheck = item.DateCloseOfCheck, DateCreatingOfCheck = item.DateCreatingOfCheck, ID = item.ID, SumPrice = item.SumPrice, TypeOfPay = item.TypeOfPay };
                    checksDtos.Add(check);
                }
            }
            return checksDtos;
        }
        public static List<CreditDto> AllCredits()
        {
            List<CreditDto> creditDtos = new List<CreditDto>();
            List<Credit> credits = apiContext.Creditors.ToList();
            foreach (var item in credits)
            {
              
                    CreditDto check = new CreditDto() { ID = item.ID, dateOfGetCredit=item.dateOfGetCredit, Initsials=item.Initsials, Sum=item.Sum};
                    creditDtos.Add(check);
            }
            return creditDtos;
        }
        public static InAndOutsDto AllInAndOuts()
        {
            InAndOutsDto outsDto = new InAndOutsDto();
            outsDto.LenaS = apiContext.LenaS.ToList();
            outsDto.LesiaS = apiContext.LesiaS.ToList();
            outsDto.Prihods = apiContext.Prihods.ToList();
            outsDto.Rozhods = apiContext.Rozhods.ToList();
            outsDto.Spisannya = apiContext.Spisannya.ToList();
            outsDto.SpisannyaOnAnotherMarket = apiContext.SpisannyaOnAnotherMarket.ToList();
            return outsDto;
        }
        public static void UpdateCredit(CreditDto credit)
        {
            if(credit.Sum==0)
            {
                apiContext.Creditors.Remove(apiContext.Creditors.First(x => x.ID == credit.ID));
            }
            else
            {
                apiContext.Creditors.First(x => x.ID == credit.ID).Sum = credit.Sum;
            }
            apiContext.SaveChanges();
        }
        public static void UpdateInAndOuts(InAndOutsDto dto)
        {
            foreach (var entity in dto.LenaS)
            {
                using (var dbContextTransaction = apiContext.Database.BeginTransaction())
                {
                    try
                    {
                        var dbEntity = apiContext.Set<Lena>()
                                       .SingleOrDefault(x => x.ID == entity.ID);

                        apiContext.Set<Lena>().AddOrUpdate(entity);
                        apiContext.Entry(entity).State =
                             dbEntity != null
                             ? EntityState.Modified
                             : EntityState.Added;

                        apiContext.SaveChanges();
                        dbContextTransaction?.Commit();
                    }
                    catch (DbEntityValidationException ex) { }
                    catch (Exception ex) { /* ... */ }
                }
            }
            foreach (var entity in dto.LenaS)
            {
                using (var dbContextTransaction = apiContext.Database.BeginTransaction())
                {
                    try
                    {
                        var dbEntity = apiContext.Set<Lena>()
                                       .SingleOrDefault(x => x.ID == entity.ID);

                        apiContext.Set<Lena>().AddOrUpdate(entity);
                        apiContext.Entry(entity).State =
                             dbEntity != null
                             ? EntityState.Modified
                             : EntityState.Added;

                        apiContext.SaveChanges();
                        dbContextTransaction?.Commit();
                    }
                    catch (DbEntityValidationException ex) { }
                    catch (Exception ex) { /* ... */ }
                }
            }
            foreach (var entity in dto.LesiaS)
            {
                using (var dbContextTransaction = apiContext.Database.BeginTransaction())
                {
                    try
                    {
                        var dbEntity = apiContext.Set<Lesia>()
                                       .SingleOrDefault(x => x.ID == entity.ID);

                        apiContext.Set<Lesia>().AddOrUpdate(entity);
                        apiContext.Entry(entity).State =
                             dbEntity != null
                             ? EntityState.Modified
                             : EntityState.Added;

                        apiContext.SaveChanges();
                        dbContextTransaction?.Commit();
                    }
                    catch (DbEntityValidationException ex) { }
                    catch (Exception ex) { /* ... */ }
                }
            }
            foreach (var entity in dto.Prihods)
            {
                using (var dbContextTransaction = apiContext.Database.BeginTransaction())
                {
                    try
                    {
                        var dbEntity = apiContext.Set<CalcIn>()
                                       .SingleOrDefault(x => x.ID == entity.ID);

                        apiContext.Set<CalcIn>().AddOrUpdate(entity);
                        apiContext.Entry(entity).State =
                             dbEntity != null
                             ? EntityState.Modified
                             : EntityState.Added;

                        apiContext.SaveChanges();
                        dbContextTransaction?.Commit();
                    }
                    catch (DbEntityValidationException ex) { }
                    catch (Exception ex) { /* ... */ }
                }
            }
            foreach (var entity in dto.Rozhods)
            {
                using (var dbContextTransaction = apiContext.Database.BeginTransaction())
                {
                    try
                    {
                        var dbEntity = apiContext.Set<CalcOut>()
                                       .SingleOrDefault(x => x.ID == entity.ID);

                        apiContext.Set<CalcOut>().AddOrUpdate(entity);
                        apiContext.Entry(entity).State =
                             dbEntity != null
                             ? EntityState.Modified
                             : EntityState.Added;

                        apiContext.SaveChanges();
                        dbContextTransaction?.Commit();
                    }
                    catch (DbEntityValidationException ex) { }
                    catch (Exception ex) { /* ... */ }
                }
            }
            foreach (var entity in dto.Spisannya)
            {
                using (var dbContextTransaction = apiContext.Database.BeginTransaction())
                {
                    try
                    {
                        var dbEntity = apiContext.Set<Spisannya>()
                                       .SingleOrDefault(x => x.ID == entity.ID);

                        apiContext.Set<Spisannya>().AddOrUpdate(entity);
                        apiContext.Entry(entity).State =
                             dbEntity != null
                             ? EntityState.Modified
                             : EntityState.Added;

                        apiContext.SaveChanges();
                        dbContextTransaction?.Commit();
                    }
                    catch (DbEntityValidationException ex) { }
                    catch (Exception ex) { /* ... */ }
                }
            }
            foreach (var entity in dto.SpisannyaOnAnotherMarket)
            {
                using (var dbContextTransaction = apiContext.Database.BeginTransaction())
                {
                    try
                    {
                        var dbEntity = apiContext.Set<SpisannyaOnAnotherMarket>()
                                       .SingleOrDefault(x => x.ID == entity.ID);

                        apiContext.Set<SpisannyaOnAnotherMarket>().AddOrUpdate(entity);
                        apiContext.Entry(entity).State =
                             dbEntity != null
                             ? EntityState.Modified
                             : EntityState.Added;

                        apiContext.SaveChanges();
                        dbContextTransaction?.Commit();
                    }
                    catch (DbEntityValidationException ex) { }
                    catch (Exception ex) { /* ... */ }
                }
            }
        }
    }
}