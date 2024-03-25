using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace API.Data
{
    public class NewfeedRepository : INewfeedRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public NewfeedRepository(DataContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PagedList<NewFeedDto>> GetAllNewFeeds(NewfeedParams newfeedParams)
        {
            var _query = _context.NewFeeds.AsQueryable();

            return await PagedList<NewFeedDto>.CreateAsync(_query.AsNoTracking()
                .ProjectTo<NewFeedDto>(_mapper.ConfigurationProvider),
                newfeedParams.PageNumber, newfeedParams.PageSize);

        }

        public async Task<NewFeed> GetNewFeedById(int id)
        {
            return await _context.NewFeeds.FindAsync(id);
        }

        public async Task<AppUser> GetNewFeedsByUserNameAsync(string username)
        {
            return await _context.Users
                .Include(feed => feed.UpContents)
                .FirstOrDefaultAsync(x => x.UserName == username);
        }

        public void AddNewFeed(NewFeed newFeed)
        {
            //var news = _mapper.Map<NewFeed>(newFeed);
            _context.NewFeeds.Add(newFeed);
            
        }

        public void UpdateNewFeed(NewFeed newFeed)
        {
            _context.Entry(newFeed).State = EntityState.Modified;
        }

        public void DeleteNewFeed(NewFeed newFeed)
        {
                _context.NewFeeds.Remove(newFeed);
        }

    }

}