﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ZaylandShop.ServiceTemplate.Storage
{
    /// <summary>
    /// Класс для применения миграций БД
    /// </summary>
    public static class MigrationsRunner
    {
        public static async Task ApplyMigrations(ILogger logger, IServiceProvider serviceProvider, string appName)
        {
            var operationId = Guid.NewGuid().ToString().Substring(0, 4);
            logger.LogInformation($"{appName}:UpdateDatabase:{operationId}: starting...");
            try
            {
                using (var serviceScope = serviceProvider.CreateScope())
                {
                    var dbContext = serviceScope.ServiceProvider.GetService<AppDbContext>();
                    await dbContext.Database.MigrateAsync();
                }

                logger.LogInformation($"{appName}:UpdateDatabase:{operationId}: successfully done");
                await Task.FromResult(true);
            }
            catch (Exception exception)
            {
                logger.LogCritical(exception, $"{appName}:UpdateDatabase.{operationId}: Migration failed");
                throw;
            }
        }
    }
}
