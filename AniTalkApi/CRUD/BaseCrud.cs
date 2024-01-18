using AniTalkApi.DataLayer.DbModels;

namespace AniTalkApi.CRUD;

public class BaseCrud
{
    protected AniTalkDbContext DbContext;

    public BaseCrud(AniTalkDbContext dbContext)
    {
        DbContext = dbContext;
    }

}