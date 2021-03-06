﻿using AutoMapper;
using DataAccessLayer.Entities;
using DataTransferObjects;

namespace WcfServiceLibrary.Helpers
{
    public static class MapperConfigurator
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<UserDTO, UserEntity>();
                cfg.CreateMap<UserEntity, UserDTO>();
                cfg.CreateMap<BoardDTO, BoardEntity>();
                cfg.CreateMap<BoardEntity, BoardDTO>();
                cfg.CreateMap<CardDTO, CardEntity>();
                cfg.CreateMap<CardEntity, CardDTO>();
                cfg.CreateMap<ColumnDTO, ColumnEntity>();
                cfg.CreateMap<ColumnEntity, ColumnDTO>();
                cfg.CreateMap<TagEntity, TagDTO>();
                cfg.CreateMap<TagDTO, TagEntity>();
                cfg.CreateMap<CommentEntity, CommentDTO>().ForMember(d => d.Login, s => s.MapFrom(x => x.Author.Login));
                cfg.CreateMap<CommentDTO, CommentEntity>();
                cfg.CreateMap<HistoryEventEntity, HistoryEventDTO>();
            });
        }
    }
}
