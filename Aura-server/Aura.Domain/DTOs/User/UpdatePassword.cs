namespace Aura.Domain.DTOs.User;
public class UpdatePassword
{
    public string CurrentPassword { get; set; }
    public string NewPassword { get; set; }
    public string ConfirmPassword { get; set; }
}