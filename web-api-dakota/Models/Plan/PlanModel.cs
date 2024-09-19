namespace web_api_dakota.Models.Plan;

public class PlanModel
{
    public int Id {get;set;}
    
    public double Price { get; set; }
    
    public DateTime Time { get; set; }

    public PlanModel(){}
    
    public PlanModel(PlanRequestDTO request)
    {
        this.Price = request.Price;
        
        this.Time = request.Time;
    }
    
}