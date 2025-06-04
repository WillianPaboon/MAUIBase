using AutoMapper;
using BaseMAUI.Settings.Mapper.Profiles;

namespace BaseMAUI.Settings.Mapper
{
    public static class AutoMapperConfig
    {
        public static IMapper Initialize()
        {
            MapperConfiguration mapperConfiguration = new MapperConfiguration(config =>
            {
                config.AddProfile(new DemoProfile());
                // Puedes agregar más perfiles si es necesario
            });

            return mapperConfiguration.CreateMapper();
        }
    }
}
