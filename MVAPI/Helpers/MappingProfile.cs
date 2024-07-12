using AutoMapper;

namespace MVAPI.Helpers
{
    public class MappingProfile :Profile
    {
        public MappingProfile() 
        {

            CreateMap<Movie, MovieDetailsDto>();
            CreateMap<Moviedto, Movie>()
                .ForMember(src => src.Poster, opt => opt.Ignore());

        }
    }
}
