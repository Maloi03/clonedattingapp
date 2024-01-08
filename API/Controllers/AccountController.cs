using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class AccountController : BaseApiController  // truyen du lieu vao bo dieu khien
    {
        private readonly DataContext _context;  // thiet lap bo dieu khien du lieu
        private readonly ITokenService _tokenService;  //dua itokenservice vao accountcontroller
        private readonly IMapper _mapper;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;

        public AccountController(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager, ITokenService tokenService, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        [HttpPost("register")] // tao duong dan den diem cuoi dang ki
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO registerDto) // dung phuong thuc khong dong bo va dung registerdto lam tham so truyen du lieu
        {
            if (await UserExists(registerDto.Username)) return BadRequest("Username is taken"); //kiem tra ten nguoi dung da co hay chua

            var user = _mapper.Map<AppUser>(registerDto);  //neu k lay duoc, phuong thuc se tao 1 ban moi de bam muoi mat khau nguoi dung

            user.UserName = registerDto.Username.ToLower();

            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded) return BadRequest(result.Errors);

            var roleResult = await _userManager.AddToRoleAsync(user, "Member");

            if (!roleResult.Succeeded) return BadRequest(result.Errors);

            return new UserDTO
            {
                Username = user.UserName,
                Token = await _tokenService.CreateToken(user),
                KnownAs = user.KnownAs,
                Gender = user.Gender
            };
        }

        [HttpPost("login")] // tao duong dan yeu cau den diem cuoi
        public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDto)
        {
            var user = await _userManager.Users
            .Include(p => p.Photos)
            .SingleOrDefaultAsync(x => x.UserName == loginDto.Username.ToLower()); // truy xuat nguoi dung vs ten nguoi dung chi dinh tu csdl

            if (user == null) return Unauthorized("Invalid username");

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded) return Unauthorized();

            return new UserDTO
            {
                Username = user.UserName,
                Token = await _tokenService.CreateToken(user),
                PhotoUrl = user.Photos.FirstOrDefault(x => x.IsMain)?.Url,
                KnownAs = user.KnownAs,
                Gender = user.Gender
            };
        }
        private async Task<bool> UserExists(string username) //tao phuong thuc tro giup va dat no o che do rieng tu de ten nguoi dung trong csdl la duy nhat va su dung ten nguoi dung cho nhieu viec khac nhau
        {
            return await _userManager.Users.AnyAsync(x => x.UserName == username.ToLower());
        }

    }
}
