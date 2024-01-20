using AniTalkApi.CRUD;
using AniTalkApi.DataLayer.DbModels;
using AniTalkApi.DataLayer.DbModels.Enums;
using AniTalkApi.DataLayer.Settings;
using Microsoft.AspNetCore.Identity;

namespace AniTalkApi.ServiceLayer.AuthServices.SignUpServices;

public abstract class BaseSignUpService
{
    protected readonly UserManager<User> UserManager;
    
    protected readonly UserCrud UserCrud;

    protected BaseSignUpService(
        UserManager<User> userManager,
        UserCrud userCrud)
    {
        UserManager = userManager;
        UserCrud = userCrud;
    }

    public abstract Task<User> SignUpAsync(Dictionary<string, string> claims);
}
