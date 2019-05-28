using AutoMapper;
using DataAccessLayer.Entities;
using DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WcfServiceLibrary.Helpers
{
    static class MapperConfigurator
    {
        static MapperConfigurator()
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
                cfg.CreateMap<UserEntity, UserDTO>();
                cfg.CreateMap<UserDTO, UserEntity>();
                cfg.CreateMap<HistoryEventEntity, HistoryEventDTO>();
            });
        }
    }
}
