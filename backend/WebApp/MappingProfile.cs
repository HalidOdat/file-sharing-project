using AutoMapper;
using Domain;
using Domain.Identity;

namespace WebApp;

public class MappingProfile : Profile {
    public MappingProfile() {
        CreateMap<FileModel, FileModelDto>();
    }
}
