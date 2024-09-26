using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using web_api_dakota.Models.AI;

namespace web_api_dakota.Models.Plan;

[Table("plans")]
public class PlanModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id {get; private set;}
    
    [Required(ErrorMessage = "The field AiId is required")]
    [Column("id_ai")]
    public int AiId {get; private set;}
    
    [Required(ErrorMessage = "Price is required")]
    [Column("price")]
    public double Price { get; private set; }
    
    [Required(ErrorMessage = "Time is required")]
    [Column("time")]
    public int Time { get; private set; }

    [ForeignKey("AiId")]
    public virtual AiModel Ai { get; private set; }
    
    public PlanModel(){}
    
    public PlanModel(PlanRequestDTO request, AiModel ai)
    {
        this.Price = request.Price;
        this.Time = request.Time;
        this.Ai = ai;
    }
    
    public void SetPrice(double price)
    {
        if (price <= 0)
        {
            throw new ArgumentException("Price must be greater than zero.");
        }

        Price = price;
    }

    public void SetTime(int time)
    {
        if (time > 0)
        {
            throw new ArgumentException("Invalid time value.");
        }

        Time = time;
    }

    public void SetAiModel(AiModel ai)
    {
        if (ai == null)
        {
            throw new ArgumentNullException(nameof(ai), "AI model cannot be null.");
        }

        Ai = ai;
        AiId = ai.Id;
    }
    
    
}