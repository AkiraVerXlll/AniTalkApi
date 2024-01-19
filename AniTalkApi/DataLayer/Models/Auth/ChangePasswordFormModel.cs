namespace AniTalkApi.DataLayer.Models.Auth;

public class ChangePasswordFormModel
{
    public string? OldPassword { get; set; }

    public string? NewPassword { get; set; }
}