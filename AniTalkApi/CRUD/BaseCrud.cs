using AniTalkApi.DataLayer.DbModels;

namespace AniTalkApi.CRUD;

public class BaseCrud
{
    protected readonly AniTalkDbContext DbContext;

    public BaseCrud(AniTalkDbContext dbContext)
    {
        DbContext = dbContext;
    }

}