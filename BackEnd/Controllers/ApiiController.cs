using BackEnd.ModelsDto;
using BackEnd.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace BackEnd.Controllers
{
    public class ApiiController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
        [HttpGet]
        public IHttpActionResult login()
        {
            var re = Request;
            var headers = re.Headers;

            if (headers.Contains("Login") && headers.Contains("Password"))
            {
                string login = headers.GetValues("Login").First();
                string password = headers.GetValues("Password").First();
                int res = WorkService.Login(login, password);
                if (res != -1)
                {
                    return Ok(res);
                }
                else
                {
                    return Ok(-1);
                }
            }
            else
            {
                return Ok(-1);
            }
            //return -1;

        }
        [HttpGet]
        public IHttpActionResult GetTypeOfAccount()
        {
            var re = Request;
            var headers = re.Headers;

            if (headers.Contains("ID"))
            {
                string ID = headers.GetValues("ID").First();

                return Ok(WorkService.GetTypeById(int.Parse(ID)));

            }
            else
            {
                return BadRequest();
            }
        }
        [HttpGet]
        public IHttpActionResult GetUsers()
        {
            var re = Request;
            var headers = re.Headers;

            if (headers.Contains("Safety"))
            {
                return Ok(WorkService.GetUsers());
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpGet]
        public IHttpActionResult DeleteUser()
        {
            var re = Request;
            var headers = re.Headers;

            if (headers.Contains("Safety") && headers.Contains("ID"))
            {

                string ID = headers.GetValues("ID").First();
                WorkService.DeleteById(int.Parse(ID));
                return Ok();

            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPost]
        public IHttpActionResult AddUser(UserDto user)
        {
            var re = Request;
            var headers = re.Headers;

            if (headers.Contains("Safety"))
            {
                WorkService.AddUser(user);
                return Ok();

            }
            else
            {
                return BadRequest();
            }
        }
        [HttpGet]
        public IHttpActionResult GetUserByID()
        {
            var re = Request;
            var headers = re.Headers;

            if (headers.Contains("Safety"))
            {

                string ID = headers.GetValues("ID").First();
                return Ok(WorkService.GetUserByID(int.Parse(ID)));

            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPost]
        public IHttpActionResult UpdateUser(UserDto user)
        {
            var re = Request;
            var headers = re.Headers;

            if (headers.Contains("Safety"))
            {
                WorkService.UpdateUser(user);
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpGet]
        public IHttpActionResult SetLastLogin()
        {
            var re = Request;
            var headers = re.Headers;

            if (headers.Contains("Safety"))
            {

                string ID = headers.GetValues("ID").First();
                WorkService.SetLastLogin(int.Parse(ID));
                return Ok();

            }
            else
            {
                return BadRequest();
            }
        }
        [HttpGet]
        public IHttpActionResult ChangePassByID()
        {
            var re = Request;
            var headers = re.Headers;

            if (headers.Contains("Safety"))
            {

                string ID = headers.GetValues("ID").First();
                string NEWPASS = headers.GetValues("NEWPASS").First();
                WorkService.ChangePasswordById(int.Parse(ID), NEWPASS);
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpGet]
        public IHttpActionResult ChangeNameAndSurname()
        {
            var re = Request;
            var headers = re.Headers;

            if (headers.Contains("Safety"))
            {
                string ID = headers.GetValues("ID").First();
                string Name = headers.GetValues("Name").First();
                string Surname = headers.GetValues("Surname").First();
                WorkService.ChangeNameAndSurname(int.Parse(ID), Name, Surname);
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpGet]
        public IHttpActionResult GetProducts()
        {
            var re = Request;
            var headers = re.Headers;

            if (headers.Contains("Safety"))
            {
                return Ok(WorkService.GetProducts());
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public IHttpActionResult IsExists([FromBody] string SpecialCode)
        {
            var re = Request;
            var headers = re.Headers;

            if (headers.Contains("Safety"))
            {
                //string SpecialCode = headers.GetValues("SpecialCode").First();
                return Ok(WorkService.IsExists(SpecialCode));
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPost]
        public IHttpActionResult AddProduct(ProductDto product)
        {
            var re = Request;
            var headers = re.Headers;

            if (headers.Contains("Safety"))
            {
                WorkService.AddProduct(product);
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public IHttpActionResult ChangeProduct(ProductDto product)
        {
            var re = Request;
            var headers = re.Headers;
            if (headers.Contains("Safety"))
            {
                WorkService.ChangeProduct(product);
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpGet]
        public IHttpActionResult GetPasswordById()
        {
            var re = Request;
            var headers = re.Headers;

            if (headers.Contains("Safety"))
            {
                string ID = headers.GetValues("ID").First();
                string pass = WorkService.GetPassByID(int.Parse(ID));
                return Ok(pass);
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpGet]
        public IHttpActionResult GetIdOfCheck()
        {
            var re = Request;
            var headers = re.Headers;

            if (headers.Contains("Safety"))
            {
                //string ID = headers.GetValues("ID").First();
                string Id = WorkService.GetIdOfCheck().ToString();
                return Ok(Id);
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPost]
        public IHttpActionResult GetBySpecialCode([FromBody]string Querry)
        {
            var re = Request;
            var headers = re.Headers;

            if (headers.Contains("Safety"))
            {
                //string Querry = headers.GetValues("Querry").First();
                var list = WorkService.GetProductDtosByQuery(Querry);
                if (list.Count!=0)
                {
                    return Ok(list);
                }
                else
                {
                    return Ok(0);
                }
                
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPost]
        public IHttpActionResult GetByName([FromBody]string Querry)
        {
            var re = Request;
            var headers = re.Headers;

            if (headers.Contains("Safety"))
            {
                var list = WorkService.GetProductDtosByName(Querry);
                if (list.Count != 0)
                {
                    return Ok(list);
                }
                else
                {
                    return Ok(0);
                }

            }
            else
            {
                return BadRequest();
            }
        }
        [HttpGet]
        public IHttpActionResult GetMaxWeigthById()
        {
            var re = Request;
            var headers = re.Headers;

            if (headers.Contains("Safety") )
            {
                string ID = headers.GetValues("ID").First();
                return Ok(WorkService.MaxWeight(int.Parse(ID)));

            }
            else
            {
                return BadRequest();
            }
        }
        [HttpGet]
        public IHttpActionResult GetMaxCountById()
        {
            var re = Request;
            var headers = re.Headers;

            if (headers.Contains("Safety"))
            {
                string ID = headers.GetValues("ID").First();
                return Ok(WorkService.MaxCount(int.Parse(ID)));

            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPost]
        public IHttpActionResult EndCheck(List<string>str)
        {
            var re = Request;
            var headers = re.Headers;

            if (headers.Contains("Safety"))
            {
                var products = JsonConvert.DeserializeObject<List<ProductInCheckDto>>(str[0]);
                var check = JsonConvert.DeserializeObject<CheckDto>(str[1]);
                WorkService.EndCheck(products, check);
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPost]
        public IHttpActionResult EndCheckCredit(List<string> str)
        {
            var re = Request;
            var headers = re.Headers;

            if (headers.Contains("Safety"))
            {
                var products = JsonConvert.DeserializeObject<List<ProductInCheckDto>>(str[0]);
                var check = JsonConvert.DeserializeObject<CheckDto>(str[1]);
                var Creditor = JsonConvert.DeserializeObject<string>(str[2]);
                WorkService.EndCheckCredit(products, check, Creditor);
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPost]
        public IHttpActionResult GetSalesByDate([FromBody] DateDto dto)
        {
            var re = Request;
            var headers = re.Headers;

            if (headers.Contains("Safety"))
            {
                return Ok(WorkService.SalesByDate(dto.date));
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpGet]
        public IHttpActionResult DeleteProductById()
        {
            var re = Request;
            var headers = re.Headers;

            if (headers.Contains("Safety") && headers.Contains("ID"))
            {

                string ID = headers.GetValues("ID").First();
                WorkService.DeleteProductById(int.Parse(ID));
                return Ok();

            }
            else
            {
                return BadRequest();
            }
        }
    }
}
