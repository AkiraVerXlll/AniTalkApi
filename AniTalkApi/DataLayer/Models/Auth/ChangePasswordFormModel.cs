namespace AniTalkApi.DataLayer.Models.Auth;

public struct ChangePasswordFormModel
{
    public string? OldPassword { get; set; }

    public string? NewPassword { get; set; }
}