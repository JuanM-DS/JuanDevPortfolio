﻿using Core.Application.DTOs.Project;
using Core.Application.QueryFilters;
using Core.Application.Wrappers;
using Core.Domain.Entities;

namespace Core.Application.Interfaces.Services
{
	public interface IProjectServices : IBaseServices<Project, ProjectDTO, SaveProjectDTO>
    {
		public Task<AppResponse<Empty>> AddTechnologyItemsAsync(Guid ProjectId, List<Guid> itemsId);

		public AppResponse<List<ProjectDTO>> GetAll(ProjectFilter filter);
	}
}
