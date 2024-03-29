﻿using API.DTOs;
using API.Entities;
using API.Extensions;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AppUser, MemberDTO>()
                .ForMember(dest => dest.PhotoUrl, opt =>
             opt.MapFrom(src => src.Photos.FirstOrDefault(x => x.IsMain).Url))
            .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.DateOfBirth.CalculateAge()));
            CreateMap<Photo, PhotoDto>();
            CreateMap<MemberUpdateDto, AppUser>();
            _ = CreateMap<NewFeed, NewFeedDto>()
        .ForMember(dest => dest.CreatorPhotoUrl, opt => opt.MapFrom(src => src.Creator.Photos.FirstOrDefault(x => x.IsMain).Url))
         ;
            CreateMap<RegisterDTO, AppUser>();
            CreateMap<Message, MessageDto>()
                .ForMember(dest => dest.SenderPhotoUrl, opt => opt.MapFrom(src =>
                    src.Sender.Photos.FirstOrDefault(x => x.IsMain).Url))
                    .ForMember(dest => dest.RecipientPhotoUrl, opt => opt.MapFrom(src =>
                    src.Recipient.Photos.FirstOrDefault(x => x.IsMain).Url));   
        }
    }
}
