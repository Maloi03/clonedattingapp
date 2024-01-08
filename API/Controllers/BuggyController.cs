using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class BuggyController : BaseApiController //dat bo dieu khien de ke thua va nhan cac phan hoi tra ve tu API
    {
        private readonly DataContext _context;

        public BuggyController(DataContext context)  //tao tham so va tu truong du lieu
        {
            _context = context;
        }

        [Authorize]     // nhan su tra ve xac thuc cua nguoi dung
        [HttpGet("auth")]  // tao httpget tra ve xac thuc auth tu API
        public ActionResult<string> GetSecret()
        {
            return "secret text";  //tra ve ket qua xac nhan
        }

        [HttpGet("not-found")]   //tra ve khong tim thay nguoi dung tu API la httpGet("auth")
        public ActionResult<AppUser> GetNotFound()  // tao phuong thuc hanh dong tra ve ket qua tim kiem tu API co tham so la AppUser
        {
            var thing = _context.Users.Find(-1);   //khai bao bien thing kieu var tim kiem nguoi dung vs tham so la -1

            if (thing == null) return NotFound();  // neu thing = null tra ve notfound

            return Ok(thing);  // neu tim thay nguoi dung tra ve ok 
        }

        [HttpGet("server-error")]   //tra ve loi server error tu API 
        public ActionResult<string> GetServerError()    // tao phuong thuc tra ve co kieu du lieu string
        {
            
                var thing = _context.Users.Find(-1);

                var thingToReturn = thing.ToString();   //khai bao du lieu var vs ten la thingtoreturn

                return thingToReturn;     //tra ve
        }

        [HttpGet("bad-request")]   // tra ve yeu cau xau tu API
        public ActionResult<string> GetBadRequest()
        {
            return BadRequest();
        }
    }
}
