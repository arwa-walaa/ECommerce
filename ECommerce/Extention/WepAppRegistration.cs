using ECommercePersistence.IdentityData.DBContext;
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

        public static async Task<WebApplication> MigrateIDentityDB(this WebApplication app)
        {
            await using var scope = app.Services.CreateAsyncScope();
            var dbContextService = scope.ServiceProvider.GetRequiredService<StoreIdentityDBContext>();
            var pendingMigration = await dbContextService.Database.GetPendingMigrationsAsync();
            if (pendingMigration.Any())
            {
                await dbContextService.Database.MigrateAsync();
            }
            return app;

        }

        public static async Task<WebApplication> SeedDb(this WebApplication app)
        {
            await using var scope = app.Services.CreateAsyncScope();
            var dataInitilizer = scope.ServiceProvider.GetRequiredKeyedService<ECommerceDomain.Contarcts.IDataInitilizer>("Default");
             await dataInitilizer.InitilizeAsync();
            return app;


        }
        public static async Task<WebApplication> SeedIdentityDb(this WebApplication app)
        {
            await using var scope = app.Services.CreateAsyncScope();
            var dataInitilizer = scope.ServiceProvider.GetRequiredKeyedService<ECommerceDomain.Contarcts.IDataInitilizer>("Identity");
            await dataInitilizer.InitilizeAsync();
            return app;


        }
    }
}
