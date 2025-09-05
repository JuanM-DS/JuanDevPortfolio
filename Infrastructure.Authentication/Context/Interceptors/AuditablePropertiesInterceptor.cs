using Core.Application.Interfaces.Shared;
using Core.Domain.CommonEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Infrastructure.Authentication.Context.Interceptors
{
	public class AuditablePropertiesInterceptor : SaveChangesInterceptor
	{
		private readonly IHttpContextProvider httpContext;

		public AuditablePropertiesInterceptor(IHttpContextProvider httpContext)
		{
			this.httpContext = httpContext;
		}
		public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
		{
			var context = eventData.Context;
			if (context is null)
				return base.SavingChangesAsync(eventData, result, cancellationToken);

			var entities = context.ChangeTracker.Entries<IAuditableProperties>();
			var client = httpContext.GetCurrentUserId().ToString();
			if (string.IsNullOrEmpty(client))
				client = "Default User";

			foreach (var item in entities)
			{
				switch (item.State)
				{
					case EntityState.Modified:
						item.Entity.Updated = DateTime.UtcNow;
						item.Entity.UpdatedBy = client;
						break;
					case EntityState.Added:
						item.Entity.Created = DateTime.UtcNow;
						item.Entity.CreatedBy = client;
						break;
					default:
						break;
				}
			}
			return base.SavingChangesAsync(eventData, result, cancellationToken);
		}
	}
}
