using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Claims;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    public class NewfeedController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public NewfeedController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public async Task<ActionResult<PagedList<NewFeedDto>>> GetNewfeeds([FromQuery] NewfeedParams newfeedParams)
        {
           return await _unitOfWork.NewfeedRepository.GetAllNewFeeds(newfeedParams);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<NewFeed>> GetNewfeedById(int id)
        {
            var newfeed = await _unitOfWork.NewfeedRepository.GetNewFeedById(id);
            if (newfeed == null)
            {
                return NotFound();
            }

            return Ok(newfeed);
        }

        [HttpPost]
        public async Task<ActionResult<NewFeed>> CreateNewFeed([FromBody] NewFeed newFeed) ///CreateNewfeedDto createNewfeedDto
         {
            var user = await _unitOfWork.UserRepository.GetUserByUsernameAsync(User.GetUsername());
            
            _unitOfWork.NewfeedRepository.AddNewFeed(newFeed);
            if (await _unitOfWork.Complete()) return Ok(newFeed); //NoContent();
            return BadRequest("Failed to post new feed");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNewFeed(int id, NewFeedDto newFeed, string user )
        {
            var username = User.GetUsername();
            //var sourceUser = await _unitOfWork.NewfeedRepository.GetNewFeedsByUserNameAsync(user);
            var dbpost = await _unitOfWork.NewfeedRepository.GetNewFeedById(id);
            if (!dbpost.Id.Equals(id))
            {
                return NotFound();
            }
            if (dbpost.CreatorUserName.Equals(username)) // thieu ! dang khong biet fix
            {
                return Forbid(); // Không cho phép người dùng sửa bài viết không phải của họ
            }

            // Cập nhật các trường của bài viết từ newFeed
            dbpost.Content = newFeed.Content;
            dbpost.Photos = newFeed.Photos;
            dbpost.Feeling = newFeed.Feeling;
            dbpost.lastModifiedTime = newFeed.lastModifiedTime = DateTime.UtcNow;
            _unitOfWork.NewfeedRepository.UpdateNewFeed(dbpost);
            if (await _unitOfWork.Complete()) return Ok(newFeed); //NoContent();
            return BadRequest("Failed to post new feed");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteNewFeed(int id)
        {
            var dbpost = await _unitOfWork.NewfeedRepository.GetNewFeedById(id);
            if (!dbpost.Id.Equals(id))
            {
                return NotFound();
            }
            _unitOfWork.NewfeedRepository.DeleteNewFeed(dbpost);
            return NoContent();
        }

    }
}