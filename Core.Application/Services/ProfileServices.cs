using Core.Application.DTOs.Profile;
using Core.Application.Interfaces.Helpers;
using Core.Application.Interfaces.Repositories;
using Core.Application.Interfaces.Services;
using Core.Domain.Entities;

namespace Core.Application.Services
{
    public class ProfileServices : BaseServices<Profile, ProfileDTO, SaveProfileDTO>, IProfileServices
    {
        public ProfileServices(IProfileRepository repo, IMapper mapper)
            : base(repo, mapper)
        { }
    }
}
