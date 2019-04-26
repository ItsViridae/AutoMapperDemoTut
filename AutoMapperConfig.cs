using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;

namespace AutoMapperDemo
{
    //Wheere ever in your Code you need to map objects use:
    //                                  Mapping to an Existing Instance Below
    //                                  var destination = Mapping.Mapper.Map<Destination>(yourSourceInstance);
    //
    //-----------------------------   The Goal here is to build a Mapper that is Independant, From the Consumer....
    //-----------------------------    ie: I coulda just made it public and configure there but this is Sexier way!
    //------------> ALWAYS IMPLEMENT TO THE INTERFACE
    public static class AutoMapperConfig
    {
        private static readonly Lazy<IMapper> Lazy = new Lazy<IMapper>(() =>
        {
            var configure = new MapperConfiguration(config => { 
                //To Ensure that internal properties are also mapped over.
                config.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
                config.AddProfile<MappingProfile>();
            });
            var mapper = configure.CreateMapper();
            return mapper;
        });
        
        public static IMapper Mapper => Lazy.Value;
    }

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //EXAMPLE:
            //CreateMap<SourceClass, DestinationClass>();
            CreateMap<Book, BookViewModel>();   // 1-WayMapping
            //Creating a Map with two sided mapping
            CreateMap<Book, BookViewModel>().ReverseMap(); // 2-WayMap!
            //If the Properties share the same Type and Nme they will be
            //mapped by Convention when using Automapper
            
            //Pojected Properties
            //CreateMap<Book, BookViewModel>()
            //    .ForMember(dest => dest.BookTitle,
            //        opts => opts.MapFrom(src => src.Title));

            //Complex Projections:
            //ResidentDTO => Address
            CreateMap<ResidentDTO, Resident>()
                .ForMember(dest => dest.Address,
                    opts => opts.MapFrom(
                        src => new Address
                        {
                            Street = src.Street,
                            City = src.City,
                            State = src.State,
                            ZipCode = src.ZipCode
                        }));



        } //End of Additional Mappings
    }
}
