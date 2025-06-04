using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseMAUI.Settings.Mapper.Profiles
{
    public class DemoProfile : Profile
    {

        public DemoProfile()
        {
            /*  CreateMap<ProjectModel, Project>()
               .ForMember(dest => dest.Active, src => src.MapFrom(data => data.IsSelected))
               .ReverseMap();*/

            //CreateMap<CustomerModel, CustomerEntity>().ReverseMap();
        }
    }
}
