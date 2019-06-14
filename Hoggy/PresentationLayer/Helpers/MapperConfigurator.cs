using AutoMapper;
using DataTransferObjects;
using PresentationLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Helpers
{
    public static class MapperConfigurator
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<UserDTO, UserModel>();
                cfg.CreateMap<BoardDTO, BoardModel>();
                cfg.CreateMap<ColumnDTO, ColumnModel>();
                cfg.CreateMap<CardDTO, CardModel>();
                cfg.CreateMap<TagDTO, TagModel>();
                cfg.CreateMap<InvitationDTO, InvitationModel>();
            });
        }
    }
}
