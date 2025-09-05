using Core.Application.Interfaces.Shared;
using Core.Domain.CommonEntities;
using Microsoft.AspNetCore.Http;
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
			var client = httpProvider.GetCurrentUserId().ToString();
			if (string.IsNullOrEmpty(client))
				client = "Anonimus User";

			foreach (var item in entities)
			{
				switch (item.State)
				{
					case EntityState.Modified:
						item.Entity.UpdatedBy = client;
						item.Entity.Updated = DateTime.UtcNow;
						break;
					case EntityState.Added:
						item.Entity.CreatedBy = client;
						item.Entity.Created = DateTime.UtcNow;
						break;
				}
			}
			return base.SavingChangesAsync(eventData, result, cancellationToken);
		}
	}
}
