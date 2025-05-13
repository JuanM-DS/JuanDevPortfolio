using Core.Application.DTOs.Profile;
using Core.Domain.Entities;

namespace Core.Application.Interfaces.Services
{
    public interface IProfileServices : IBaseServices<Profile, ProfileDTO, SaveProfileDTO> { }

}
