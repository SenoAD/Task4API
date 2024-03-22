using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MyRESTService.BLL.DTOs;
using MyRESTService.Domain.Models;

namespace MyRESTService.BLL.Profiles
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<CategoryCreateDTO, Category>();
            CreateMap<CategoryUpdateDTO, Category>();

            CreateMap<Article, ArticleDTO>().ReverseMap();
            CreateMap<ArticleCreateDTO, Article>();
            CreateMap<ArticleUpdateDTO, Article>();

            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<UserCreateDTO, User>();
            CreateMap<UserUpdateDTO, User>();
            CreateMap<LoginDTO, User>();

            CreateMap<Role, RoleDTO>();
            CreateMap<RoleCreateDTO, Role>();
        }
    }
}
