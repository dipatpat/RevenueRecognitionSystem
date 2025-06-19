using RevenueRecognitionSystem.Shared.Enums;

namespace RevenueRecognitionSystem.Auth;

public class RegisterRequest
{
    public string Email { get; set; }
    public string Login { get; set; }  
    public string Password { get; set; }
    public EmployeeRole Role { get; set; }  
}
