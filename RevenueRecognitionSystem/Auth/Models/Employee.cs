namespace RevenueRecognitionSystem.Auth;

public class Employee
{
    public int Id { get; set; }
    public string Login { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }     
    public string Salt { get; set; }
    public string Role { get; set; }         
    public string RefreshToken { get; set; }
    public DateTime? RefreshTokenExp { get; set; }
}
