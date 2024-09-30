namespace web_api_dakota.Services.Interfaces;

/**
 * This interface serves to execute features schedule in database 
 */

public interface IDbService
{

    Task<bool> InsertRolesInDatabase();

}