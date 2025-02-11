namespace CreationHub.Models;

public class Rating
{
    public long Id { get; set; }
    public long NicePartUsageId { get; set; }
    public int? Creativity { get; set; }

    public int? Uniqueness { get; set; }
}