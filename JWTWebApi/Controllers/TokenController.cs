using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace JWTWebApi.Controllers
{
    public class TokenController : ApiController
    {
        [HttpGet]
        //Call like > http://localhost:4074/api/Token?UserName=admin&Password=123
        public IHttpActionResult ValidateLogin(string userName, string password)
        {
            if (userName == "admin" && password == "123")
            {
                return Ok(TokenManager.GenerateToken(userName));
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpGet]
        [CustomAuthenticationFilter]
        public IHttpActionResult GetEmployee()
        {
            return Ok("Successfull");
        }
    }
}
