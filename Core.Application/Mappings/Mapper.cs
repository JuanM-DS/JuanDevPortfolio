using Core.Application.DTOs.CommentReferences;
using Core.Application.DTOs.Experience;
using Core.Application.DTOs.ExperienceDetail;
using Core.Application.DTOs.Profile;
using Core.Application.DTOs.Project;
using Core.Application.DTOs.ProjectImage;
using Core.Application.DTOs.Skill;
using Core.Application.DTOs.TTechnologyItem;
using Core.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace Core.Application.Mappings
{
	[Mapper]
	public static partial class Mapper
	{
		public static partial TTarget Map<TTarget, TSource>(TSource src);

		public static List<TTarget> Map<TTarget, TSource>(List<TSource> src)
		{
			return src.Select(x => Map<TTarget, TSource>(x)).ToList();
		}

		#region CommentReference
		[MapperIgnoreSource(nameof(CommentReference.Created))]
		[MapperIgnoreSource(nameof(CommentReference.CreatedBy))]
		[MapperIgnoreSource(nameof(CommentReference.Updated))]
		[MapperIgnoreSource(nameof(CommentReference.UpdatedBy))]
		[MapperIgnoreSource(nameof(CommentReference.IsConfirmed))]
		[MapperIgnoreSource(nameof(CommentReference.Profile))]
		[MapperIgnoreSource(nameof(CommentReference.AccountId))]
		[MapperIgnoreTarget(nameof(CommentReferenceDTO.PersonName))]
		[MapperIgnoreTarget(nameof(CommentReferenceDTO.ProfileImageUrl))]
		private static partial CommentReferenceDTO Map(CommentReference source);

		[MapperIgnoreTarget(nameof(CommentReference.Created))]
		[MapperIgnoreTarget(nameof(CommentReference.CreatedBy))]
		[MapperIgnoreTarget(nameof(CommentReference.Updated))]
		[MapperIgnoreTarget(nameof(CommentReference.UpdatedBy))]
		[MapperIgnoreTarget(nameof(CommentReference.IsConfirmed))]
		[MapperIgnoreTarget(nameof(CommentReference.Profile))]
		[MapperIgnoreTarget(nameof(CommentReference.Id))]
		private static partial CommentReference Map(SaveCommentReferenceDTO source);
		#endregion

		#region Project
		[MapperIgnoreTarget(nameof(Project.Created))]
		[MapperIgnoreTarget(nameof(Project.CreatedBy))]
		[MapperIgnoreTarget(nameof(Project.Updated))]
		[MapperIgnoreTarget(nameof(Project.UpdatedBy))]
		[MapperIgnoreTarget(nameof(Project.Profile))]
		[MapperIgnoreTarget(nameof(Project.ProfileId))]
		[MapperIgnoreTarget(nameof(Project.Id))]
		private static partial Project Map(SaveProjectDTO source);

		[MapperIgnoreSource(nameof(Project.Created))]
		[MapperIgnoreSource(nameof(Project.CreatedBy))]
		[MapperIgnoreSource(nameof(Project.Updated))]
		[MapperIgnoreSource(nameof(Project.UpdatedBy))]
		[MapperIgnoreSource(nameof(Project.ProjectImages))]
		[MapperIgnoreSource(nameof(Project.Profile))]
		[MapperIgnoreSource(nameof(Project.TechnologyItems))]
		private static partial ProjectDTO Map(Project source);
		#endregion

		#region Profile
		[MapperIgnoreTarget(nameof(Profile.Created))]
		[MapperIgnoreTarget(nameof(Profile.CreatedBy))]
		[MapperIgnoreTarget(nameof(Profile.Updated))]
		[MapperIgnoreTarget(nameof(Profile.UpdatedBy))]
		[MapperIgnoreTarget(nameof(Profile.Id))]
		[MapperIgnoreTarget(nameof(Profile.ComenntReferences))]
		[MapperIgnoreTarget(nameof(Profile.Skills))]
		[MapperIgnoreTarget(nameof(Profile.Experiences))]
		[MapperIgnoreTarget(nameof(Profile.Projects))]
		private static partial Profile Map(SaveProfileDTO source);

		[MapperIgnoreSource(nameof(Profile.Created))]
		[MapperIgnoreSource(nameof(Profile.CreatedBy))]
		[MapperIgnoreSource(nameof(Profile.Updated))]
		[MapperIgnoreSource(nameof(Profile.UpdatedBy))]
		[MapperIgnoreSource(nameof(Profile.ComenntReferences))]
		[MapperIgnoreSource(nameof(Profile.Skills))]
		[MapperIgnoreSource(nameof(Profile.Experiences))]
		[MapperIgnoreSource(nameof(Profile.Projects))]
		private static partial ProfileDTO Map(Profile source);
		#endregion

		#region ProjectImage
		[MapperIgnoreTarget(nameof(ProjectImage.Created))]
		[MapperIgnoreTarget(nameof(ProjectImage.CreatedBy))]
		[MapperIgnoreTarget(nameof(ProjectImage.Updated))]
		[MapperIgnoreTarget(nameof(ProjectImage.UpdatedBy))]
		[MapperIgnoreTarget(nameof(ProjectImage.Project))]
		[MapperIgnoreTarget(nameof(ProjectImage.Id))]
		[MapperIgnoreSource(nameof(SaveProjectImageDTO.ImageFile))]
		private static partial ProjectImage Map(SaveProjectImageDTO source);
		
		[MapperIgnoreSource(nameof(ProjectImage.Created))]
		[MapperIgnoreSource(nameof(ProjectImage.CreatedBy))]
		[MapperIgnoreSource(nameof(ProjectImage.Updated))]
		[MapperIgnoreSource(nameof(ProjectImage.UpdatedBy))]
		[MapperIgnoreSource(nameof(ProjectImage.Project))]
		private static partial ProjectImageDTO Map(ProjectImage source);
		#endregion

		#region Skill
		[MapperIgnoreTarget(nameof(Skill.Created))]
		[MapperIgnoreTarget(nameof(Skill.CreatedBy))]
		[MapperIgnoreTarget(nameof(Skill.Updated))]
		[MapperIgnoreTarget(nameof(Skill.UpdatedBy))]
		[MapperIgnoreTarget(nameof(Skill.Profile))]
		[MapperIgnoreTarget(nameof(Skill.Id))]
		private static partial Skill Map(SaveSkillDTO source);

		[MapperIgnoreSource(nameof(Skill.Created))]
		[MapperIgnoreSource(nameof(Skill.CreatedBy))]
		[MapperIgnoreSource(nameof(Skill.Updated))]
		[MapperIgnoreSource(nameof(Skill.UpdatedBy))]
		[MapperIgnoreSource(nameof(Skill.Profile))]
		[MapperIgnoreSource(nameof(Skill.TechnologyItems))]
		private static partial SkillDTO Map(Skill source);
		#endregion

		#region TechnologyItem
		[MapperIgnoreTarget(nameof(TechnologyItem.Created))]
		[MapperIgnoreTarget(nameof(TechnologyItem.CreatedBy))]
		[MapperIgnoreTarget(nameof(TechnologyItem.Updated))]
		[MapperIgnoreTarget(nameof(TechnologyItem.UpdatedBy))]
		[MapperIgnoreTarget(nameof(TechnologyItem.Id))]
		[MapperIgnoreSource(nameof(SaveTechnologyItemDTO.ImageFile))]
		private static partial TechnologyItem Map(SaveTechnologyItemDTO source);
		
		[MapperIgnoreSource(nameof(TechnologyItem.Created))]
		[MapperIgnoreSource(nameof(TechnologyItem.CreatedBy))]
		[MapperIgnoreSource(nameof(TechnologyItem.Updated))]
		[MapperIgnoreSource(nameof(TechnologyItem.UpdatedBy))]
		private static partial TechnologyItemDTO Map(TechnologyItem source);
		#endregion

		#region WorkExperience
		[MapperIgnoreTarget(nameof(WorkExperience.Created))]
		[MapperIgnoreTarget(nameof(WorkExperience.CreatedBy))]
		[MapperIgnoreTarget(nameof(WorkExperience.Updated))]
		[MapperIgnoreTarget(nameof(WorkExperience.UpdatedBy))]
		[MapperIgnoreTarget(nameof(WorkExperience.Id))]
		[MapperIgnoreTarget(nameof(WorkExperience.Profile))]
		[MapperIgnoreSource(nameof(SaveWorkExperienceDTO.LogoFile))]
		private static partial WorkExperience Map(SaveWorkExperienceDTO source);
		
		[MapperIgnoreSource(nameof(WorkExperience.Created))]
		[MapperIgnoreSource(nameof(WorkExperience.CreatedBy))]
		[MapperIgnoreSource(nameof(WorkExperience.Updated))]
		[MapperIgnoreSource(nameof(WorkExperience.UpdatedBy))]
		[MapperIgnoreSource(nameof(WorkExperience.Profile))]
		[MapperIgnoreSource(nameof(WorkExperience.TechnologyItems))]
		[MapperIgnoreSource(nameof(WorkExperience.ExperienceDetails))]
		private static partial WorkExperienceDTO Map(WorkExperience source);
		#endregion

		#region WorkExperienceDetail
		[MapperIgnoreTarget(nameof(WorkExperienceDetail.Created))]
		[MapperIgnoreTarget(nameof(WorkExperienceDetail.CreatedBy))]
		[MapperIgnoreTarget(nameof(WorkExperienceDetail.Updated))]
		[MapperIgnoreTarget(nameof(WorkExperienceDetail.UpdatedBy))]
		[MapperIgnoreTarget(nameof(WorkExperienceDetail.Id))]
		[MapperIgnoreTarget(nameof(WorkExperienceDetail.Experience))]
		private static partial WorkExperienceDetail Map(SaveWorkExperienceDetailDTO source);
		
		[MapperIgnoreSource(nameof(WorkExperienceDetail.Created))]
		[MapperIgnoreSource(nameof(WorkExperienceDetail.CreatedBy))]
		[MapperIgnoreSource(nameof(WorkExperienceDetail.Updated))]
		[MapperIgnoreSource(nameof(WorkExperienceDetail.UpdatedBy))]
		[MapperIgnoreSource(nameof(WorkExperienceDetail.Experience))]
		private static partial WorkExperienceDetailDTO Map(WorkExperienceDetail source);
		#endregion
	}
}
