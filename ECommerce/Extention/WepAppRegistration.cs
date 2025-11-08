using Microsoft.EntityFrameworkCore;

namespace ECommerce.Extention
{
    public static class WepAppRegistration
    {
        public static WebApplication  MigrateDB(this  WebApplication app )
        {
            using var scope = app.Services.CreateScope();
            var dbContextService = scope.ServiceProvider.GetRequiredService<ECommercePersistence.Data.DbContext.StoreDbContext>();
            if (dbContextService.Database.GetPendingMigrations().Any())
            {
                dbContextService.Database.Migrate();
            }
            return app;

        }

        public static WebApplication SeedDb(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var dataInitilizer = scope.ServiceProvider.GetRequiredService<ECommerceDomain.Contarcts.IDataInitilizer>();
            dataInitilizer.Initilize();
            return app;


        }

    }
}
