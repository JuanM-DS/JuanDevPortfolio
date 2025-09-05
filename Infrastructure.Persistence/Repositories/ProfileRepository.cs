using Core.Application.Interfaces.Repositories;
using Core.Application.QueryFilters;
using Core.Domain.Entities;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class ProfileRepository : BaseRepository<Profile>, IProfileRepository
    {
        public ProfileRepository(MainContext context)
            : base(context)
        { }

		public IEnumerable<Profile> GetAll(ProfileFilter filter)
		{
			var query = _entity.AsQueryable();

			if (!string.IsNullOrWhiteSpace(filter.ProfesionalTitle))
				query = query.Where(x => x.ProfesionalTitle.Contains(filter.ProfesionalTitle));

			if (filter.AccountId is not null)
				query = query.Where(x => x.AccountId == filter.AccountId);

			return query.AsEnumerable();
		}

		public async Task<Profile?> GetByAccountAsync(Guid AccountId)
		{
			return await _entity.FirstOrDefaultAsync(x => x.AccountId == AccountId);
		}

		public async Task<bool> SetResumeToProfile(Guid ProfileId, string cvUrl)
		{
			var profile = await _entity.FindAsync(ProfileId);
			if (profile is null)
				return false;
			profile.CvUrl = cvUrl;
			var result = await _context.SaveChangesAsync();
			return result > 0;
		}
	}
}
