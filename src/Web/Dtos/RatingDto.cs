namespace CreationHub.Web.Dtos;

public class RatingDto
{
    public long Id { get; set; }

    public int? Creativity { get; set; }

    public int? Uniqueness { get; set; }
    
    public long NicePartUsageId { get; set; }
}