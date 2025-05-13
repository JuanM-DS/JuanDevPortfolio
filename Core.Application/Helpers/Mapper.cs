using Core.Application.Interfaces.Helpers;
using System.Security.Cryptography;

namespace Core.Application.Helpers
{
    public class Mapper : IMapper
    {
        private readonly Dictionary<(Type outout, Type input), Func<object, object>> _mappingProfileDictionary;

        public Mapper()
        {
            _mappingProfileDictionary = new();
        }

        public TResult? Handler<TResult, TSource>(TSource source)
        {
            try
            {
                if(source is not null)
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

        public void AddMappingProfile<TResult, TSource>(Func<object, object> func)
        {
            _mappingProfileDictionary.Add((typeof(TResult), typeof(TSource)), func);
        }
    }
}
