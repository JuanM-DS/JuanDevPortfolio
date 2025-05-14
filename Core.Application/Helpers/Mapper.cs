using Core.Application.DTOs.Profile;
using Core.Application.Interfaces.Helpers;
using Core.Domain.Entities;

namespace Core.Application.Helpers
{
    public class Mapper : IMapper
    {
        private Dictionary<(Type outout, Type input), Func<object, object>> _mappingProfileDictionary;

        public Mapper()
        {
            _mappingProfileDictionary = new();
            AddMappingProfile();
        }

        public TResult? Handler<TResult, TSource>(TSource source)
        {
            try
            {
                if (source is not null)
                {
                    if (_mappingProfileDictionary.TryGetValue((typeof(TResult), typeof(TSource)), out var func))
                    {
                        return (TResult)func(source);
                    }
                }

                return default;
            }
            catch (Exception)
            {
                return default;
            }
        }

        private void AddMappingProfile()
        {
            _mappingProfileDictionary = new Dictionary<(Type outout, Type input), Func<object, object>>()
            {
                {
                    (typeof(Profile), typeof(ProfileDTO)), src =>
                    {
                        var source = (ProfileDTO)src;
                        return new Profile()
                        {
                            CvUrl = source.CvUrl,
                            Description = source.Description,
                            GitHubRepositoryUrl = source.GitHubRepositoryUrl,
                            Id = source.Id,
                            LinkedinUrl = source.LinkedinUrl,
                            Name = source.Name,
                            ProfesionalTitle = source.ProfesionalTitle,
                            ProfileImageUrl = source.ProfileImageUrl
                        };

                    }
                },
                {
                    (typeof(ProfileDTO), typeof(Profile)), src =>
                    {
                        var source = (Profile)src;

                        return new ProfileDTO(
                            source.Id,
                            source.Name,
                            source.ProfesionalTitle,
                            source.Description,
                            source.ProfileImageUrl,
                            source.GitHubRepositoryUrl,
                            source.LinkedinUrl,
                            source.CvUrl);
                    }
                }
            };
        }
    }
}
