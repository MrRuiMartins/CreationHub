namespace CreationHub.Models;

public class RatingDto
{
    public long Id { get; set; }

    public int? Creativity { get; set; }

    public int? Uniqueness { get; set; }
    
    public int NicePartUsageId { get; set; }
}