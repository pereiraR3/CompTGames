namespace web_api_dakota.Models.Plan;

public record PlanUpdateDTO(
    
    int Id,
    
    int AiId,
    
    double Price, 
    
    int Time
);