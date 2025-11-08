using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ECommerce.Extention
{
    public static class WepAppRegistration
    {
        public static async Task<WebApplication>  MigrateDB(this  WebApplication app )
        {
            await using var scope = app.Services.CreateAsyncScope();
            var dbContextService = scope.ServiceProvider.GetRequiredService<ECommercePersistence.Data.DbContext.StoreDbContext>();
            var pendingMigration = await dbContextService.Database.GetPendingMigrationsAsync();
            if (pendingMigration.Any()) {
                await dbContextService.Database.MigrateAsync();
            }
            return app;

        }

        public static async Task<WebApplication> SeedDb(this WebApplication app)
        {
            await using var scope = app.Services.CreateAsyncScope();
            var dataInitilizer = scope.ServiceProvider.GetRequiredService<ECommerceDomain.Contarcts.IDataInitilizer>();
             await dataInitilizer.InitilizeAsync();
            return app;


        }

    }
}
