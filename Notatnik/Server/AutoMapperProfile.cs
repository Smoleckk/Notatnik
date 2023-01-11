using AutoMapper;
using Notatnik.Shared.Dtos.NoteDto;
using Notatnik.Shared.Models;
using System;

namespace Notatnik.Server
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Note, NoteDisplayDto>();
            CreateMap<NoteDisplayDto, Note>();


        }
    }
}
