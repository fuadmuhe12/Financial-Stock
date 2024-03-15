using api.Data;
using Microsoft.EntityFrameworkCore;

static class DatabaseMigrate
{
    public static async Task MigrateDbAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        FinanceContext dbContext = scope.ServiceProvider.GetRequiredService<FinanceContext>();
        await dbContext.Database.MigrateAsync();
    }
}
