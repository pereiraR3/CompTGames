using web_api_dakota.Services.Interfaces;

namespace web_api_dakota.Utils.Scheduled;

public class ScheduledTaskDbService : BackgroundService
{
    
    private readonly ILogger<ScheduledTaskDbService> _logger;
    private readonly IServiceScopeFactory _serviceScopeFactory;    
    
    public ScheduledTaskDbService(ILogger<ScheduledTaskDbService> logger, IServiceScopeFactory serviceScopeFactory)
    {
        _logger = logger;
        _serviceScopeFactory = serviceScopeFactory;
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Scheduled Task Service started.");
        
        // Aguarda 10 segundos antes de executar a primeira vez
        await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
        
        ExecuteTaskRoles();
        
    }
    
    private async void ExecuteTaskRoles()
    {
        using (var scope = _serviceScopeFactory.CreateScope())
        {
            var dbService = scope.ServiceProvider.GetRequiredService<IDbService>();

            await dbService.InsertRolesInDatabase();
        }
    }
    
}