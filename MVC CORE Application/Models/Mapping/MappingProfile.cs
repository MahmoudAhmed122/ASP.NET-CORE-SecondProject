using AutoMapper;
using MVC_CORE.DDL.Entities;
using MVC_CORE_Application.Models.ViewModel;

namespace MVC_CORE_Application.Models.Mapping
{
    public class MappingProfile:Profile
    {

        public MappingProfile() {

            CreateMap<Category, CategoryViewModel>().ReverseMap();
            CreateMap<BookViewModel , Book>().ReverseMap();

        }
    }
}
