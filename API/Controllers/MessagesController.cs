using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class MessagesController : BaseApiController   // ke thua va nhan cac phan hoi tra ve API
    {
        // tao tham so va truong du lieu
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;
        public MessagesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }

        // [HttpPost]   // gui du lieu len may chu
        // public async Task<ActionResult<MessageDto>> CreateMessage(CreateMessageDto createMessageDto)
        // {
        //     var username = User.GetUsername();    // khai bao bien username
        //     if (username == createMessageDto.RecipientUsername.ToLower())  // neu username bằng tên người nhận => trả về badrequest
        //         return BadRequest("You cannot send messages to yourself");

        //     var sender = await _userRepository.GetUserByUsernameAsync(username);   
        //     var recipient = await _userRepository.GetUserByUsernameAsync(createMessageDto.RecipientUsername);

        //     if (recipient == null) return NotFound();
        //     var message = new Message
        //     {
        //         Sender = sender,
        //         Recipient = recipient,
        //         SenderUserName = sender.UserName,
        //         RecipientUsername = recipient.UserName,
        //         Content = createMessageDto.Content
        //     };
        //     _messageRepository.AddMessage(message);
        //     if (await _messageRepository.SaveAllAsync()) return Ok(_mapper.Map<MessageDto>(message));
        //     return BadRequest("Failed to send message");

        // }
        [HttpGet]   //lay du lieu tu csdl
        public async Task<ActionResult<IEnumerable<MessageDto>>> GetMessagesForUser([FromQuery] MessageParams messageParams)
        {
            messageParams.Username = User.GetUsername();
            var messages = await _unitOfWork.MessageRepository.GetMessagesForUser(messageParams);
            Response.AddPaginationHeader(new PaginationHeader(messages.CurrentPage, messages.PageSize, messages.TotalCount, messages.TotalPages));
            return messages;
        }
        // [HttpGet("thread/{username}")]
        // public async Task<ActionResult<IEnumerable<MessageDto>>> GetMessageThread(string username)
        // {
        //     var currentUsername = User.GetUsername();
        //     return Ok(await _messageRepository.GetMessageThread(currentUsername, username));
        // }

        [HttpDelete("{id}")]   // xoa thong tin la id tren may chu
        public async Task<ActionResult> DeleteMessage(int id)
        {
            var username = User.GetUsername();

            var message = await _unitOfWork.MessageRepository.GetMessage(id);
            
            if (message.Sender.UserName != username && message.Recipient.UserName != username)
                return Unauthorized();

            if (message.Sender.UserName == username) message.SenderDeleted = true;

            if (message.Recipient.UserName == username) message.RecipientDeleted = true;

            if (message.SenderDeleted && message.RecipientDeleted)
                _unitOfWork.MessageRepository.DeleteMessage(message);

            if (await  _unitOfWork.Complete()) return Ok();

            return BadRequest("Problem deleting the message");

        }
    }
}