namespace Core.Application.Interfaces.Helpers
{
	public interface IMapper
    {
        public TResult? Map<TResult, TSource>(TSource source);
        public List<TResult>? Map<TResult, TSource>(List<TSource> source);
    }
}
