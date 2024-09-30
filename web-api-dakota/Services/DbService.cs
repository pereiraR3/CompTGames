using Microsoft.EntityFrameworkCore;
using web_api_dakota.Data;
using web_api_dakota.Services.Interfaces;

namespace web_api_dakota.Services;

public class DbService : IDbService
{
    
    private readonly WebDakotaContext _dbContext;

    public DbService(WebDakotaContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<bool> InsertRolesInDatabase()
    {
        string sqlVerify = "SELECT COUNT(*) FROM roles";
        
        int countRows = await _dbContext.Database.ExecuteSqlRawAsync(sqlVerify);
        
        if(countRows != 2)
            return false;
        
        // SQL to insert multiple values, one per row
        string sql = "INSERT INTO roles (role_name) VALUES ('Admin'), ('Client')";

        // Executes the SQL command asynchronously
        int rowsAffected = await _dbContext.Database.ExecuteSqlRawAsync(sql);

        // Checks if any row was affected
        return rowsAffected > 0;
    }
    
}