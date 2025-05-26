using Core.Application.Interfaces.Shared;
using Core.Domain.CommonEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Infrastructure.Persistence.Context.Interceptors
{
	public class SaveAuditablePropertiesInterceptor : SaveChangesInterceptor
	{
		private readonly IHttpContextProvider httpProvider;

		public SaveAuditablePropertiesInterceptor(IHttpContextProvider httpProvider)
		{
			this.httpProvider = httpProvider;
		}
		public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
		{
			var context = eventData.Context;
			if(context is null)
				return base.SavingChangesAsync(eventData, result, cancellationToken);

			var entities = context.ChangeTracker.Entries<IAuditableProperties>();

			foreach (var item in entities)
			{
				switch (item.State)
				{
					case EntityState.Modified:
						item.Entity.UpdatedBy = httpProvider.GetCurrentUserId().ToString() ?? "Anonimus User";
						item.Entity.Updated = DateTime.UtcNow;
						break;
					case EntityState.Added:
						item.Entity.CreatedBy = httpProvider.GetCurrentUserId().ToString() ?? "Anonimus User";
						item.Entity.Created = DateTime.UtcNow;
						break;
				}
			}
			return base.SavingChangesAsync(eventData, result, cancellationToken);
		}
	}
}
