namespace Domain.User;

public class User
{
    public long Id { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? Role { get; set; }
}