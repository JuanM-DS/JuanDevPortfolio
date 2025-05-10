using Core.Application.Interfaces.Services;
using Core.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Diagnostics;

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
						item.Entity.UpdatedBy = httpProvider.GetCurrentUserName() ?? "Anonimus User";
						item.Entity.Updated = DateTime.UtcNow;
						break;
					case EntityState.Added:
						item.Entity.CreatedBy = httpProvider.GetCurrentUserName() ?? "Anonimus User";
						item.Entity.Created = DateTime.UtcNow;
						break;
				}
			}
			return base.SavingChangesAsync(eventData, result, cancellationToken);
		}
	}
}
