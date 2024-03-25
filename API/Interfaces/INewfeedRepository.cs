using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace API.Interfaces
{
    public interface INewfeedRepository
    {
        Task<NewFeed> GetNewFeedById(int id);
        Task<AppUser> GetNewFeedsByUserNameAsync(string username);
        void AddNewFeed(NewFeed newFeed);
        void UpdateNewFeed(NewFeed newFeed);
        void DeleteNewFeed(NewFeed newFeed);
         Task<PagedList<NewFeedDto>> GetAllNewFeeds(NewfeedParams newfeedParams);
    }
}